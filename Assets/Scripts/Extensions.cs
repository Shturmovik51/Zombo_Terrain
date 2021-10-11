using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

namespace ZomboTerrain
{
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
                Debug.Log($"{item.Key} - встречается {item.count} раз(а)");
            }
        }

        public static void SaimonSaidStartCoroutine(this IEnumerator self)
        {
            var platform = new GameObject(name:"Coroutine").AddComponent<CoroutineMonobeh>();
            platform.StartCoroutine(self);
        }
    }
}

