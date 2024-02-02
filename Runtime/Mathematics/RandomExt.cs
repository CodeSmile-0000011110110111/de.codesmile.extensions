// Copyright (C) 2021-2024 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace CodeSmile
{
	[BurstCompile]
	public static class RandomExt
	{
		private static UInt32 s_RunningSeed;

		[BurstCompile, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextOnCircleDirection(this ref Random rnd, out float2 direction,
			Single radius = 1f) => direction = rnd.NextFloat2Direction() * radius;

		[BurstCompile, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextInsideCircleDirection(this ref Random rnd, out float2 direction,
			Single radius = 1f) =>
			direction = rnd.NextFloat2Direction() * rnd.NextFloat2() * radius;

		[BurstCompile, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextOnSphereDirection(this ref Random rnd, out float3 direction,
			Single radius = 1f) => direction = rnd.NextFloat3Direction() * radius;

		[BurstCompile, MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextInsideSphereDirection(this ref Random rnd, out float3 direction,
			Single radius = 1f) =>
			direction = rnd.NextFloat3Direction() * rnd.NextFloat3() * radius;

		public static UInt32 NextSeed
		{
			get => s_RunningSeed = math.max(1, s_RunningSeed + 1);
			set => s_RunningSeed = value;
		}
	}
}
