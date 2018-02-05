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
	public Vector3D Velocity;

	Vector3D position;
	Quaternion rotation;

	public Inventory Inventory;

	/// <summary>
	/// Gets or sets the Position of this Player in it's SolarSystem.
	/// </summary>
	public Vector3D Position
	{
		get
		{
			return position;
		}

		set
		{
			position = value;
			PlayerManager.Instance.UpdateEntitiesForPlayersInSystem(SolarSystem);
		}
	}

	/// <summary>
	/// Gets or sets the Rotation of this Player in it's SolarSystem.
	/// </summary>
	public Quaternion Rotation
	{
		get
		{
			return rotation;
		}

		set
		{
			rotation = value;
			PlayerManager.Instance.UpdateEntitiesForPlayersInSystem(SolarSystem);
		}
	}

	/// <summary>
	/// Determines if this Entity has an Inventory or not.
	/// </summary>
	public bool HasInventory
	{
		get
		{
			return Inventory != null;
		}
	}

	/// <summary>
	/// Updates the Position for this Entity based on it's Velocity.
	/// </summary>
	public void UpdatePosition()
	{ 
		Position += Velocity * Time.deltaTime;
	}
}