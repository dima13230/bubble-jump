using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public float MinXDistance = 1;
    public float MaxXDistance = 3f;

    [Tooltip("На такое значение повышается высота каждой новой платформы")]
    public float DeltaGlobalHeightOffset = 0.7f;

    public PlatformType[] PlatformTypes;

    [Tooltip("Столько платформ будет создано в уровне")]
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
        // в начале уровня непосредственно под игроком всегда спавнится обычная платформа
        Instantiate(PlatformType.GetPlatform(PlatformTypes, "Normal").Platform, Camera.main.ViewportToWorldPoint(positionCenter), Quaternion.identity);

        // создаём по 5 платформ влево и вправо от центральной платформы
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
            globalHeightOffset += DeltaGlobalHeightOffset;
        }
    }

}