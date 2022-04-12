using UnityEngine;

namespace DejaMoo.SoPrefs
{
    [CreateAssetMenu(menuName = "ScriptableObject/PlayerPref/Int")]
    public class IntPref : SoPref<int>
    {
        public override int Value 
        {
            get
            {
                if (PlayerPrefs.HasKey(key))
                    return PlayerPrefs.GetInt(key);
                PlayerPrefs.SetInt(key, defaultValue);
                return PlayerPrefs.GetInt(key);
            }
            set
            {
                if (Value.Equals(value)) return;
                PlayerPrefs.SetInt(key, value);
                editorValue = value;
                valueChanged.Raise(value);
            } 
        }
    }
}