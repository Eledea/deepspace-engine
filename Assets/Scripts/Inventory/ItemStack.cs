using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The ItemStack class defines a stack of Items.
/// </summary>
public class ItemStack
{
	/// <summary>
	/// The Items in this stack.
	/// </summary>
	public HashSet<Item> Items { get; set; }

	/// <summary>
	/// The type of this ItemStack.
	/// </summary>
	public IType Type { get; set; }

	/// <summary>
	/// Adds an item to this ItemStack.
	/// </summary>
	public void AddItemToStack(Item item)
	{
		if (Items == null)
			Items = new HashSet<Item> ();

		Items.Add (item);
	}

	/// <summary>
	/// Removes an item from this ItemStack.
	/// </summary>
	public void RemoveItemFromStack(Item item)
	{
		if (Items != null)
			Items.Remove (item);
	}
}