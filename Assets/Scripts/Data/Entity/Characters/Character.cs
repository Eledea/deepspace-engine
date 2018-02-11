﻿using DeepSpace.Controllers;
using DeepSpace.Core;
using System;
using UnityEngine;

namespace DeepSpace
{
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

			Transform = new MyEntityTransformComponent(Vector3D.zero, position, rotation);
			Inventory = new MyEntityInventoryComponent(4, 4);
		}

		//NonSerialised fields.
		public EntityController m_entityController;
		public OverlayController m_overlayController;

		Player m_player;

		public Player Player
		{
			get { return m_player; }
		}

		Action cbInventoryUpdateFunc;

		public bool IsUsingInventorySystem
		{
			get
			{
				return m_overlayController.ShowingOverlay == false;
			}
		}

		protected int m_healthPoints;
		protected float m_oxygenLevel;

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

		public void RegisterInventoryUpdateCallback(Action callback)
		{
			cbInventoryUpdateFunc += callback;
		}
	}
}