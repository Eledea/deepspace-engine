﻿using DeepSpace.Controllers;
using DeepSpace.Core;
using DeepSpace.Characters;	
using DeepSpace.InventorySystem;
using DeepSpace.World;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace DeepSpace.Networking
{
	// <summary>
	/// Manages the Players for the Galaxy that we have loaded.
	/// </summary>
	public class PlayerManager : NetworkBehaviour
	{
		public static PlayerManager Instance { get; protected set; }

		void OnEnable()
		{
			Instance = this;

			m_players = new List<Player>();
		}

		List<Player> m_players;

		/// <summary>
		/// Returns all the Players being managed as an array.
		/// </summary>
		public Player[] GetPlayersInManager
		{
			get
			{
				return m_players.ToArray();
			}
		}

		/// <summary>
		/// Returns the number of Players being mananged.
		/// </summary>
		public int PlayerCount
		{
			get
			{
				return m_players.Count;
			}
		}

		/// <summary>
		/// Creates a Player and adds it to the PlayerManager.
		/// </summary>
		public void OnNewPlayerConnect(PlayerConnection c, string name, SolarSystem ss)
		{
			c.gameObject.name = name;
			SolarSystemView view = c.gameObject.GetComponent<SolarSystemView>();

			Player p = new Player(name, 37331, new Vector3D(10, 0, 8), Quaternion.identity, view);
			AddPlayerToManager(p, ss);

			p.Character.Inventory.AddItemStackAt(InventoryManager.Instance.SpawnNewItemStack(IType.Wood, Random.Range(1, 51)), new Vector2I(0, 2));
			p.Character.Inventory.AddItemStackAt(InventoryManager.Instance.SpawnNewItemStack(IType.Stone, Random.Range(1, 51)), new Vector2I(3, 1));

			view.OnSolarSystemChange();
		}

		/// <summary>
		/// Adds a player to the PlayerManager.
		/// </summary>
		public void AddPlayerToManager(Player p, SolarSystem ss)
		{
			m_players.Add(p);

			MovePlayerToSolarSystem(p, ss);
		}

		/// <summary>
		/// Updates the local Entity representation for all Players in a SolarSystem.
		/// </summary>
		public void UpdateEntityForPlayersInSystem(Entity e, SolarSystem ss)
		{
			if (ss == null)
				return;

			//Debug.Log(string.Format("Updated an Entity with name: {0}", e.Name))

			foreach (Player p in ss.PlayersInSystem)
				p.View.UpdateGameObjectForEntity(e);
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
			m_players.Remove(p);
		}
	}
}