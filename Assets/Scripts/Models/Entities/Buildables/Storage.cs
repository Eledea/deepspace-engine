using DeepSpace.Core;
using DeepSpace.InventorySystem;
using UnityEngine;

/// <summary>
/// The Storage class defines a Storage Entity in a SolarSystem.
/// </summary>
public class Storage : Buildable
{
	public Storage()
	{
		Inventory = new Inventory(this, 4, 4);
	}
}