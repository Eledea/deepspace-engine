using DeepSpace.Controllers;
using DeepSpace.Core;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace DeepSpace.Networking
{
	// <summary>
	/// Manages the Players for the Galaxy that we have loaded.
	/// </summary>
	public class PlayerManager : NetworkBehaviour
	{
		public static void OnNewPlayerConnect(PlayerConnection c, string name, SolarSystem ss)
		{
			c.gameObject.name = name;
			var view = c.gameObject.GetComponent<SolarSystemView>() as SolarSystemView;

			long id = DateTime.Now.Ticks;
			var p = new Player(name, id, new Vector3D(10, 0, 8), Quaternion.identity, view);

			OnPlayerSolarSystemChanged(p, ss);

			view.OnLocalCharacterSpawned();

			p.Character.Inventory.AddItemStackAt(InventoryManager.OnItemStackCreated(new MyItemDefinitionId("Wood", 0, 50), UnityEngine.Random.Range(1, 51)), new Vector2I(0, 2));
			p.Character.Inventory.AddItemStackAt(InventoryManager.OnItemStackCreated(new MyItemDefinitionId("Stone", 1, 50), UnityEngine.Random.Range(1, 51)), new Vector2I(3, 1));
		}

		public static void OnPlayerSolarSystemChanged(Player p, SolarSystem ss)
		{
			ss.AddPlayerToSolarSystem(p);
		}

		public static void OnEntityTransformComponentUpdate(Entity e)
		{
			if (e.SolarSystem == null)
				return;

			//Debug.Log(string.Format("Updated an Entity with name: {0}", e.Name))

			foreach (Player p in e.SolarSystem.PlayersInSystem)
				p.View.UpdateGameObjectForEntity(e);
		}

		public static void OnEntityInventoryComponentUpdated(Entity e)
		{
			//TODO: We need a way to make this more efficient.
			//Explore possibilities of task parallelism, asynchronous execution and linear memory allocation.

			foreach (Player p in e.SolarSystem.PlayersInSystem)
			{
				if (p.Character.IsUsingInventorySystem)
					p.Character.Controllers.OverlayController.OnInventoryUpdate();
			}
		}
	}
}