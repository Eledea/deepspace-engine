using System;
using UnityEngine;

namespace DeepSpace.InventorySystem
{
	// <summary>
	/// Manages Inventory operations for the Galaxy that we have loaded.
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
		public void SpawnNewItemStackAt(IType type, int numItems, Inventory inv, Vector2 index)
		{
			//TODO: Check to see if we exceed the number of items this stack can hold.

			var t = Type.GetType(string.Format("DeepSpace.InventorySystem.{0}", type.ToString()));
			ItemStack s = (ItemStack)Activator.CreateInstance(t, numItems, inv, index);

			inv.AddItemStackAt(s, index);
		}

		/// <summary>
		/// Moves an existing ItemStack from one index to another.
		/// </summary>
		public void MoveItemStackTo(Inventory inv, Vector2 index, Inventory newInv, Vector2 newIndex)
		{
			if (inv.IsItemStackAt(index) == false || (inv == newInv && index == newIndex))
				return;

			ItemStack s1 = inv.RemoveItemStackFrom(index);
			ItemStack s2 = newInv.RemoveItemStackFrom(newIndex);

			//TODO: Make Logic here work.
		}

		/// <summary>
		/// Splits an existing ItemStack into 2 seperate ItemStacks with n Items in.
		/// </summary>
		public void SplitItemStackAtTo(Inventory inv, Vector2 index, int n, Inventory newInv, Vector2 newIndex)
		{
			ItemStack s1 = inv.GetItemStackAt(index);
			ItemStack s2 = inv.GetItemStackAt(index);

			//TODO: Make Logic here work.
		}

		/// <summary>
		/// Determines if we can move Items from Itemstack s1 to s2.
		/// </summary>
		bool CanMoveToItemStack(ItemStack s1, ItemStack s2)
		{
			return s1.Type == s2.Type;
		}

		/// <summary>
		/// Determines if the ItemStacks are mergable or not.
		/// </summary>
		bool CanMergeItemStacks(ItemStack s1, ItemStack s2)
		{
			return s1.Type == s2.Type && s2.ItemAddability >= s1.NumItems;
		}

		/// <summary>
		/// Updates the ItemStack graphics for all players who are using the Inventory system in a SolarSystem.
		/// </summary>
		public void UpdateItemStackGraphicsForPlayersInSolarSystem(SolarSystem ss)
		{
			foreach (Player p in ss.PlayersInSystem)
			{
				//InventoryController instances will not be networked. Therefore, we have to use our data class to call the
				//function to update the Inventories currently being managed.

				UpdateItemStackGraphicsForCharacter(p.Character);
			}
		}

		/// <summary>
		/// Updates the ItemStack graphics for a Character.
		/// </summary>
		public void UpdateItemStackGraphicsForCharacter(Character c)
		{
			if (c.IsUsingInventorySystem)
				c.overlayController.OnInventoryUpdate();
		}
	}
}