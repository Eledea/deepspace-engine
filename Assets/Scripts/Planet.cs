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
	public void GeneratePlanet()
	{
		this.Mu = 39860044180000;
		this.InitAngle = UnityEngine.Random.Range(0, Mathf.PI*2);
		this.OrbitalDistance = 149597870700;
		this.OrbitalPeriod = (Math.Sqrt((4 * Math.PI * Math.PI * Math.Pow(this.OrbitalDistance, 3)) / this.Parent.Mu) * 10) / 1000000;
	}
}