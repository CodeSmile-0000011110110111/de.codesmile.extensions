// Copyright (C) 2021-2024 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using UnityEngine;

namespace CodeSmile.Components
{
	/// <summary>
	///     Hooks the Escape key to Application.Quit.
	///     <remarks>
	///         It annoys me that builds no longer support Alt+F4 and thus there is no way
	///         to quit a build except going through Task Manager.
	///     </remarks>
	/// </summary>
	internal class PressKeyToQuit : MonoBehaviour
	{
		// [Tooltip("The modifiers must be held for the QuitKey to be registered.")]
		// public KeyCode Modifier = KeyCode.LeftControl;

		[Tooltip("The key that will quit if the Modifiers are also held down.")]
		public KeyCode QuitKey = KeyCode.Escape;

		private void Update()
		{
			if (Input.GetKeyDown(QuitKey))
				Application.Quit();
		}
	}
}
