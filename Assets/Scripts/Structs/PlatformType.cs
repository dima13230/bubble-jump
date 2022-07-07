using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlatformType
{
    public string Name;
    public GameObject Platform;

    [Tooltip("Вероятность спавна платформы вычисляется с помощью весового распределения вероятности")]
    public int SpawnWeight;

    [Tooltip("Под безопасной понимается неподвижная платформа, на которой можно прыгать бесконечное количество раз. Выбираются случайным образом для начала уровня")]
    public bool CanBeSpawnedAsSafe;

    // служебная переменная для удобного вычисления суммированного с предыдущими элементами веса вероятности спавна платформы
    private float cumulativeWeight;

    /// <summary>
    /// Возвращает случайную платформу с учётом посчитанной вероятности её появления
    /// </summary>
    /// <param name="array">Заданный массив разновидностей платформ</param>
    /// <returns></returns>
    public static PlatformType RandomPlatform(PlatformType[] array)
    {
        int cumulativeWeightSum = 0;

        for (int i = 0; i < array.Length; i++)
        {
            cumulativeWeightSum += array[i].SpawnWeight;
            array[i].cumulativeWeight = cumulativeWeightSum;
        }

        float probability = Random.value * cumulativeWeightSum;
        return array.FirstOrDefault(p => p.cumulativeWeight > probability);
        
    }

    /// <summary>
    /// Возвращает случайную безопасную платформу из общего массива разновидностей платформ с учётом посчитанной вероятности её появления
    /// </summary>
    /// <param name="array">Заданный массив разновидностей платформ</param>
    /// <returns></returns>
    public static PlatformType RandomSafePlatform(PlatformType[] array)
    {
        int cumulativeWeightSum = 0;

        PlatformType[] safePlatforms = array.Where(p => p.CanBeSpawnedAsSafe).ToArray();

        for (int i = 0; i < safePlatforms.Length; i++)
        {
                cumulativeWeightSum += safePlatforms[i].SpawnWeight;
                safePlatforms[i].cumulativeWeight = cumulativeWeightSum;
        }

        float probability = Random.value * cumulativeWeightSum;
        return safePlatforms.FirstOrDefault(p => p.cumulativeWeight > probability);

    }

    /// <summary>
    /// Получает платформу по её названию из массива разновидностей платформ
    /// </summary>
    /// <param name="array">Заданный массив разновидностей платформ</param>
    /// <param name="name">Название искомой платформы</param>
    /// <returns></returns>
    public static PlatformType GetPlatform(PlatformType[] array, string name)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Name == name)
                return array[i];
        }
        return null;
    }
}
