using System.Collections.Generic;
using UnityEngine;

namespace DejaMoo.Utilities
{
    public class EditorSet<T> : ScriptableObject
    {
        public List<T> set = new List<T>();
    }
}
