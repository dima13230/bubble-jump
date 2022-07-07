using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlatformType
{
    public string Name;
    public GameObject Platform;

    [Tooltip("����������� ������ ��������� ����������� � ������� �������� ������������� �����������")]
    public int SpawnWeight;

    [Tooltip("��� ���������� ���������� ����������� ���������, �� ������� ����� ������� ����������� ���������� ���. ���������� ��������� ������� ��� ������ ������")]
    public bool CanBeSpawnedAsSafe;

    // ��������� ���������� ��� �������� ���������� �������������� � ����������� ���������� ���� ����������� ������ ���������
    private float cumulativeWeight;

    /// <summary>
    /// ���������� ��������� ��������� � ������ ����������� ����������� � ���������
    /// </summary>
    /// <param name="array">�������� ������ �������������� ��������</param>
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
    /// ���������� ��������� ���������� ��������� �� ������ ������� �������������� �������� � ������ ����������� ����������� � ���������
    /// </summary>
    /// <param name="array">�������� ������ �������������� ��������</param>
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
    /// �������� ��������� �� � �������� �� ������� �������������� ��������
    /// </summary>
    /// <param name="array">�������� ������ �������������� ��������</param>
    /// <param name="name">�������� ������� ���������</param>
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
