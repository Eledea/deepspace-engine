using DeepSpace.Core;
using DeepSpace.Networking;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// The Entity class defines an Entity in a SolarSystem.
	/// </summary>
	public class Entity
	{
		public string Name;
		public long EntityId;

		public SolarSystem SolarSystem;

		Vector3D m_velocity;
		Vector3D m_position;
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

				//TODO: Make this update client data with an action system instead.

				m_position = value;
				PlayerManager.Instance.UpdateEntityForPlayersInSystem(this, SolarSystem);
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
				PlayerManager.Instance.UpdateEntityForPlayersInSystem(this, SolarSystem);
			}
		}

		MyInventoryComponent m_inventoryComponent;

		public bool HasInventory
		{
			get { return Inventory != null; }
		}

		public MyInventoryComponent Inventory
		{
			get
			{
				return m_inventoryComponent;
			}

			set
			{
				m_inventoryComponent = value;
			}
		}
	}
}