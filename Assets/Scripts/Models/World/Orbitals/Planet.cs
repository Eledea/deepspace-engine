using DeepSpace.Core;

/// <summary>
/// Sub-class of an Orbital for storing a Planet.
/// </summary>
public class Planet : Orbital
{
	public void GeneratePlanet(int i, int maxMoons)
	{
		Name = "Planet";
		OrbitalAngle = Utility.RandomizedPointAngle;
		OrbitalDistance = (ulong)(50000 * (i + 1));

		int numMoons = UnityEngine.Random.Range (0, maxMoons + 1);

		for (int m = 0; m < numMoons; m++)
		{
			Moon moon = new Moon ();
			moon.GenerateMoon ();
			this.AddChild (moon);
		}
	}
}