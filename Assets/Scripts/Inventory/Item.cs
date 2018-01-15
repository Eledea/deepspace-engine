/// <summary>
/// The IType enum describes the corresponding integers for an Item's name.
/// </summary>
public enum IType
{
	Wood
}

/// <summary>
/// The Item class defines an Item.
/// </summary>
public class Item
{
	/// <summary>
	/// The type of this Item.
	/// </summary>
	public IType Type { get; set; }

	/// <summary>
	/// The ItemStack this Item belongs to.
	/// </summary>
	public ItemStack Stack { get; set; }

	/// <summary>
	/// Move this Item to a new ItemStack.
	/// </summary>
	public void MoveToStack(ItemStack s)
	{
		if (Stack != null)
			Stack.RemoveItemFromStack (this);

		s.AddItemToStack (this);
		Stack = s;
	}
}