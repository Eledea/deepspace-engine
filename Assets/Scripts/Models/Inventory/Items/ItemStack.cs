﻿using System.Collections.Generic;
using UnityEngine;

namespace DeepSpace.InventorySystem
{
	/// <summary>
	/// The ItemStack class defines a stack of Items.
	/// </summary>
	public class ItemStack
	{
		/// <summary>
		/// The Inventory this ItemStack is located.
		/// </summary>
		public Inventory Inv { get; set; }

		/// <summary>
		/// The x index of this ItemStack in it's array.
		/// </summary>
		public int Inv_x;

		/// <summary>
		/// The y index of this ItemStack in it's array.
		/// </summary>
		public int Inv_y;

		/// <summary>
		/// The number of items currently held by this ItemStack.
		/// </summary>
		public int StackedItems { get; set; }

		/// <summary>
		/// The maximum number of items that this ItemStack can hold.
		/// </summary>
		public int MaxItems { get; set; }

		/// <summary>
		/// The type of this ItemStack.
		/// </summary>
		public IType IType { get; set; }

		/// <summary>
		/// Gets the index of this ItemStack in it's Inventory.
		/// </summary>
		public Vector2 InventoryIndex
		{
			get
			{
				return new Vector2 (Inv_x, Inv_y);
			}
		}

		/// <summary>
		/// Gets the number of Items in this ItemStack.
		/// </summary>
		public string ItemCount
		{
			get
			{
				return StackedItems.ToString ();
			}
		}

		/// <summary>
		/// Returns the Type name of this ItemStack.
		/// </summary>
		public string ITypeName
		{
			get
			{
				return IType.ToString ();
			}
		}

		/// <summary>
		/// Returns the Type integer of this ItemStack.
		/// </summary>
		public int ITypeInt
		{
			get
			{
				return (int)IType;
			}
		}

		/// <summary>
		/// Adds an item to this ItemStack.
		/// </summary>
		public void AddItemsToStack(int n)
		{
			StackedItems += n;
		}

		/// <summary>
		/// Removes an item from this ItemStack.
		/// </summary>
		public void RemoveItemsFromStack(int n)
		{
			StackedItems -= n;
		}
	}
}