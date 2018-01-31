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

		players = new List<Player>();

		playersToID = new Dictionary<Player, long>();
		IDToPlayers = new Dictionary<long, Player>();
	}

	List<Player> players;

	Dictionary<Player, long> playersToID;
	Dictionary<long, Player> IDToPlayers;

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
	public void CreatePlayerInManager(SolarSystem mySolarSystem)
	{
		Debug.Log ("Created a new player!");

		Player p = new Player();
		p.Name = "Sam";
		p.Position = new Vector3D (100, 0, 98);
		p.Rotation = Quaternion.Euler (0, 0, 0);

		//TODO: Figure out a way to assign a Player an ID and consider if the Player class should be self aware.
		long id = (long)37331;

		AddPlayerToManager (p, id);

		WoodStack w = new WoodStack (p, 5);
		StoneStack s = new StoneStack (p, 11);

		p.AddItemStackAt (w, 0, 0);
		p.AddItemStackAt (s, 2, 3);
	}

	/// <summary>
	/// Adds a player to the PlayerManager.
	/// </summary>
	public void AddPlayerToManager(Player p, long id)
	{
		players.Add (p);

		playersToID [p] = id;
		IDToPlayers [id] = p;
	}

	/// <summary>
	/// Returns a Player from an id. 
	/// </summary>
	public Player GetPlayerInManager(long id)
	{
		return IDToPlayers [id];
	}

	/// <summary>
	/// Returns an ID from a Player. 
	/// </summary>
	public long GetPlayerID(Player p)
	{
		return playersToID [p];
	}

	/// <summary>
	/// Removes a player to the PlayerManager.
	/// </summary>
	public void RemovePlayerFromManager(Player p)
	{
		IDToPlayers.Remove (GetPlayerID(p));
		playersToID.Remove (p);

		players.Remove(p);
	}
}