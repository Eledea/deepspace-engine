using DeepSpace.Core;		
using DeepSpace.InventorySystem;
using System.Collections.Generic;
using UnityEngine;

// <summary>
/// Manages the Players for the Galaxy that we have loaded.
/// </summary>
public class PlayerManager : MonoBehaviour
{
	public static PlayerManager Instance { get; protected set; }

	void OnEnable()
	{
		Instance = this;

		Players = new List<Player>();
	}

	List<Player> Players;

	/// <summary>
	/// Returns the number of Players being mananged.
	/// </summary>
	public int PlayerCount
	{
		get
		{
			return Players.Count;
		}
	}

	/// <summary>
	/// Creates a Player to the InventoryManager.
	/// </summary>
	public void CreatePlayerInManager(SolarSystem mySolarSystem)
	{
		if (Players == null)
			Players = new List<Player> ();

		Player p = new Player();
		p.Name = "Sam";
		p.SolarSystem = mySolarSystem;
		p.Position = new Vector3D (100, 0, 98);
		p.Rotation = Quaternion.Euler (0, 0, 0);
		p.Health = 100;
		p.Oxygen = 100;

		Players.Add (p);

		WoodStack w = new WoodStack (p, 5);
		StoneStack s = new StoneStack (p, 11);

		p.AddItemStackAt (w, 0, 0);
		p.AddItemStackAt (s, 2, 3);
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
	/// Returns a Player at an array index. 
	/// </summary>
	public Player GetPlayerInManager(int i)
	{
		return Players[i];
	}
}