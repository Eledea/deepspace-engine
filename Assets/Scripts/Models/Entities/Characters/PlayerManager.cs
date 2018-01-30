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

		playersToID = new Dictionary<Player, int>();
		IDToPlayers = new Dictionary<int, Player>();
	}

	Dictionary<Player, int> playersToID;
	Dictionary<int, Player> IDToPlayers;

	/// <summary>
	/// Returns the number of Players being mananged.
	/// </summary>
	public int PlayerCount
	{
		get
		{
			return playersToID.Count;
		}
	}

	/// <summary>
	/// Creates a Player to the InventoryManager.
	/// </summary>
	public void CreatePlayerInManager(SolarSystem mySolarSystem)
	{
		Debug.Log ("Created a new player!");

		Player p = new Player();
		p.DisplayName = "Sam";
		p.SolarSystem = mySolarSystem;
		p.Position = new Vector3D (100, 0, 98);
		p.Rotation = Quaternion.Euler (0, 0, 0);

		//TODO: Figure out a way to assign a Player an ID and consider if the Player class should be self aware.
		int id = 37331;

		AddPlayerToManager (p, id);

		WoodStack w = new WoodStack (p, 5);
		StoneStack s = new StoneStack (p, 11);

		p.AddItemStackAt (w, 0, 0);
		p.AddItemStackAt (s, 2, 3);
	}

	/// <summary>
	/// Adds the player to the PlayerManager.
	/// </summary>
	public void AddPlayerToManager(Player p, int id)
	{
		playersToID [p] = id;
		IDToPlayers [id] = p;
	}

	/// <summary>
	/// Returns a Player from an id. 
	/// </summary>
	public Player GetPlayerInManager(int id)
	{
		return IDToPlayers [id];
	}

	/// <summary>
	/// Returns an ID from a Player. 
	/// </summary>
	public int GetPlayerID(Player p)
	{
		return playersToID [p];
	}
}