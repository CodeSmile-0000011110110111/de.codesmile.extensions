// Copyright (C) 2021-2023 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeSmile.Extensions
{
	/// <summary>
	/// UnityEngine.Object extension methods
	/// </summary>
	public static class ObjectExt
	{
		/// <summary>
		///     Destroys the object regardless of Edit or Play mode.
		/// </summary>
		/// <remarks>
		///     Depending on editor vs play mode it calls either DestroyImmediate or Destroy.
		///		In Builds it directly calls Destroy.
		/// </remarks>
		/// <param name="self"></param>
#if UNITY_EDITOR
		public static void DestroyInAnyMode(this Object self)
		{
			if (Application.isPlaying == false)
				Object.DestroyImmediate(self);
			else
				Object.Destroy(self);
		}
#else
		public static void DestroyInAnyMode(this Object self) => Object.Destroy(self);
#endif
	}
}
