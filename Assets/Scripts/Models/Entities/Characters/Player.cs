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
	public SolarSystemView solarSystemView;
	public EntityController entityController;
	public OverlayController overlayController;

	//Call this action if an Inventory in the SolarSystem a Player is in gets updated.
	Action cbInventoryUpdateFunc;

	protected int m_healthPoints;
	protected float m_oxygenLevel;

	/// <summary>
	/// Returns a value indicating whether this Player is using the Inventory system.
	/// </summary>
	public bool IsUsingInventorySystem
	{
		get
		{
			return overlayController.ShowingOverlay == false;
		}
	}

	/// <summary>
	/// Gets or sets the Health of this Player.
	/// </summary>
	public int Health
	{
		get
		{
			return m_healthPoints;
		}

		set
		{
			m_healthPoints = value;
		}
	}

	/// <summary>
	/// Gets or sets the suit Oxygen level for this Player.
	/// </summary>
	public float Oxygen
	{
		get
		{
			return m_oxygenLevel;
		}

		set
		{
			m_oxygenLevel = value;
		}
	}

	/// <summary>
	/// Register a function to callback when an Inventory in this Player's SolarSystem updates.
	/// </summary>
	public void RegisterInventoryUpdateCallback(Action callback)
	{
		cbInventoryUpdateFunc += callback;
	}

	/// <summary>
	/// Unregister a function to callback when the Inventory updates.
	/// </summary>
	public void UnregisterInventoryUpdateCallback(Action callback)
	{
		cbInventoryUpdateFunc -= callback;
	}
}