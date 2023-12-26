// Copyright (C) 2021-2023 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeSmile.Extensions
{
	/// <summary>
	///     UnityEngine.Transform extension methods
	/// </summary>
	public static class TransformExt
	{
		/// <summary>
		///     Destroys (in any mode) all children of the transform.
		/// </summary>
		/// <param name="t"></param>
		public static void DestroyAllChildren(this Transform t)
		{
			for (var i = t.childCount - 1; i >= 0; i--)
				t.GetChild(i).gameObject.DestroyInAnyMode();
		}

		/// <summary>
		///     Destroys the transform's GameObject regardless of Edit or Play mode.
		/// </summary>
		/// <remarks>
		///     Depending on editor vs play mode it calls either DestroyImmediate or Destroy.
		///     In Builds it directly calls Destroy.
		/// </remarks>
		/// <remarks>
		///     Transforms cannot be destroyed, Unity throws an error if you try.
		///		This extension only serves to remove the additional and sometimes annoyingly easy to forget
		///		"t.gameObject.Destroy()" indirection in code. This makes it a little cleaner: "t.Destroy()"
		/// </remarks>
		/// <param name="self"></param>
#if UNITY_EDITOR
		public static void DestroyInAnyMode(this Transform self)
		{
			if (Application.isPlaying == false)
				Object.DestroyImmediate(self.gameObject);
			else
				Object.Destroy(self.gameObject);
		}
#else
		public static void DestroyInAnyMode(this Transform self) => Object.Destroy(self.gameObject);
#endif

		// public static Transform FindOrCreateChild(this Transform parent, String name,
		// 	HideFlags hideFlags = HideFlags.None)
		// {
		// 	var t = parent.Find(name);
		// 	if (t != null)
		// 		return t;
		//
		// 	return new GameObject(name)
		// 	{
		// 		hideFlags = hideFlags,
		// 		transform = { parent = parent.transform },
		// 	}.transform;
		// }
	}
}
