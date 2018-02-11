using DeepSpace.Core;
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

		public ItemStack SpawnNewItemStack(IType type, int numItems)
		{
			//TODO: Implement new Item system.

			var t = Type.GetType(string.Format("DeepSpace.{0}", type.ToString()));
			ItemStack s = Activator.CreateInstance(t, numItems) as ItemStack;

			return s;
		}

		public void MoveItemStackTo(MyEntityInventoryComponent inv, Vector2I index, MyEntityInventoryComponent newInv, Vector2I newIndex)
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

		public void SplitItemStackAtTo(MyEntityInventoryComponent inv, Vector2I index, float percentage, MyEntityInventoryComponent newInv, Vector2I newIndex)
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

		void MoveItemsToStack(ItemStack s1, ItemStack s2, int n)
		{
			s1.RemoveItems(n);
			s2.AddItems(n);
		}

		bool CanMergeItemStacks(ItemStack s1, ItemStack s2, float percentage)
		{
			if (s1 == null || s2 == null)
				return false;

			return s1.Type == s2.Type && s2.ItemAddability >= (s1.NumItems * percentage);
		}

		bool CanMoveToItemStack(ItemStack s1, ItemStack s2)
		{
			if (s1 == null || s2 == null)
				return false;

			return s1.Type == s2.Type;
		}

		public void UpdateItemStackGraphicsForPlayersInSolarSystem(SolarSystem ss)
		{
			foreach (Player p in ss.PlayersInSystem)
			{
				//InventoryController instances will not be networked. Therefore, we have to use our data class to call the
				//function to update the Inventories currently being managed.

				UpdateItemStackGraphicsForCharacter(p.Character);
			}
		}

		public void UpdateItemStackGraphicsForCharacter(Character c)
		{
			if (c.IsUsingInventorySystem)
				c.m_overlayController.OnInventoryUpdate();
		}
	}
}