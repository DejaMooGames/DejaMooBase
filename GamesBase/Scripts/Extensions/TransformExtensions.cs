using System.Collections.Generic;
using UnityEngine;

namespace DejaMoo.Extensions
{
    public static class TransformExtensions
    {
        public static List<GameObject> GetAllChildren(this Transform parent)
        {
            var children = new List<GameObject>();
            for (var i = 0; i < parent.childCount; i++)
            {
                var child = parent.GetChild(i).gameObject;
                children.Add(child);
                children.AddRange(GetAllChildren(child.transform));
            }

            return children;
        }
    }
}