using System.Collections.Generic;
using UnityEngine;

namespace DeepSpace.InventorySystem
{
	// <summary>
	/// Manages the Inventories for the Galaxy that we have loaded.
	/// </summary>
	public class InventoryManager : MonoBehaviour
	{
		public static InventoryManager Instance { get; protected set; }

		void OnEnable()
		{
			Instance = this;

			inventoryToGameObject = new Dictionary<Inventory, GameObject>();
			gameObjectToInventory = new Dictionary<GameObject, Inventory>();
		}

		Dictionary<Inventory, GameObject> inventoryToGameObject;
		Dictionary<GameObject, Inventory> gameObjectToInventory;

		/// <summary>
		/// Adds an Inventory and it's respective GameObject to the InventoryManager.
		/// </summary>
		public void AddInventoryToManager(Inventory inv, GameObject go)
		{
			inventoryToGameObject [inv] = go;
			gameObjectToInventory [go] = inv;
		}

		/// <summary>
		/// Removes an Inventory and it's respective GameObject from the InventoryManager.
		/// </summary>
		public void RemoveInventoryFromManager(Inventory inv)
		{
			GameObject myGO = inventoryToGameObject [inv];
			gameObjectToInventory.Remove (myGO);

			inventoryToGameObject.Remove (inv);
		}

		/// <summary>
		/// Removes a GameObject and it's respective Inventory from the InventoryManager.
		/// </summary>
		public void RemoveGameObjectFromManager(GameObject go)
		{
			Inventory myInv = gameObjectToInventory [go];
			inventoryToGameObject.Remove (myInv);

			gameObjectToInventory.Remove (go);
		}

		/// <summary>
		/// Determines whether this GameObject has an Inventory paired with it.
		/// </summary>
		public bool IsInventoryAttachedTo(GameObject go)
		{
			if (gameObjectToInventory [go] != null)
				return true;
			else
				return false;
		}

		/// <summary>
		/// Determines whether this Inventory has a GameObject paired with it.
		/// </summary>
		public bool IsGameObjectAttachedTo(Inventory inv)
		{
			if (inventoryToGameObject [inv] != null)
				return true;
			else
				return false;
		}

		/// <summary>
		/// Gets the Inventory this GameObject is paired with.
		/// </summary>
		public Inventory GetInventoryAttachedTo(GameObject go)
		{
			return gameObjectToInventory [go];
		}

		/// <summary>
		/// Gets the Inventory this GameObject is paired with.
		/// </summary>
		public GameObject GetInventoryAttachedTo(Inventory inv)
		{
			return inventoryToGameObject [inv];
		}

		/// <summary>
		/// Updates the ItemStack graphics for players who are using the Inventory system.
		/// </summary>
		public void UpdateItemStackGraphicsForPlayers()
		{
			for (int i = 0; i < PlayerManager.Instance.PlayerCount; i++)
			{
				//InventoryController instances will not be networked. Therefore, we have to use our data class to call the function to update Inventories.

				Player myPlayer = PlayerManager.Instance.GetPlayerInManager (i);

				if (myPlayer.IsUsingInventorySystem)
					myPlayer.InventoryUpdateFlag = true;
			}
		}
	}
}