using UnityEngine;

/// <summary>
/// The Wood class defines an item of Wood.
/// </summary>
public class WoodStack : ItemStack
{
	public WoodStack(Inventory inv, int n)
	{
		Inv = inv;

		IType = IType.Wood;
		MaxItems = 50;

		if (MaxItems >= n)
			StackedItems = n;
		else
			Debug.LogError ("ERROR: Can't create stack with more Items than it's limit!");
	}
}