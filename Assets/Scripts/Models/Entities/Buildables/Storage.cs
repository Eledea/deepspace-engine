using DeepSpace.Core;
using DeepSpace.InventorySystem;
using UnityEngine;

/// <summary>
/// The Storage class defines a Storage Entity in a SolarSystem.
/// </summary>
public class Storage : Buildable
{
	public Storage(string name, long id, Vector3D position, Quaternion rotation)
	{
		Name = name;
		EntityId = id;
		Position = position;
		Rotation = rotation;

		InventoryManager.Instance.AddInventoryToEntity(this, 8, 4);
	}
}