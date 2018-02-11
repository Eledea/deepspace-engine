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
		public MyEntityTransformComponent(Vector3D velocity, Vector3D position, Quaternion rotation)
		{
			Velocity = velocity;
			Position = position;
			Rotation = rotation;
		}

		//TODO: Introduce a callback system to PlayerManager to maintain component seperation from Networking.

		public event Action<Entity> OnTransformComponentUpdate;

		Vector3D m_velocity;
		Vector3D m_position;

		//TODO: Consider using a different Quaternion class built from scratch to allow better
		//optimisation and more centralisation.

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
			}
		}
	}
}