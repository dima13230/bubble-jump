using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float MinXDistance = 1;
    public float MaxXDistance = 3f;

    public float MinYDistance = 1;
    public float MaxYDistance = 3;

    public PlatformType[] PlatformTypes;

    public int PlatformsPerGeneration = 20;

    float globalHeightOffset;

    // Start is called before the first frame update
    void Start()
    {
        // создаём безопасные платформы на нулевой высоте
        Vector3 positionCenter = new Vector3(
            0.5f,
            0,
            Camera.main.nearClipPlane);
        Instantiate(PlatformType.GetPlatform(PlatformTypes, "Normal").Platform, Camera.main.ViewportToWorldPoint(positionCenter), Quaternion.identity);

        for (float i = 1; i < 5; i++)
        {
            Vector3 position1 = new Vector3(
                0.5f + i / 5,
                0,
                Camera.main.nearClipPlane);
            Instantiate(PlatformType.RandomSafePlatform(PlatformTypes).Platform, Camera.main.ViewportToWorldPoint(position1), Quaternion.identity);

            Vector3 position2 = new Vector3(
                0.5f - i / 5,
                0,
                Camera.main.nearClipPlane);
            Instantiate(PlatformType.RandomSafePlatform(PlatformTypes).Platform, Camera.main.ViewportToWorldPoint(position2), Quaternion.identity);
        }

        // создаём остальные платформы
        for (int i = 0; i < PlatformsPerGeneration; i++)
        {
            Vector3 position = new Vector3(
                Random.Range(MinXDistance, MaxXDistance),
                globalHeightOffset + 0.8f,
                Camera.main.nearClipPlane);

            PlatformType currentType = PlatformType.RandomPlatform(PlatformTypes);
            Instantiate(currentType.Platform, position, Quaternion.identity);
            globalHeightOffset += 0.7f;
        }
    }

}