using System;
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
		}

		/// <summary>
		/// Adds an Inventory to an Entity.
		/// </summary>
		public void AddInventoryToEntity(Entity e, int x, int y)
		{
			Inventory inv = new Inventory(e, x, y);
			e.Inventory = inv;
		}

		/// <summary>
		/// Removes an Inventory from an Entity.
		/// </summary>
		public void RemoveInventoryFromEntity(Entity e)
		{
			e.Inventory = null;
		}

		/// <summary>
		/// Spawns a new ItemStack for an IType.
		/// </summary>
		public void SpawnNewItemStackAt(IType type, int numItems, Inventory inv, int x, int y)
		{
			//TODO: Check to see if we exceed the number of items this stack can hold.

			var t = Type.GetType(string.Format("DeepSpace.InventorySystem.{0}", type.ToString()));
			ItemStack s = (ItemStack)Activator.CreateInstance(t, numItems, inv, x, y);

			inv.AddItemStackAt(s, x, y);
		}

		/// <summary>
		/// Moves n Items from one ItemStack to another.
		/// </summary>
		public void MoveItemsToStack(ItemStack s1, ItemStack s2, int n)
		{
			if (s1.Type == s2.Type)
			{
				s1.RemoveItems(n);
				s2.AddItems(n);
			}
			else
			{
				Debug.LogError("ERROR: Cannot move Items between two stacks with different Item Ids.");
			}
		}

		/// <summary>
		/// Updates the ItemStack graphics for players who are using the Inventory system.
		/// </summary>
		public void UpdateItemStackGraphicsForPlayers()
		{
			foreach (Player p in PlayerManager.Instance.GetPlayersInManager)
			{
				//InventoryController instances will not be networked. Therefore, we have to use our data class to call the
				//function to update the Inventories currently being managed.

				if (p.IsUsingInventorySystem)
					p.InventoryUpdateFlag = true;
			}
		}
	}
}