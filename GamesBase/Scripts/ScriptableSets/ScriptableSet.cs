using System.Collections;
using System.Collections.Generic;
using DejaMoo.Extensions;
using UnityEngine;

public class ScriptableSet<T> : ScriptableObject
{
    public List<T> set = new List<T>();
}