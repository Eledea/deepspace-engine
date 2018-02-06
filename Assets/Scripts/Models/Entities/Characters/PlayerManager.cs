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

	public GameObject GlobalPlayer;

	void OnEnable()
	{
		Instance = this;

		players = new List<Player>();
	}

	List<Player> players;

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
	public void CreatePlayer(string name, SolarSystem ss)
	{
		GameObject global = Instantiate(GlobalPlayer);
		global.name = name;

		Player p = new Player(name, 37331, new Vector3D(0, 0, -2), Quaternion.identity);
		AddPlayerToManager(p, ss);

		SolarSystemView myView = global.GetComponent<SolarSystemView>();
		myView.Player = p;
		p.solarSystemView = myView;

		myView.OnSolarSystemChange();

		InventoryManager.Instance.SpawnNewItemStackAt(IType.Wood, Random.Range(1, 51), p.Inventory, 0, 2);
		InventoryManager.Instance.SpawnNewItemStackAt(IType.Stone, Random.Range(1, 51), p.Inventory, 3, 1);
	}

	/// <summary>
	/// Adds a player to the PlayerManager.
	/// </summary>
	public void AddPlayerToManager(Player p, SolarSystem ss)
	{
		players.Add(p);

		MovePlayerToSolarSystem(p, ss);
	}

	/// <summary>
	/// Updates the local Entity representation for all Players in a SolarSystem.
	/// </summary>
	public void UpdateEntitiesForPlayersInSystem(SolarSystem ss)
	{
		if (ss == null)
			return;

		foreach(Player p in ss.PlayersInSystem)
			UpdateEntitiesForPlayer(p);
	}

	/// <summary>
	/// Updates the local Entity representation for a Player.
	/// </summary>
	public void UpdateEntitiesForPlayer(Player p)
	{
		p.solarSystemView.UpdateAllEntities();
	}

	/// <summary>
	/// Moves a Player to a new SolarSystem.
	/// </summary>
	public void MovePlayerToSolarSystem(Player p, SolarSystem ss)
	{
		ss.AddPlayerToSolarSystem(p);
	}

	/// <summary>
	/// Removes a player to the PlayerManager.
	/// </summary>
	public void RemovePlayerFromManager(Player p)
	{
		players.Remove(p);
	}
}