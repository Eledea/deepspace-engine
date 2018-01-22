﻿using UnityEngine;

/// <summary>
/// The Wood class defines an item of Stone.
/// </summary>
public class StoneStack : ItemStack
{
	public StoneStack(int n)
	{
		IType = IType.Stone;
		MaxItems = 50;

		if (MaxItems >= n)
			StackedItems = n;
		else
			Debug.LogError ("ERROR: Can't create stack with more Items than it's limit!");
	}
}