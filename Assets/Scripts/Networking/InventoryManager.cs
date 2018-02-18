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

		public Stack OnItemStackCreated(IType type, int numItems)
		{
			//TODO: Implement new Item system.

			var t = Type.GetType(string.Format("DeepSpace.{0}", type.ToString()));
			Stack s = Activator.CreateInstance(t, numItems) as Stack;

			return s;
		}

		public void OnItemStackMoved(MyEntityInventoryComponent inv, Vector2I index, MyEntityInventoryComponent newInv, Vector2I newIndex)
		{
			if (inv.IsItemStackAt(index) == false || (inv == newInv && index == newIndex))
				return;

			Stack s1 = inv.RemoveItemStackFrom(index);
			Stack s2 = newInv.RemoveItemStackFrom(newIndex);

			if (CanMergeItemStacks(s1, s2, 1f))
				MoveItemsToStack(s2, s1, s2.NumItems);
			else if (CanMoveToItemStack(s1, s2))
				MoveItemsToStack(s2, s1, s1.ItemAddability);

			if (s1 != null && s1.NumItems != 0)
				newInv.AddItemStackAt(s1, newIndex);
			if (s2 != null && s2.NumItems != 0)
				inv.AddItemStackAt(s2, index);
		}

		public void OnItemStackSplit(MyEntityInventoryComponent inv, Vector2I index, float percentage, MyEntityInventoryComponent newInv, Vector2I newIndex)
		{
			Stack s1 = inv.GetItemStackAt(index);
			Stack s2 = newInv.GetItemStackAt(newIndex);

			if (s1.NumItems == 1)
			{
				OnItemStackMoved(inv, index, newInv, newIndex);
				return;
			}

			if (s2 == null)
				s2 = OnItemStackCreated(s1.Type, 0);

			if (CanMoveToItemStack(s1, s2))
				MoveItemsToStack(s1, s2, Mathf.FloorToInt(s1.NumItems * percentage));

			if (s1 != null && s1.NumItems != 0)
				inv.AddItemStackAt(s1, index);
			if (s2 != null && s2.NumItems != 0)
				newInv.AddItemStackAt(s2, newIndex);
		}

		void MoveItemsToStack(Stack s1, Stack s2, int n)
		{
			s1.RemoveItems(n);
			s2.AddItems(n);
		}

		bool CanMergeItemStacks(Stack s1, Stack s2, float percentage)
		{
			if (s1 == null || s2 == null)
				return false;

			return s1.Type == s2.Type && s2.ItemAddability >= (s1.NumItems * percentage);
		}

		bool CanMoveToItemStack(Stack s1, Stack s2)
		{
			if (s1 == null || s2 == null)
				return false;

			return s1.Type == s2.Type;
		}

		public void OnEntityInventoryComponentUpdated(Entity e)
		{
			//TODO: Avoid pointer chasing here. Use C# job system?

			foreach (Player p in e.SolarSystem.PlayersInSystem)
			{
				if (p.Character.IsUsingInventorySystem)
					p.Character.Controllers.OverlayController.OnInventoryUpdate();
			}
		}
	}
}