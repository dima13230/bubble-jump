using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUtil
{
    /// <summary>
    /// ���������� true ���� �� ���� ������� � ������� �� ����� ����, false � ��������� ������
    /// </summary>
    /// <param name="objects">����������� ������</param>
    /// <returns>true ���� �� ���� ������� � ������� �� ����� ����, false � ��������� ������</returns>
    public static bool AssertNotNull(params object[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] == null)
                return false;
        }
        return true;
    }
}
