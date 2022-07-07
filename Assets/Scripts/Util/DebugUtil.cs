using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUtil
{
    /// <summary>
    /// ¬озвращает true если ни один элемент в массиве не равен нулю, false в противном случае
    /// </summary>
    /// <param name="objects">ѕровер€емый массив</param>
    /// <returns>true если ни один элемент в массиве не равен нулю, false в противном случае</returns>
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
