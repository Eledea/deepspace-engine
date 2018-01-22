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
	public List<Item> Items { get; set; }

	/// <summary>
	/// The type of this ItemStack.
	/// </summary>
	public IType IType { get; set; }

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
	public void AddItemToStack(Item item)
	{
		if (Items == null)
			Items = new List<Item> ();

		//Check to see if this ItemStack is the same IType as the Item we are trying to add.
		if (this.IType == item.Type)
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