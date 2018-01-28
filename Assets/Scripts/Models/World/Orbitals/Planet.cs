using DeepSpace.Utility;
using UnityEngine;

/// <summary>
/// Sub-class of an Orbital for storing a Planet.
/// </summary>
public class Planet : Orbital
{
	public void GeneratePlanet(int i, int maxMoons)
	{
		OrbitalAngle = Utility.RandomizedPointAngle;
		OrbitalDistance = (ulong)(50000 * (i + 1));

		int numMoons = Random.Range (0, maxMoons + 1);

		for (int m = 0; m < numMoons; m++)
		{
			Moon moon = new Moon ();
			moon.GenerateMoon ();
			this.AddChild (moon);
		}
	}
}