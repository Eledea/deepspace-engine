using DeepSpace.Controllers;
using DeepSpace.Core;
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

			Transform = new MyEntityTransformComponent(this, position, rotation);
			Inventory = new MyEntityInventoryComponent(this, 4, 4);
		}

		public OverlayController OverlayController { get; set; }

		public bool IsUsingInventorySystem
		{
			get
			{
				if (m_player.IsSpawned == false)
					return false;

				return OverlayController.ShowingOverlay;
			}
		}

		Player m_player;
		public Player Player
		{
			get
			{
				return m_player;
			}
		}

		public int Health
		{
			get;
			set;
		}

		public float Oxygen
		{
			get;
			set;
		}
	}
}