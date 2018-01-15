using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Inventory class defines an Inventory.
/// </summary>
public class Inventory
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
	/// The Inventory of this Player.
	/// </summary>
	public ItemStack[,] Inventory_ { get; set; }

	/// <summary>
	/// Adds an ItemStack at this array position.
	/// </summary>
	public void AddItemStackAt(ItemStack s, int x, int y)
	{
		Inventory_ [x, y] = s;
	}

	/// <summary>
	/// Removes an ItemStack from this array position.
	/// </summary>
	public void RemoveItemStackFrom(int x, int y)
	{
		Inventory_ [x, y] = null;
	}

	/// <summary>
	/// Gets an ItemStack at this array position.
	/// </summary>
	public ItemStack GetItemStackAt(int x, int y)
	{
		return Inventory_ [x, y];
	}

	/// <summary>
	/// Moves an ItemStack to another Inventory slot.
	/// </summary>
	public void MoveItemStackTo(int x, int y, Inventory inventory, int a, int b)
	{
		if (Inventory_ [x, y] != null)
		{
			inventory.AddItemStackAt (GetItemStackAt (x, y), a, b);
			this.RemoveItemStackFrom (x, y);
		}
		else
		{
			Debug.LogError ("ERROR: No ItemStack at this array position!");
			return;
		}
	}

	/// <summary>
	/// Splits an ItemStack into 2 ItemStacks
	/// </summary>
	public ItemStack SplitStack(int x, int y)
	{
		ItemStack stackToSplit = GetItemStackAt (x, y);
		int numberPerStack = stackToSplit.Items.Count / 2;

		//TODO: Make this work with odd numbers.

		ItemStack newItemStack = new ItemStack ();
		for (int i = 0; i < numberPerStack; i++)
			stackToSplit.Items [i].MoveToStack (newItemStack);

		//Now remove the Items from our existing stack.
		for (int j = 0; j < numberPerStack; j++)
			stackToSplit.RemoveItemFromStack(stackToSplit.Items[j]);

		return newItemStack;
	}
}