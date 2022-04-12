using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

using UnityEngine.SceneManagement;

public class UtilityFunctions : EditorWindow
{
	private string layerNumStr = "0";

	[MenuItem("Window/Utility Functions")]
	public static void ShowWindow()
	{
		GetWindow<UtilityFunctions>("Utility Functions");
	}

	private void OnGUI()
	{
		if (GUILayout.Button("Find Layers"))
		{
			FindLayers();
		}
		
		GUILayout.Space(20f);
		layerNumStr = GUILayout.TextField(layerNumStr, 2);

		if (GUILayout.Button("Find Objects on Layer"))
		{
			if (int.TryParse(layerNumStr, out var num))
			{
				FindLayerObjects(num);
			}
			else
				Debug.Log("Input a valid int");
		}
		
		var so = new SerializedObject(UtilityFunctionData.Instance);
		var listProp = so.FindProperty("layerObjects");
		EditorGUILayout.PropertyField(listProp, true);

		if (GUILayout.Button("Find Objects with TextMesh Component"))
		{
			FindTextMeshComponents();
		}

		var textMeshProp = so.FindProperty("textMeshGameObjects");
		EditorGUILayout.PropertyField(textMeshProp, true);

		if (GUILayout.Button("Find all objects with Missing Components"))
		{
			FindGameObjectsWithMissingComponents();
		}

		var missingComponentProp = so.FindProperty("missingComponentObjects");
		EditorGUILayout.PropertyField(missingComponentProp, true);
	}

	private void FindLayerObjects(int layerNum)
	{
		Debug.Log($"FindingObjects on {LayerMask.LayerToName(layerNum)} layer");

		var gameObjects = new List<GameObject>();

		#if UNITY_2021
				if(UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() is { } stage)
		{
			var allTransforms = stage.prefabContentsRoot.GetComponentsInChildren<Transform>(true);
			foreach (var tform in allTransforms)
			{
				gameObjects.Add(tform.gameObject);
			}
			
			LayerFunctionsData.Instance.list.Clear();
			
			foreach (var obj in gameObjects)
			{
				if (obj.layer != layerNum) continue;
				LayerFunctionsData.Instance.list.Add(obj);
			}
		}
		else
		{
			gameObjects = FindObjectsOfType<GameObject>(true).ToList();
			LayerFunctionsData.Instance.list.Clear();
			foreach (var obj in gameObjects)
			{
				if (obj.layer != layerNum) continue;
				LayerFunctionsData.Instance.list.Add(obj);
			}
		}
		#else
		if(UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage() is { } stage)
		{
			var allTransforms = stage.prefabContentsRoot.GetComponentsInChildren<Transform>(true);
			foreach (var tform in allTransforms)
			{
				gameObjects.Add(tform.gameObject);
			}
			
			UtilityFunctionData.Instance.layerObjects.Clear();
			
			foreach (var obj in gameObjects)
			{
				if (obj.layer != layerNum) continue;
				UtilityFunctionData.Instance.layerObjects.Add(obj);
			}
		}
		else
		{
			gameObjects = FindObjectsOfType<GameObject>(true).ToList();
			UtilityFunctionData.Instance.layerObjects.Clear();
			foreach (var obj in gameObjects)
			{
				if (obj.layer != layerNum) continue;
				UtilityFunctionData.Instance.layerObjects.Add(obj);
			}
		}
		#endif
	}

	private void FindTextMeshComponents()
	{
		UtilityFunctionData.Instance.textMeshGameObjects.Clear();
		var scene = SceneManager.GetActiveScene();
		var rootObjects = scene.GetRootGameObjects();
		foreach (var rootObject in rootObjects)
		{
			var components = rootObject.GetComponentsInChildren<TextMesh>(true);
			foreach (var component in components)
			{
				if(UtilityFunctionData.Instance.textMeshGameObjects.Contains(component.gameObject))
					continue;
				UtilityFunctionData.Instance.textMeshGameObjects.Add(component.gameObject);
			}
		}
	}

	private static void PrintObjectAndRootName(GameObject obj)
	{
		var root = obj.transform.root;
		Debug.Log(root == obj.transform
			          ? $"{obj.name}"
			          : $"{obj.name} under {root.name}");
	}

	private void FindLayers()
	{
		Debug.Log("Check used layers");
		var layerCount = new Dictionary<string, int>();
		var gameObjects = FindObjectsOfType<GameObject>().ToList();

		// iterate objects and save to dictionary
		foreach (var obj in gameObjects)
		{
			var layerName = LayerMask.LayerToName(obj.layer);
			if (layerCount.ContainsKey(layerName))
			{
				layerCount[layerName]++;
			}
			else
			{
				layerCount.Add(layerName, 1);
			}
		}

		// log to console
		foreach (var entry in layerCount)
		{
			Debug.Log(entry.Key + ": " + entry.Value);
		}

		// unused layers
		var layerNames = new List<string>();
		for (var i = 4; i <= 31; i++) //user defined layers start with layer 8 and unity supports 31 layers
		{
			var layerN = LayerMask.LayerToName(i); //get the name of the layer
			if (
				layerN.Length >
				0) //only add the layer if it has been named (comment this line out if you want every layer)
				layerNames.Add(layerN);
		}

		var listOfKeys = layerCount.Keys.ToList();
		var unusedLayers = layerNames.Except(listOfKeys).ToList();
		var joined = string.Join(", ", unusedLayers);
		var scene = SceneManager.GetActiveScene();
		Debug.Log("Unused layers in " + scene.name + ": " + joined);

		Debug.Log("Check used layers done");
	}

	public void FindGameObjectsWithMissingComponents()
	{
		UtilityFunctionData.Instance.missingComponentObjects = new List<GameObject>();
		var gameObjects = FindObjectsOfType<GameObject>().ToList();
		foreach (var gameObject in gameObjects)
		{
			var components = gameObject.GetComponents<Component>();
			foreach (var component in components)
			{
				if(component is null && !UtilityFunctionData.Instance.missingComponentObjects.Contains(gameObject))
					UtilityFunctionData.Instance.missingComponentObjects.Add(gameObject);
			}
		}
	}
}