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
		Orbitals = new List<Orbital> ();
		Entities = new List<Entity> ();
	}

	Star myStar;

	List <Orbital> Orbitals;
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
	/// Returns the Orbitals in this SolarSystem as an array.
	/// </summary>
	public Orbital[] OrbitalsInSystem
	{
		get
		{
			return Orbitals.ToArray ();
		}
	}

	/// <summary>
	/// Returns the Entities in this SolarSystem as an array.
	/// </summary>
	public Entity[] EntitiesInSystem
	{
		get
		{
			return Entities.ToArray ();
		}
	}

	/// <summary>
	/// Generates a new SolarSystem of Orbitals.
	/// </summary>
	public void GenerateSolarSystem(int numPlanets, int maxMoons)
	{
		myStar = new Star ();
		myStar.GenerateStar (numPlanets, maxMoons);

		AddOrbitalToList (myStar);
	}

	/// <summary>
	/// Adds an Orbital to this SolarSystem's list of Orbitals.
	/// </summary>
	public void AddOrbitalToList(Orbital o)
	{
		Orbitals.Add (o);

		if (o.Children != null)
			for (int i = 0; i < o.Children.Count; i++)
				AddOrbitalToList (o.Children [i]);
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
}