// Copyright (C) 2021-2024 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;
using UnityEngine;

namespace CodeSmile.Components
{
	[RequireComponent(typeof(SphereCollider))]
	public class DestroyOnCollision : MonoBehaviour
	{
		[Tooltip("GameObject to destroy on collision. If unset (null) will destroy the GameObject this script is attached to.")]
		[SerializeField] private GameObject m_ObjectToDestroy;
		[Tooltip("Prefab to instantiate in the same place as the destroyed object. Does nothing if not assigned.")]
		[SerializeField] private GameObject m_SpawnInPlacePrefab;
		[SerializeField] private Boolean m_CollidesWithTriggers;

		private SphereCollider m_Collider;

		private void Awake() => m_Collider = GetComponent<SphereCollider>();

		private void FixedUpdate()
		{
			var pos = transform.position + m_Collider.center;
			var layerMask = m_Collider.includeLayers.value;
			var triggerCollide = m_CollidesWithTriggers ? QueryTriggerInteraction.Collide : QueryTriggerInteraction.Ignore;

			// if (Physics.CheckSphere(pos, m_Collider.radius, layerMask, triggerCollide))
			// 	DestroyObject();

			var hitColliders = Physics.OverlapSphere(pos, m_Collider.radius, layerMask, triggerCollide);
			foreach (var hitCollider in hitColliders)
			{
				var responder = hitCollider.GetComponent<IImpactResponder>();
				if (responder != null)
					responder.Impact(this.transform);
			}
			
			if (hitColliders.Length > 0)
				DestroyObject();
		}

		private void DestroyObject()
		{
			if (m_SpawnInPlacePrefab != null)
			{
				var t = m_ObjectToDestroy.transform;
				Instantiate(m_SpawnInPlacePrefab, t.position, t.rotation, t.parent);
			}

			Destroy(m_ObjectToDestroy != null ? m_ObjectToDestroy : gameObject);
		}
	}
}
