using DeepSpace.Core;
using DeepSpace.InventorySystem;
using System;
using UnityEngine;

/// <summary>
/// The Character class defines a Character for a Player.
/// </summary>
public class Character : Entity
{
	public Character(Player player, string name, long id, Vector3D position, Quaternion rotation)
	{
		m_player = player;

		Name = name;
		EntityId = id;
		Position = position;
		Rotation = rotation;

		InventoryManager.Instance.AddInventoryToEntity(this, 4, 4);
	}

	Player m_player;

	/// <summary>
	/// Returns this Character's Player.
	/// </summary>
	public Player Player
	{
		get { return m_player; }
	}

	//NonSerialised fields.
	public EntityController entityController;
	public OverlayController overlayController;

	Action cbInventoryUpdateFunc;

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

	protected int m_healthPoints;
	protected float m_oxygenLevel;

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
}