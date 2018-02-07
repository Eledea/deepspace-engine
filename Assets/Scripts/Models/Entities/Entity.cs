using DeepSpace.Core;
using DeepSpace.InventorySystem;
using UnityEngine;

/// <summary>
/// The Entity class defines an Entity in a SolarSystem.
/// </summary>
public class Entity
{
	public string Name;
	public long EntityId;
	public SolarSystem SolarSystem;

	public Inventory Inventory;

	Vector3D m_velocity;

	/// <summary>
	/// Gets or sets the Position of this Entity in it's SolarSystem.
	/// </summary>
	public Vector3D Velocity
	{
		get { return m_velocity; }
		set
		{
			m_velocity = value;
			Position += m_velocity * Time.deltaTime;
		}
	}

	Vector3D m_position;

	/// <summary>
	/// Gets or sets the Position of this Entity in it's SolarSystem.
	/// </summary>
	public Vector3D Position
	{
		get { return m_position; }
		set
		{
			if (value == m_position)
				return;

			m_position = value;
			PlayerManager.Instance.UpdateEntityForPlayersInSystem(this, SolarSystem);
		}
	}

	Quaternion m_rotation;

	/// <summary>
	/// Gets or sets the Rotation of this Entity in it's SolarSystem.
	/// </summary>
	public Quaternion Rotation
	{
		get { return m_rotation; }
		set
		{
			m_rotation = value;
			PlayerManager.Instance.UpdateEntityForPlayersInSystem(this, SolarSystem);
		}
	}

	/// <summary>
	/// Determines if this Entity has an Inventory or not.
	/// </summary>
	public bool HasInventory
	{
		get { return Inventory != null; }
	}
}