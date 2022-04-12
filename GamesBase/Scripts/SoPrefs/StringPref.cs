using UnityEngine;

namespace DejaMoo.SoPrefs
{
    [CreateAssetMenu(menuName = "ScriptableObject/PlayerPref/String")]
    public class StringPref : SoPref<string>
    {
        public override string Value 
        {
            get
            {
                if (PlayerPrefs.HasKey(key))
                    return PlayerPrefs.GetString(key);
                PlayerPrefs.SetString(key, defaultValue);
                return PlayerPrefs.GetString(key);
            }
            set
            {
                if (Value.Equals(value)) return;
                PlayerPrefs.SetString(key, value);
                editorValue = value;
                valueChanged.Raise(value);
            } 
        }
    }
}