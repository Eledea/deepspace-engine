using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage all our Inventories in the Galaxy that we have loaded.
/// </summary>
public class InventoryManager : MonoBehaviour
{
	//TODO: Use this to keep track of all the instances of Inventory for the Entities we have in this Galaxy.

	List<Player> Players;
	List<Inventory> Inventories;

	/// <summary>
	/// Adds a Player to the InventoryManager.
	/// </summary>
	public void AddPlayerToManager(Player p)
	{
		if (Players == null)
			Players = new List<Player> ();

		Players.Add (p);
	}

	/// <summary>
	/// Removes an Inventory from the InventoryManager.
	/// </summary>
	public void RemovePlayerFromManager(Player p)
	{
		if (Players != null)
			Players.Remove (p);
	}

	/// <summary>
	/// Adds an Inventory to the InventoryManager.
	/// </summary>
	public void AddInventoryToManager(Inventory inv)
	{
		if (Inventories == null)
			Inventories = new List<Inventory> ();

		Inventories.Add (inv);
	}

	/// <summary>
	/// Removes an Inventory from the InventoryManager.
	/// </summary>
	public void RemoveInventoryFromManager(Inventory inv)
	{
		if (Inventories != null)
			Inventories.Remove (inv);
	}
}