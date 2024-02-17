// Copyright (C) 2021-2024 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using UnityEngine;

namespace CodeSmile.Components
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
		private void Awake()
		{
			if (enabled)
				DontDestroyOnLoad(gameObject);
		}
	}
}
