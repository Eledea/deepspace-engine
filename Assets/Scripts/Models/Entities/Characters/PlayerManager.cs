using DeepSpace.Core;		
using DeepSpace.InventorySystem;
using System;
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

		players = new List<Player>();
		playerId = new Dictionary<long, Player>();
	}

	List<Player> players;
	Dictionary<long, Player> playerId;

	/// <summary>
	/// Returns all the Players being managed as an array.
	/// </summary>
	public Player[] GetPlayersInManager
	{
		get
		{
			return players.ToArray();
		}
	}

	/// <summary>
	/// Returns the number of Players being mananged.
	/// </summary>
	public int PlayerCount
	{
		get
		{
			return players.Count;
		}
	}

	/// <summary>
	/// Creates a Player to the InventoryManager.
	/// </summary>
	public void CreatePlayerInManager(SolarSystem ss)
	{
		Player p = new Player();
		p.Name = "Sam";
		p.EntityId = 37331;
		p.Position = new Vector3D(0, 0, -2);
		p.Rotation = Quaternion.Euler(0, 0, 0);

		AddPlayerToManager(p);
		MovePlayerToSolarSystem(p, ss);

		Wood w = new Wood(p.Inventory, 37);
		Stone s = new Stone(p.Inventory, 22);

		p.Inventory.AddItemStackAt(w, 1, 0);
		p.Inventory.AddItemStackAt(s, 2, 3);
	}

	/// <summary>
	/// Returns a Player from an id. 
	/// </summary>
	public Player GetPlayerWithId(long id)
	{
		return playerId [id];
	}

	/// <summary>
	/// Returns an ID from a Player. 
	/// </summary>
	public long PlayerId(Player p)
	{
		return p.EntityId;
	}

	/// <summary>
	/// Adds a player to the PlayerManager.
	/// </summary>
	public void AddPlayerToManager(Player p)
	{
		players.Add(p);
		playerId[p.EntityId] = p;
	}

	/// <summary>
	/// Moves a Player to a new SolarSystem.
	/// </summary>
	public void MovePlayerToSolarSystem(Player p, SolarSystem ss)
	{
		p.SolarSystem = ss;
		ss.AddEntityToSolarSystem(p);
	}

	/// <summary>
	/// Removes a player to the PlayerManager.
	/// </summary>
	public void RemovePlayerFromManager(Player p)
	{
		playerId.Remove (p.EntityId);
		players.Remove(p);
	}
}