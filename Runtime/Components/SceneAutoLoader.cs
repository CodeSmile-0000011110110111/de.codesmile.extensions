// Copyright (C) 2021-2024 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using CodeSmile.Tools;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeSmile.Components
{
	public class SceneAutoLoader : OneTimeTaskBehaviour
	{
		[SerializeField] private SceneReference m_SceneToLoad;
		[SerializeField] private Single m_TimeToWaitBeforeLoad;

		public static void DestroyAll()
		{
			foreach (var loader in FindObjectsByType<SceneAutoLoader>(FindObjectsSortMode.None))
				loader.Disable();
		}

		public static void LoadScene()
		{
			var loader = FindAnyObjectByType<SceneAutoLoader>();
			if (loader != null)
				loader.LoadSceneInternal();
		}

		private void OnValidate()
		{
			m_SceneToLoad?.OnValidate();
			m_TimeToWaitBeforeLoad = Mathf.Max(0f, m_TimeToWaitBeforeLoad);
		}

		private void OnDestroy() => StopAllCoroutines();

		private void Start()
		{
			if (String.IsNullOrWhiteSpace(m_SceneToLoad?.SceneName))
				throw new ArgumentException($"{nameof(SceneAutoLoader)}: scene not assigned");

			StartCoroutine(LoadSceneAfterDelay(m_TimeToWaitBeforeLoad));
		}

		private IEnumerator LoadSceneAfterDelay(Single timeToWaitBeforeLoad)
		{
			yield return new WaitForSeconds(m_TimeToWaitBeforeLoad);

			LoadSceneInternal();
		}

		public void LoadSceneInternal()
		{
			Debug.Log($"{nameof(SceneAutoLoader)}: loading {m_SceneToLoad.SceneName}");
			SceneManager.LoadScene(m_SceneToLoad.SceneName, LoadSceneMode.Single);
			StopAllCoroutines();
			TaskPerformed();
		}

		public void Disable()
		{
			enabled = false;
			TaskPerformed();
		}
	}
}
