using UnityEngine;

namespace DejaMoo.SoPrefs
{
    [CreateAssetMenu(menuName = "ScriptableObject/PlayerPref/Bool")]
    public class BoolPref : SoPref<bool>
    {
        public override bool Value 
        {
            get
            {
                if (PlayerPrefs.HasKey(key))
                    return PlayerPrefs.GetInt(key) != 0;
                PlayerPrefs.SetInt(key, defaultValue ? 1 : 0);
                return PlayerPrefs.GetInt(key) != 0;
            }
            set
            {
                if (Value.Equals(value)) return;
                PlayerPrefs.SetInt(key, value ? 1 : 0);
                editorValue = value;
                valueChanged.Raise(value);
            }
        }
    }
}