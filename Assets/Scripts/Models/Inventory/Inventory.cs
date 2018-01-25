﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepSpace.InventorySystem
{
	/// <summary>
	/// The Inventory class defines an Inventory of an Entity.
	/// </summary>
	public class Inventory : Entity
	{
		//What is the best way of storing the data for Items in an Inventory???
		//We have a few options...

		//The data type that makes the most sense in this situation in a 2D array
		//for that columns and rows that we will have on the interface.

		//But HOW will the game know what each slot has???
		//We COULD use integers to store data and have each interger correspond to an
		//item type in an enum....but how will we know how much of each item we will have???

		//A 2D int array in this situation is no good, because we are NOT able to know
		//how many of an item we have in each slot.

		/// <summary>
		/// The Inventory of this Inventory.
		/// </summary>
		protected ItemStack[,] Inv { get; set; }

		/// <summary>
		/// Returns the length of the x axis of this Inventory.
		/// </summary>
		public int InvSize_x
		{
			get
			{
				return Inv.GetLength (0);
			}
		}

		/// <summary>
		/// Returns the length of the y axis of this Inventory.
		/// </summary>
		public int InvSize_y
		{
			get
			{
				return Inv.GetLength (1);
			}
		}

		/// <summary>
		/// Adds an ItemStack at this array position.
		/// </summary>
		public void AddItemStackAt(ItemStack s, int x, int y)
		{
			if (Inv [x, y] == null)
				Inv [x, y] = s;

			s.Inv_x = x;
			s.Inv_y = y;
		}

		/// <summary>
		/// Removes an ItemStack from this array position.
		/// </summary>
		public void RemoveItemStackFrom(int x, int y)
		{
			Inv [x, y] = null;
		}

		/// <summary>
		/// Determines whether this instance contains an ItemStack at the specified array index.
		/// </summary>
		public bool IsItemStackAt(int x, int y)
		{
			if (GetItemStackAt (x, y) != null)
				return true;
			else
				return false;
		}

		/// <summary>
		/// Gets an ItemStack at this array position.
		/// </summary>
		public ItemStack GetItemStackAt(int x, int y)
		{
			return Inv [x, y];
		}

		/// <summary>
		/// Moves an ItemStack to another Inventory slot.
		/// </summary>
		public void MoveItemStackTo(int x, int y, Inventory inventory, int a, int b)
		{
			if (Inv [x, y] != null)
			{
				inventory.AddItemStackAt (GetItemStackAt (x, y), a, b);
				this.RemoveItemStackFrom (x, y);
			}
		}
	}
}