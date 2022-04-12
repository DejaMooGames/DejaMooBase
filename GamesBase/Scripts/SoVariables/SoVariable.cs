using System;
using UnityEngine;

namespace DejaMoo.SoVariables
{
    public class SoVariable<T> : ScriptableObject where T : IComparable, IEquatable<T>
    {
        [Multiline] public string developerDescription;
        public T value;
    }
}