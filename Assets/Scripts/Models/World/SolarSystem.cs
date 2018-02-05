using DeepSpace.Core;
using DeepSpace.InventorySystem;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The SolarSystem class defines a SolarSystem and the Orbitals and Entities within it.
/// </summary>
public class SolarSystem
{
	List <Entity> Entities;

	/// <summary>
	/// Returns the Entities in this SolarSystem as an array.
	/// </summary>
	public Entity[] EntitiesInSystem
	{
		get
		{
			if (Entities == null)
				Entities = new List<Entity>();

			return Entities.ToArray ();
		}
	}

	/// <summary>
	/// Generates a new SolarSystem of Orbitals.
	/// </summary>
	public void GenerateSolarSystem(int numPlanets, int maxMoons)
	{
		Star myStar = new Star ();
		myStar.GenerateStar (numPlanets, maxMoons);
		AddEntityToSolarSystem(myStar);

		foreach (Orbital child in myStar.ChildOrbitals)
			AddEntityToSolarSystem(child);

		Storage s = new Storage("Storage", 63739, new Vector3D(15, 0, 10), Quaternion.identity);
		AddEntityToSolarSystem(s);

		InventoryManager.Instance.SpawnNewItemStackAt(IType.Wood, 42, s.Inventory, 4, 2);
	}

	/// <summary>
	/// Adds an Entity to this SolarSystem.
	/// </summary>
	public void AddEntityToSolarSystem(Entity entity)
	{
		if (Entities == null)
			Entities = new List<Entity>();

		Entities.Add (entity);
		entity.SolarSystem = this;
	}

	/// <summary>
	/// Removes an Entity from this SolarSystem.
	/// </summary>
	public void RemoveEntityFromSolarSystem(Entity entity)
	{
		if (Entities == null)
			return;

		Entities.Remove (entity);
		entity.SolarSystem = null;
	}
}