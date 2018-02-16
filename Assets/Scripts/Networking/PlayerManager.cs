using DeepSpace.Controllers;
using DeepSpace.Core;
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

		public Player[] GetPlayersInManager
		{
			get
			{
				return m_players.ToArray();
			}
		}

		public int PlayerCount
		{
			get
			{
				return m_players.Count;
			}
		}

		public void OnNewPlayerConnect(PlayerConnection c, string name, SolarSystem ss)
		{
			c.gameObject.name = name;
			SolarSystemView view = c.gameObject.GetComponent<SolarSystemView>();

			Player p = new Player(name, 37331, new Vector3D(10, 0, 8), Quaternion.identity, view);

			m_players.Add(p);
			OnPlayerSolarSystemChanged(p, ss);

			view.OnLocalCharacterSpawned();

			p.Character.Inventory.AddItemStackAt(InventoryManager.Instance.OnItemStackCreated(IType.Wood, Random.Range(1, 51)), new Vector2I(0, 2));
			p.Character.Inventory.AddItemStackAt(InventoryManager.Instance.OnItemStackCreated(IType.Stone, Random.Range(1, 51)), new Vector2I(3, 1));
		}

		public void OnEntityTransformComponentUpdate(Entity e)
		{
			if (e.SolarSystem == null)
				return;

			//Debug.Log(string.Format("Updated an Entity with name: {0}", e.Name))

			foreach (Player p in e.SolarSystem.PlayersInSystem)
				p.View.UpdateGameObjectForEntity(e);
		}

		public void OnPlayerSolarSystemChanged(Player p, SolarSystem ss)
		{
			ss.AddPlayerToSolarSystem(p);
		}
	}
}