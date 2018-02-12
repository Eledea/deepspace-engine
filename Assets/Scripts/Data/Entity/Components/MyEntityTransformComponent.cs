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

		public MyEntityTransformComponent(Entity e, Vector3D velocity, Vector3D position, Quaternion rotation)
		{
			Entity = e;

			m_velocity = velocity;
			m_position = position;
			m_rotation = rotation;
		}

		public event Action<Entity> OnTransformComponentUpdate;

		Vector3D m_velocity;
		Vector3D m_position;

		//TODO: Implement our own Quaternion struct instead of using the Unity one.

		Quaternion m_rotation;

		public Vector3D Velocity
		{
			get { return m_velocity; }
			set
			{
				m_velocity = value;
				Position += m_velocity * Time.deltaTime;
			}
		}

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