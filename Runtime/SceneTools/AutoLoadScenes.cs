// Copyright (C) 2021-2024 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeSmile.SceneTools
{
	/// <summary>
	///     A list of scenes that are loaded automatically and additively every time the game launches (enter playmode, build).
	///     The scenes are guaranteed to be loaded before Awake() of any script in the first scene.
	/// </summary>
	[CreateAssetMenu(fileName = nameof(AutoLoadScenes), menuName = "Scriptable Objects/" + nameof(AutoLoadScenes))]
	public class AutoLoadScenes : ScriptableObject
	{
		[SerializeField] private List<SceneReference> m_AdditiveScenes;

		public IReadOnlyList<SceneReference> AdditiveScenes => m_AdditiveScenes.AsReadOnly();

		public static AutoLoadScenes Instance => Resources.Load<AutoLoadScenes>(nameof(AutoLoadScenes));

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void RuntimeInit_AdditiveLoadScenes()
		{
			foreach (var sceneRef in Instance.AdditiveScenes)
				SceneManager.LoadScene(sceneRef.SceneName, LoadSceneMode.Additive);
		}

		private void OnValidate() => ValidateSceneReferences();

		private void ValidateSceneReferences()
		{
#if UNITY_EDITOR
			// make sure scene names are up to date
			foreach (var sceneRef in m_AdditiveScenes)
				sceneRef.OnValidate();

			// make sure we keep only unique names but retain the order
			var set = new List<SceneReference>();
			foreach (var sceneRef in m_AdditiveScenes)
			{
				if (sceneRef.SceneName != null && set.Contains(sceneRef) == false)
					set.Add(sceneRef);
			}

			m_AdditiveScenes = set.ToList();

			EditorUtility.SetDirty(this);
			AssetDatabase.SaveAssetIfDirty(this);
#endif
		}

		public void AddScene(Scene scene)
		{
#if UNITY_EDITOR
			var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scene.path);
			m_AdditiveScenes.Add(new SceneReference(sceneAsset));

			ValidateSceneReferences();
#endif
		}
	}
}
