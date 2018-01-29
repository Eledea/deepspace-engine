using System.Collections.Generic;
using UnityEngine;

namespace DeepSpace.InventorySystem
{
	// <summary>
	/// Manages the myInventories for the Galaxy that we have loaded.
	/// </summary>
	public class InventoryManager : MonoBehaviour
	{
		public static InventoryManager Instance { get; protected set; }

		void OnEnable()
		{
			Instance = this;

			InventoryToEntity = new Dictionary<Inventory, Entity>();
			entityToInventory = new Dictionary<Entity, Inventory>();
		}

		Dictionary<Inventory, Entity> InventoryToEntity;
		Dictionary<Entity, Inventory> entityToInventory;

		/// <summary>
		/// Adds an Inventory and it's respective Entity to the InventoryManager.
		/// </summary>
		public void AddInventoryToManager(Inventory myInv, Entity myEntity)
		{
			InventoryToEntity [myInv] = myEntity;
			entityToInventory [myEntity] = myInv;
		}

		/// <summary>
		/// Removes an Inventory and it's respective Entity from the InventoryManager.
		/// </summary>
		public void RemoveInventoryFromManager(Inventory myInv)
		{
			Entity myEntity = InventoryToEntity [myInv];
			entityToInventory.Remove (myEntity);

			InventoryToEntity.Remove (myInv);
		}

		/// <summary>
		/// Removes a Entity and it's respective Inventory from the InventoryManager.
		/// </summary>
		public void RemoveInventoryFromManager(Entity myEntity)
		{
			Inventory mymyInv = entityToInventory [myEntity];
			InventoryToEntity.Remove (mymyInv);

			entityToInventory.Remove (myEntity);
		}

		/// <summary>
		/// Determines whether this Entity has an Inventory paired with it.
		/// </summary>
		public bool IsInventoryAttachedTo(Entity myEntity)
		{
			if (entityToInventory [myEntity] != null)
				return true;
			else
				return false;
		}

		/// <summary>
		/// Determines whether this Inventory has an Entity paired with it.
		/// </summary>
		public bool IsEntityAttachedTo(Inventory myInv)
		{
			if (InventoryToEntity [myInv] != null)
				return true;
			else
				return false;
		}

		/// <summary>
		/// Gets the Inventory this Entity is paired with.
		/// </summary>
		public Inventory GetInventoryAttachedTo(Entity myEntity)
		{
			return entityToInventory [myEntity];
		}

		/// <summary>
		/// Gets the Entity this Inventory is paired with.
		/// </summary>
		public Entity GetInventoryAttachedTo(Inventory myInv)
		{
			return InventoryToEntity [myInv];
		}

		/// <summary>
		/// Updates the ItemStack graphics for players who are using the Inventory system.
		/// </summary>
		public void UpdateItemStackGraphicsForPlayers()
		{
			for (int i = 0; i < PlayerManager.Instance.PlayerCount; i++)
			{
				//InventoryController instances will not be networked. Therefore, we have to use our data class to call the function to update myInventories.

				Player myPlayer = PlayerManager.Instance.GetPlayerInManager (i);

				if (myPlayer.IsUsingInventorySystem)
					myPlayer.InventoryUpdateFlag = true;
			}
		}
	}
}