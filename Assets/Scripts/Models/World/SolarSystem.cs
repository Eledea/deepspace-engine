using DeepSpace.Core;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The SolarSystem class defines a SolarSystem and the Orbitals and Entities within it.
/// </summary>
public class SolarSystem
{
	public SolarSystem()
	{
		Entities = new List<Entity> ();
	}

	Star myStar;

	List <Entity> Entities;

	/// <summary>
	/// Returns the Star for this SolarSystem.
	/// </summary>
	public Star Star
	{
		get
		{
			return myStar;
		}
	}

	/// <summary>
	/// Generates a new SolarSystem of Orbitals.
	/// </summary>
	public void GenerateSolarSystem(int numPlanets, int maxMoons)
	{
		myStar = new Star ();
		myStar.GenerateStar (numPlanets, maxMoons);
	}
		
	/// <summary>
	/// Adds an Entity to this SolarSystem.
	/// </summary>
	public void AddEntityToSolarSystem(Entity entity)
	{
		Entities.Add (entity);
	}

	/// <summary>
	/// Removes an Entity from this SolarSystem.
	/// </summary>
	public void RemoveInventoryFromSolarSystem(Entity entity)
	{
		Entities.Remove (entity);
	}

	/// <summary>
	/// Returns the Entities being managed as an array.
	/// </summary>
	public Entity[] GetEntitiesInSystem()
	{
		return Entities.ToArray ();
	}
}