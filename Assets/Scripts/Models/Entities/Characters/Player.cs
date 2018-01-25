﻿using DeepSpace.InventorySystem;
using UnityEngine;

/// <summary>
/// Independent data class for storing a Player.
/// </summary>
public class Player : Inventory
{
	public Player()
	{
		Inv = new ItemStack [4,4];
	}

	/// <summary>
	/// The Health of this Player (out of 100).
	/// </summary>
	public float Health { get; set; }

	/// <summary>
	/// The Oxygen level of this Player (out of 100).
	/// </summary>
	public float Oxygen { get; set; }

	/// <summary>
	/// Returns a value indicating whether this Player is using the Inventory system.
	/// </summary>
	public bool IsUsingInventorySystem { get; set; }

	/// <summary>
	/// Specifies whether or not this Player should update it's Inventory view.
	/// </summary>
	public bool InventoryUpdateFlag { get; set; }
}