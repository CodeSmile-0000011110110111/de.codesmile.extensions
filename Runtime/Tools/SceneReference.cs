// Copyright (C) 2021-2024 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using UnityEditor;
using UnityEngine;

namespace CodeSmile.Tools
{
	/// <summary>
	///     Allows drag & drop of scene assets in the Inspector and serializes the scene's name for runtime scene loading.
	/// </summary>
	[Serializable]
	public sealed class SceneReference
	{
		[SerializeField, HideInInspector] private String m_SceneName;

		/// <summary>
		/// The scene name of the assigned SceneAsset.
		/// </summary>
		public String SceneName => m_SceneName;

		/// <summary>
		/// Call this from the MonoBehaviour that owns the SceneReference. Call it in OnValidate to ensure
		/// the SceneName is always in sync with the SceneAsset.
		/// </summary>
		public void OnValidate()
		{
#if UNITY_EDITOR
			SceneAsset = m_SceneAsset;
#endif
		}

#if UNITY_EDITOR
		[SerializeField] private SceneAsset m_SceneAsset;
		public SceneAsset SceneAsset
		{
			get => m_SceneAsset;
			set
			{
				m_SceneAsset = value;
				m_SceneName = m_SceneAsset != null ? m_SceneAsset.name : null;
			}
		}
#endif
	}
}
