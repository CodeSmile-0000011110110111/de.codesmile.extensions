// Copyright (C) 2021-2024 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using NUnit.Framework;
using System;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace CodeSmile.Tests
{
	public class RandomExtTests
	{
		private Random m_Random;

		[SetUp]
		public void SetUp() => m_Random =
			new Random((UInt32)(Time.realtimeSinceStartupAsDouble * 10.0) + 1);

		[Test] public void RandomNext_OnCircle_ChangesState()
		{
			var initialState = m_Random.state;
			m_Random.NextOnCircleDirection(out var direction);

			Assert.AreNotEqual(m_Random.state, initialState);
		}

		[TestCase(0f), TestCase(1f), TestCase(3.333333f), TestCase(-111.222f)]
		public void RandomNext_OnCircle_DirectionHasRadiusLength(Single radius)
		{
			m_Random.NextOnCircleDirection(out var direction, radius);

			Assert.AreEqual(math.abs(radius), math.length(direction));
		}

		[Test] public void RandomNext_InsideCircle_ChangesState()
		{
			var initialState = m_Random.state;
			m_Random.NextInsideCircleDirection(out var direction);

			Assert.AreNotEqual(m_Random.state, initialState);
		}

		[TestCase(0f), TestCase(1f), TestCase(3.333333f), TestCase(-111.222f)]
		public void RandomNext_InsideCircle_DirectionWithinRadiusLength(Single radius)
		{
			m_Random.NextInsideCircleDirection(out var direction, radius);

			Assert.GreaterOrEqual(math.abs(radius), math.length(direction));
		}

		[Test] public void RandomNext_OnSphere_ChangesState()
		{
			var initialState = m_Random.state;
			m_Random.NextOnSphereDirection(out var direction);

			Assert.AreNotEqual(m_Random.state, initialState);
		}

		[TestCase(0f), TestCase(1f), TestCase(3.333333f), TestCase(-111.222f)]
		public void RandomNext_OnSphere_DirectionHasRadiusLength(Single radius)
		{
			m_Random.NextOnSphereDirection(out var direction, radius);

			Assert.AreEqual(math.abs(radius), math.length(direction));
		}

		[Test] public void RandomNext_InsideSphere_ChangesState()
		{
			var initialState = m_Random.state;
			m_Random.NextInsideSphereDirection(out var direction);

			Assert.AreNotEqual(m_Random.state, initialState);
		}

		[TestCase(0f), TestCase(1f), TestCase(3.333333f), TestCase(-111.222f)]
		public void RandomNext_InsideSphere_DirectionWithinRadiusLength(Single radius)
		{
			m_Random.NextInsideSphereDirection(out var direction, radius);

			Assert.GreaterOrEqual(math.abs(radius), math.length(direction));
		}

		[Test] public void RandomSeed_NextSeed_IncrementsSeedByOne()
		{
			RandomExt.NextSeed = 0;

			Assert.AreEqual(1, RandomExt.NextSeed);
			Assert.AreEqual(2, RandomExt.NextSeed);
			Assert.AreEqual(3, RandomExt.NextSeed);
		}

		[Test] public void RandomSeed_NextSeedOverflows_ReturnsOneAndContinuesSequence()
		{
			RandomExt.NextSeed = UInt32.MaxValue - 1;

			Assert.AreEqual(UInt32.MaxValue, RandomExt.NextSeed);
			Assert.AreEqual(1, RandomExt.NextSeed);
			Assert.AreEqual(2, RandomExt.NextSeed);
		}
	}
}
