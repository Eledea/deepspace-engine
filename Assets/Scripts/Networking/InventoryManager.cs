using DeepSpace.Core;
using DeepSpace.Characters;
using DeepSpace.InventorySystem;
using DeepSpace.World;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace DeepSpace.Networking
{
	// <summary>
	/// Manages Inventory operations for the Galaxy that we have loaded.
	/// </summary>
	public class InventoryManager : NetworkBehaviour
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
		public ItemStack SpawnNewItemStack(IType type, int numItems)
		{
			//TODO: Check to see if we exceed the number of items this stack can hold.

			var t = Type.GetType(string.Format("DeepSpace.InventorySystem.{0}", type.ToString()));
			ItemStack s = (ItemStack)Activator.CreateInstance(t, numItems);

			return s;
		}

		/// <summary>
		/// Moves an existing ItemStack from one index to another.
		/// </summary>
		public void MoveItemStackTo(Inventory inv, Vector2I index, Inventory newInv, Vector2I newIndex)
		{
			if (inv.IsItemStackAt(index) == false || (inv == newInv && index == newIndex))
				return;

			ItemStack s1 = inv.RemoveItemStackFrom(index);
			ItemStack s2 = newInv.RemoveItemStackFrom(newIndex);

			if (CanMergeItemStacks(s1, s2, 1f))
				MoveItemsToStack(s2, s1, s2.NumItems);
			else if (CanMoveToItemStack(s1, s2))
				MoveItemsToStack(s2, s1, s1.ItemAddability);

			if (s1 != null && s1.NumItems != 0)
				newInv.AddItemStackAt(s1, newIndex);
			if (s2 != null && s2.NumItems != 0)
				inv.AddItemStackAt(s2, index);
		}

		/// <summary>
		/// Splits an existing ItemStack into 2 seperate ItemStacks with n Items in.
		/// </summary>
		public void SplitItemStackAtTo(Inventory inv, Vector2I index, float percentage, Inventory newInv, Vector2I newIndex)
		{
			ItemStack s1 = inv.GetItemStackAt(index);
			ItemStack s2 = newInv.GetItemStackAt(newIndex);

			if (s1.NumItems == 1)
			{
				MoveItemStackTo(inv, index, newInv, newIndex);
				return;
			}

			if (s2 == null)
				s2 = SpawnNewItemStack(s1.Type, 0);

			if (CanMoveToItemStack(s1, s2))
				MoveItemsToStack(s1, s2, Mathf.FloorToInt(s1.NumItems * percentage));

			if (s1 != null && s1.NumItems != 0)
				inv.AddItemStackAt(s1, index);
			if (s2 != null && s2.NumItems != 0)
				newInv.AddItemStackAt(s2, newIndex);
		}

		/// <summary>
		/// Moves n items from ItemStack s1 to s2.
		/// </summary>
		void MoveItemsToStack(ItemStack s1, ItemStack s2, int n)
		{
			s1.RemoveItems(n);
			s2.AddItems(n);
		}

		/// <summary>
		/// Determines if the ItemStacks are mergable or not.
		/// </summary>
		bool CanMergeItemStacks(ItemStack s1, ItemStack s2, float percentage)
		{
			if (s1 == null || s2 == null)
				return false;

			return s1.Type == s2.Type && s2.ItemAddability >= (s1.NumItems * percentage);
		}

		/// <summary>
		/// Determines if we can move Items from Itemstack s1 to s2.
		/// </summary>
		bool CanMoveToItemStack(ItemStack s1, ItemStack s2)
		{
			if (s1 == null || s2 == null)
				return false;

			return s1.Type == s2.Type;
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