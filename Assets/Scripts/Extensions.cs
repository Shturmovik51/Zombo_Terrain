using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class Extensions
{
    public static int HowMuchChars(this string self)
    {
        return self.Length;
    }

    public static int HowMuchParameters<T>(this List<T> self)
    {
        return self.Count;
    }

    public static void LogElementsCount<T>(this List<T> self)
    {
        var list = from parameter in self
                   group parameter by parameter into groups
                   let count = groups.Count()
                   select new { groups.Key, count };

        foreach (var item in list)
        {
            Debug.Log($"{item.Key} - ����������� {item.count} ���(�)");
        }
    }

    //public static BuffMethods BuffUpply(this Buff self)
    //{

    //}


}
