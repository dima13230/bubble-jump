using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils
{
    /// <summary>
    /// ���������� 0, ���� value ��������� � ���������� [-deadZone..deadZone], ����� ���������� value
    /// </summary>
    /// <returns></returns>
    public static float ProcessDeadZone(float value, float deadZone)
    {
        if (value >= -deadZone && value <= deadZone)
        {
            return 0;
        }
        return value;
    }
}
