// Copyright (C) 2021-2024 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using UnityEngine;

namespace CodeSmile.Components
{
	public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T s_Instance;
		private static Boolean s_Instantiated;

		public static T Singleton
		{
			get
			{
				if (s_Instantiated == false)
					InstantiateSingleton();

				return s_Instance;
			}
		}

		protected static void InstantiateSingleton()
		{
			if (s_Instantiated == false)
			{
				s_Instantiated = true;
				s_Instance = CreateDontDestroyOnLoadInstance();
			}
		}

		private static T CreateDontDestroyOnLoadInstance()
		{
			var instance = new GameObject($"{typeof(T)}", typeof(T)).GetComponent<T>();
			DontDestroyOnLoad(instance);
			return instance;
		}

		/// <summary>
		///     To be called by implementations to reset static fields during RuntimeInit
		/// </summary>
		protected void ResetStaticFields()
		{
			s_Instantiated = false;
			s_Instance = null;
		}

		protected abstract void RuntimeInitializeOnLoad();
	}
}
