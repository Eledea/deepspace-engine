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
	public Vector3D Position;

	public Vector3D Velocity;
	public Quaternion Rotation;

	public Inventory Inventory;

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