using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
	private static T instance;

	public static T Instance
	{
		get
		{
			if (instance is { }) return instance;
			
			var assets = Resources.LoadAll<T>("");
			if (assets is null || assets.Length < 1)
				instance = CreateInstance<T>();
			else
				instance = assets[0];

			return instance;
		}
	}
}