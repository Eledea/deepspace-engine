using DeepSpace.Core;
using DeepSpace.Networking;
using System.Collections.Generic;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// The SolarSystem class defines a SolarSystem and the Orbitals and Entities within it.
	/// </summary>
	public class SolarSystem
	{
		List<Player> m_players;
		List<Entity> m_entities;

		/// <summary>
		/// Returns the Players in this SolarSystem as an array.
		/// </summary>
		public Player[] PlayersInSystem
		{
			get
			{
				if (m_players == null)
					m_players = new List<Player>();

				return m_players.ToArray();
			}
		}

		/// <summary>
		/// Returns the Entities in this SolarSystem as an array.
		/// </summary>
		public Entity[] EntitiesInSystem
		{
			get
			{
				if (m_entities == null)
					m_entities = new List<Entity>();

				return m_entities.ToArray();
			}
		}

		/// <summary>
		/// Generates a new SolarSystem of Orbitals.
		/// </summary>
		public void GenerateSolarSystem(int numPlanets, int maxMoons)
		{
			var myStar = new Star() as Star;
			myStar.GenerateStar(numPlanets, maxMoons);
			AddEntityToSolarSystem(myStar);

			foreach (Orbital child in myStar.ChildOrbitals)
				AddEntityToSolarSystem(child);

			var s = new Storage("Storage", 673731, Vector3D.zero, new Vector3D(10, 0, 10), Quaternion.identity);
			AddEntityToSolarSystem(s);
		}

		/// <summary>
		/// Adds a Player to this SolarSystem.
		/// </summary>
		public void AddPlayerToSolarSystem(Player p)
		{
			if (m_players == null)
				m_players = new List<Player>();

			m_players.Add(p);

			AddEntityToSolarSystem(p.Character);
		}

		/// <summary>
		/// Adds an Entity to this SolarSystem.
		/// </summary>
		public void AddEntityToSolarSystem(Entity e)
		{
			if (m_entities == null)
				m_entities = new List<Entity>();

			m_entities.Add(e);
			e.SolarSystem = this;

			PlayerManager.OnEntityTransformComponentUpdate(e);
		}

		/// <summary>
		/// Removes a Player from this SolarSystem.
		/// </summary>
		public void RemovePlayerFromSolarSystem(Player p)
		{
			if (m_players == null)
				return;

			m_players.Remove(p);
			RemoveEntityFromSolarSystem(p.Character);
		}

		/// <summary>
		/// Removes an Entity from this SolarSystem.
		/// </summary>
		public void RemoveEntityFromSolarSystem(Entity entity)
		{
			if (m_entities == null)
				return;

			m_entities.Remove(entity);
			entity.SolarSystem = null;
		}
	}
}