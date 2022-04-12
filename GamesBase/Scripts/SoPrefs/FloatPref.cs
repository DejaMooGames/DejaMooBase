using System;
using UnityEngine;

namespace DejaMoo.SoPrefs
{
    [CreateAssetMenu(menuName = "ScriptableObject/PlayerPref/Float")]
    public class FloatPref : SoPref<float>
    {
        public override float Value
        {
            get
            {
                if (PlayerPrefs.HasKey(key))
                    return PlayerPrefs.GetFloat(key);
                PlayerPrefs.SetFloat(key, defaultValue);
                return PlayerPrefs.GetFloat(key);
            }
            set
            {
                if (!(Math.Abs(Value - value) > Constants.FloatTol)) return;
                PlayerPrefs.SetFloat(key,value);
                editorValue = value;
                valueChanged.Raise(value);
            }
        }
    }
}
