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

			Transform = new MyEntityTransformComponent(this, Vector3D.zero, position, rotation);
			Inventory = new MyEntityInventoryComponent(this, 4, 4);
		}

		[NonSerialized] public InputOutput Controllers;

		public bool IsUsingInventorySystem
		{
			get
			{
				return Controllers.OverlayController.ShowingOverlay == false;
			}
		}

		public bool IsUsingBuildingSystem
		{
			get
			{
				return Controllers.BuildController.IsBuilding == false;
			}
		}

		Player m_player;
		public Player Player
		{
			get { return m_player; }
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
	}
}