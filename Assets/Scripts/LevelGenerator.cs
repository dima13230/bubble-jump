using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float MinXDistance = 1;
    public float MaxXDistance = 3f;
    public PlatformType[] PlatformTypes;
    public float PlatformHeightVariation = 0.2f; // Variation in platform heights
    public int PlatformsPerGeneration = 150;
    public float DeltaGlobalHeightOffset = 0.8f; // Adjust this value as needed

    private GameObject lastPlatform;
    private Camera mainCamera;
    private float platformWidth;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        platformWidth = CalculatePlatformWidth();

        // Generate the first platform just below the player's starting position
        Vector3 playerStartingPosition = new Vector3(0.5f, 0f, mainCamera.nearClipPlane);
        InstantiatePlatform(playerStartingPosition, true);

        for (int i = 0; i < PlatformsPerGeneration; i++)
        {
            // Calculate the position of the next platform based on the last platform
            Vector3 position = CalculateNextPlatformPosition();

            // Randomly adjust the platform height within the specified variation range
            position.y += Random.Range(-PlatformHeightVariation, PlatformHeightVariation);

            // Instantiate the platform at the calculated position
            GameObject newPlatform = InstantiatePlatform(position);

            // Update the last platform reference
            lastPlatform = newPlatform;
        }
    }

    Vector3 CalculateNextPlatformPosition()
    {
        float lastPositionX = lastPlatform != null ? lastPlatform.transform.position.x : 0f;
        float lastPositionY = lastPlatform != null ? lastPlatform.transform.position.y : 0;

        // Calculate the position based on the last platform's position
        float randomX = (int)Random.Range(-1f, 1f) + Random.Range(MinXDistance, MaxXDistance);
        float newX = lastPositionX + randomX;
        float newY = lastPositionY + DeltaGlobalHeightOffset;

        // Check if the platform is outside the viewport bounds
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(new Vector3(newX, newY, mainCamera.nearClipPlane));

        while (viewportPos.x < 0.4 || viewportPos.x > 0.6)
        {
            viewportPos.x = Mathf.Clamp01(viewportPos.x);
            Vector3 worldPos = mainCamera.ViewportToWorldPoint(viewportPos);
            newX = worldPos.x;
        }

        return new Vector3(newX, newY, mainCamera.nearClipPlane);
    }

    float CalculatePlatformWidth()
    {
        // Calculate the width of the platform based on the screen size
        float screenHeight = mainCamera.orthographicSize * 2;
        float screenWidth = screenHeight * mainCamera.aspect;

        // Assume MinXDistance and MaxXDistance are normalized (0 to 1)
        return (MaxXDistance - MinXDistance) * screenWidth;
    }

    GameObject InstantiatePlatform(Vector3 position, bool mustBeSafe = false)
    {
        PlatformType currentType = PlatformType.RandomPlatform(PlatformTypes);

        // Instantiate and return the platform at the given position
        while (mustBeSafe && !currentType.CanBeSpawnedAsSafe)
            currentType = PlatformType.RandomPlatform(PlatformTypes);

        // Convert normalized position to world coordinates
        //Vector3 worldPosition = mainCamera.ViewportToWorldPoint(position);
        return Instantiate(currentType.Platform, position, Quaternion.identity);
    }
}
