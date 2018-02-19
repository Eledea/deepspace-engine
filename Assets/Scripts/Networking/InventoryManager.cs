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
		//TOOD: Populate this from the Inspector using a script.
		public static MyItemDefinitionId[] ItemDefinitions =
		{
			new MyItemDefinitionId("Wood", 0, 50),
			new MyItemDefinitionId("Stone", 1, 50),
		};

		public static Stack OnItemStackCreated(MyItemDefinitionId? definition, int numItems)
		{
			if (definition.HasValue == false)
				throw new NullReferenceException();

			var s = new Stack((MyItemDefinitionId)definition, numItems) as Stack;
			return s;
		}

		public static Stack OnItemStackCreated(int definitionId, int numItems)
		{
			var definition = ItemDefinitions[definitionId];
			return OnItemStackCreated(definition, numItems);
		}

		public static void OnItemStackMoved(MyEntityInventoryComponent inv, Vector2I index, MyEntityInventoryComponent newInv, Vector2I newIndex)
		{
			if (inv.IsItemStackAt(index) == false || (inv == newInv && index == newIndex))
				return;

			var s1 = inv.RemoveItemStackFrom(index);
			var s2 = newInv.RemoveItemStackFrom(newIndex);

			if (CanMergeItemStacks(s1, s2, 1F))
				MoveItemsToStack(s2, s1, s2.NumItems);
			else if (CanMoveToItemStack(s1, s2))
				MoveItemsToStack(s2, s1, s1.ItemAddability);

			if (s1 != null && s1.NumItems != 0)
				newInv.AddItemStackAt(s1, newIndex);
			if (s2 != null && s2.NumItems != 0)
				inv.AddItemStackAt(s2, index);
		}

		public static void OnItemStackSplit(MyEntityInventoryComponent inv, Vector2I index, float percentage, MyEntityInventoryComponent newInv, Vector2I newIndex)
		{
			var s1 = inv.GetItemStackAt(index);
			var s2 = newInv.GetItemStackAt(newIndex);

			if (s1.NumItems == 1)
			{
				OnItemStackMoved(inv, index, newInv, newIndex);
				return;
			}

			if (s2 == null)
				s2 = OnItemStackCreated(s1.DefinitionId.Id, 0);

			if (CanMoveToItemStack(s1, s2))
				MoveItemsToStack(s1, s2, Mathf.FloorToInt(s1.NumItems * percentage));

			if (s1 != null && s1.NumItems != 0)
				inv.AddItemStackAt(s1, index);
			if (s2 != null && s2.NumItems != 0)
				newInv.AddItemStackAt(s2, newIndex);
		}

		private static void MoveItemsToStack(Stack s1, Stack s2, int n)
		{
			s1.RemoveItems(n);
			s2.AddItems(n);
		}

		private static bool CanMergeItemStacks(Stack s1, Stack s2, float percentage)
		{
			return CanMoveToItemStack(s1, s2) && s2.ItemAddability >= (s1.NumItems * percentage);
		}

		private static bool CanMoveToItemStack(Stack s1, Stack s2)
		{
			if (s1 == null || s2 == null)
				return false;

			return s1.DefinitionId.Id == s2.DefinitionId.Id;
		}
	}
}