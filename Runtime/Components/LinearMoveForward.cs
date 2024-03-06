// Copyright (C) 2021-2024 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using UnityEngine;

namespace CodeSmile.Components
{
	public class LinearMoveForward : MonoBehaviour
	{
		[SerializeField] private Single m_SpeedInUnitsPerSecond = 1f;

		private void FixedUpdate()
		{
			var frameSpeed = m_SpeedInUnitsPerSecond * Time.deltaTime;
			var velocity = transform.forward * frameSpeed;
			transform.localPosition += velocity;
		}
	}
}
