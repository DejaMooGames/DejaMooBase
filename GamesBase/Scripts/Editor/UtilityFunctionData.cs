using System.Collections.Generic;
using UnityEngine;

public class UtilityFunctionData : ScriptableObject
{
	private static UtilityFunctionData instance;

	public static UtilityFunctionData Instance
	{
		get
		{
			if (instance != null) return instance;
			
			var assets = Resources.LoadAll<UtilityFunctionData>("");
			if (assets is null || assets.Length < 1)
				instance = CreateInstance<UtilityFunctionData>();
			else
				instance = assets[0];

			return instance;
		}
	}

	public List<GameObject> layerObjects = new List<GameObject>();
	public List<GameObject> textMeshGameObjects = new List<GameObject>();
	public List<GameObject> missingComponentObjects = new List<GameObject>();
}