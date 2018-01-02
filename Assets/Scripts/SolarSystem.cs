using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : Orbital
{
	/// <summary>
	/// Generate a new SolarSystem.
	/// </summary>
	public void Generate()
	{
		Orbital myStar = new Orbital();
		myStar.GraphicID = 0;
		this.AddChild (myStar);

		Orbital planet = new Orbital();
		planet.MakePlanet();
		planet.GraphicID = 1;
		myStar.AddChild (planet);
	}
}