using UnityEngine;

/// <summary>
/// Sub-class of an Orbital for storing a Planet.
/// </summary>
public class Planet : Orbital
{
	/// <summary>
	/// Generates the data for the Orbitals around this Planet.
	/// </summary>
	public void GeneratePlanet(int i, int maxMoons)
	{
		InitAngle = Random.Range(0, Mathf.PI * 2);
		Mu = 41590495;
		OrbitalDistance = (ulong)(1000000 * (i + 1));
		OrbitalPeriod = (ulong)System.Math.Sqrt((39.4784176 * System.Math.Pow(OrbitalDistance, 3)) / Mu);

		int numMoons = Random.Range (0, maxMoons + 1);

		for (int m = 0; m < numMoons; m++)
		{
			Moon moon = new Moon ();
			moon.GenerateMoon ();
			this.AddChild (moon);
		}
	}
}