using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DejaMoo.Extensions
{
    public static class GeneralExtensions
    {
        public static bool Approx(this Quaternion quatA, Quaternion quatB, float tolerance)
        {
            return Quaternion.Dot(quatA, quatB) > 1 - tolerance;
        }
        
        public static int Digits(this int n)
        {
            if (n >= 0)
            {
                if (n < 10) return 1;
                if (n < 100) return 2;
                if (n < 1000) return 3;
                if (n < 10000) return 4;
                if (n < 100000) return 5;
                if (n < 1000000) return 6;
                if (n < 10000000) return 7;
                if (n < 100000000) return 8;
                return n < 1000000000 ? 9 : 10;
            }

            if (n > -10) return 2;
            if (n > -100) return 3;
            if (n > -1000) return 4;
            if (n > -10000) return 5;
            if (n > -100000) return 6;
            if (n > -1000000) return 7;
            if (n > -10000000) return 8;
            if (n > -100000000) return 9;
            return n > -1000000000 ? 10 : 11;
        }

        public static bool Approx(this float floatA, float floatB, float tolerance)
        {
            return Mathf.Abs(floatA - floatB) < tolerance;
        }

        public static T2 GetRandomValue<T1, T2>(this Dictionary<T1, T2> dictionary)
        {
            return dictionary.ElementAt(Random.Range(0, dictionary.Count)).Value;
        }

        public static T1 GetRandomKey<T1, T2>(this Dictionary<T1, T2> dictionary)
        {
            return dictionary.ElementAt(Random.Range(0, dictionary.Count)).Key;
        }

        public static Color WithAlpha(this Color color, float alpha)
        {
            alpha = Mathf.Clamp(alpha, 0, 1);
            return new Color(color.r, color.g, color.b, alpha);
        }
    }
}
