using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/LogTypeSprites", fileName = "LogTypeSprites")]
public class LogTypeSprites : ScriptableObject
{
    [SerializeField] private Sprite logSprite;
    [SerializeField] private Sprite warningSprite;
    [SerializeField] private Sprite errorSprite;
    [SerializeField] private Sprite exceptionSprite;
    [SerializeField] private Sprite assertSprite;

    public Sprite GetSpriteByType(LogType type)
    {
        return type switch
        {
            LogType.Error => errorSprite,
            LogType.Assert => assertSprite,
            LogType.Warning => warningSprite,
            LogType.Log => logSprite,
            LogType.Exception => exceptionSprite,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}