using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathUtils
{
    /// <summary>
    /// Возвращает 0, если value находится в промежутке [-deadZone..deadZone], иначе возвращает value
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
