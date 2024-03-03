// Copyright (C) 2021-2024 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using UnityEngine;

namespace CodeSmile.Components
{
	public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T s_Instance;

		public static T Singleton
		{
			get
			{
				if (s_Instance == null)
				{
					var go = new GameObject($"{typeof(T)}", typeof(T));
					s_Instance = go.GetComponent<T>();
				}

				return s_Instance;
			}
		}
	}
}
