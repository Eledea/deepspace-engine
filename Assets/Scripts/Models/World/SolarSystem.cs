using DeepSpace.Core;
using System.Collections.Generic;

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

		Storage s = new Storage();
		s.Name = "Storage";
		s.Position = new Vector3D(15, 0, 10);
		AddEntityToSolarSystem(s);
	}

	/// <summary>
	/// Adds an Entity to this SolarSystem.
	/// </summary>
	public void AddEntityToSolarSystem(Entity entity)
	{
		if (Entities == null)
			Entities = new List<Entity>();

		Entities.Add (entity);
	}

	/// <summary>
	/// Removes an Entity from this SolarSystem.
	/// </summary>
	public void RemoveEntityFromSolarSystem(Entity entity)
	{
		if (Entities != null)
			Entities.Remove (entity);
	}
}