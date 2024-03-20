// Copyright (C) 2021-2024 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using UnityEngine;

namespace CodeSmile.Components
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
		// deferred to Start rather than Awake to allow Multiplayer Roles to possibly strip components during Awake
		// because this script moves the object to the root and Multiplayer Roles won't strip root objects
		private void Start()
		{
			if (enabled)
			{
				Apply(gameObject);
				Destroy(this);
			}
		}

		public static void Apply(GameObject go)
		{
			// DDoL only works on root game objects
			go.transform.parent = null;

			DontDestroyOnLoad(go);
		}
	}
}
