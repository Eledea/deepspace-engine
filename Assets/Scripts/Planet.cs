using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sub-class of Orbital for Planet.
/// </summary>
public class Planet : Orbital
{
	/// <summary>
	/// Generate the Orbitals for this Planet.
	/// </summary>
	public void GeneratePlanet(int maxMoons)
	{
		//TODO: We need a better way of defining the properties of an Orbital.

		this.Name = "Planet";
		this.Type = 2;
		this.Mu = 39860044180000;
		this.InitAngle = UnityEngine.Random.Range(0, Mathf.PI*2);
		this.OrbitalDistance = 149597870700;
		this.OrbitalPeriod = (Math.Sqrt((4 * Math.PI * Math.PI * Math.Pow(this.OrbitalDistance, 3)) / this.Parent.Mu) * 10) / 1000000;

		//Generate the moons for this planet.
		for (int i = 0; i < UnityEngine.Random.Range (0, maxMoons + 1); i++)
		{
			Orbital moon = new Orbital ();
			this.AddChild (moon);

			moon.Name = "Moon";
			moon.Type = 3;
			moon.Mu = 4904869500000;
			moon.InitAngle = UnityEngine.Random.Range(0, Mathf.PI*2);
			moon.OrbitalDistance = 3840000000;
			moon.OrbitalPeriod = (Math.Sqrt((4 * Math.PI * Math.PI * Math.Pow(moon.OrbitalDistance, 3)) / moon.Parent.Mu) * 10) / 10000000;
		}
	}
}