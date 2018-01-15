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
		OrbitalAngle = Random.Range(0, Mathf.PI * 2);
		OrbitalDistance = (ulong)(1000000 * (i + 1));

		int numMoons = Random.Range (0, maxMoons + 1);

		for (int m = 0; m < numMoons; m++)
		{
			Moon moon = new Moon ();
			moon.GenerateMoon ();
			this.AddChild (moon);
		}
	}
}