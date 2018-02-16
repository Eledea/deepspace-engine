using DeepSpace.Core;
using System;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// The MyEntityTransformComponent class defines a Transform of an Entity.
	/// </summary>
	public class MyEntityTransformComponent : MyEntityComponentBase
	{
		public Entity Entity { get; private set; }

		public MyEntityTransformComponent(Entity e, Vector3D position, Quaternion rotation)
		{
			Entity = e;

			m_position = position;
			m_rotation = rotation;
		}

		//Call this when this Transform component changes.
		public event Action<Entity> OnTransformComponentUpdate;

		Vector3D m_position;
		public Vector3D Position
		{
			get { return m_position; }
			set
			{
				if (value == m_position)
					return;

				m_position = value;
				OnTransformComponentUpdate(Entity);
			}
		}

		//TODO: Implement our own Quaternion struct instead of using the Unity one.
		Quaternion m_rotation;
		public Quaternion Rotation
		{
			get { return m_rotation; }
			set
			{
				if (value == m_rotation)
					return;

				m_rotation = value;
				OnTransformComponentUpdate(Entity);
			}
		}
	}
}