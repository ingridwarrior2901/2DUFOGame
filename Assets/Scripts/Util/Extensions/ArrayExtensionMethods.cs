using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ArrayExtensionMethods
{
    public static bool IsEmpty<T>(this T[] self) where T : class
    {
        return self.Length == 0;
    }
}