using DeepSpace.Core;
using DeepSpace.InventorySystem;
using System;
using UnityEngine;

/// <summary>
/// Independent data class for storing a Player.
/// </summary>
public class Player : Entity
{
	public Player(string name, long id, Vector3D position, Quaternion rotation)
	{
		Name = name;
		EntityId = id;
		Position = position;
		Rotation = rotation;

		InventoryManager.Instance.AddInventoryToEntity(this, 4, 4);
	}

	//NonSerialised fields.
	public MovementController movementController;
	public InventoryController inventoryController;

	public bool InventoryUpdateFlag;

	protected int healthPoints;
	protected float oxygenLevel;

	/// <summary>
	/// Returns a value indicating whether this Player is using the Inventory system.
	/// </summary>
	public bool IsUsingInventorySystem
	{
		get
		{
			return inventoryController.ShowingOverlay == false;
		}
	}

	/// <summary>
	/// Gets or sets the Health of this Player.
	/// </summary>
	public int Health
	{
		get
		{
			return healthPoints;
		}

		set
		{
			healthPoints = value;
		}
	}

	/// <summary>
	/// Gets or sets the suit Oxygen level for this Player.
	/// </summary>
	public float Oxygen
	{
		get
		{
			return oxygenLevel;
		}

		set
		{
			oxygenLevel = value;
		}
	}
}