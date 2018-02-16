using DeepSpace.Controllers;
using DeepSpace.Core;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// The Player class defines the attributes of a Player both online and offline and it's respective Character.
	/// </summary>
	public class Player
	{
		public Player(string name, long id, Vector3D position, Quaternion rotation, SolarSystemView view)
		{
			m_character = new Character(this, name, id, position, rotation);

			m_solarSystemView = view;
			m_solarSystemView.Character = m_character;
		}

		public bool IsOnline
		{
			get
			{
				return m_solarSystemView;
			}
		}

		public bool IsSpawned
		{
			get
			{
				if (IsOnline == false)
					return false;

				return m_solarSystemView.GetComponent<SolarSystemView>().LocalCharacter;
			}
		}

		SolarSystemView m_solarSystemView;

		public SolarSystemView View
		{
			get { return m_solarSystemView; }
		}

		Character m_character;

		public Character Character
		{
			get { return m_character; }
		}
	}
}