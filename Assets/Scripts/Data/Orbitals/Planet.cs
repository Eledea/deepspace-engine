using DeepSpace.Core;

namespace DeepSpace.Orbitals
{
	/// <summary>
	/// The Planet class defines a Planet in a SolarSystem.
	/// </summary>
	public class Planet : Orbital
	{
		public void GeneratePlanet(int i, int maxMoons)
		{
			Name = "Planet";
			OrbitalAngle = Utility.RandomizedPointAngle;
			OrbitalDistance = (ulong)(50000 * (i + 1));
			Position = OrbitalPosition;

			int numMoons = UnityEngine.Random.Range(0, maxMoons + 1);

			for (int m = 0; m < numMoons; m++)
			{
				Moon moon = new Moon();
				moon.GenerateMoon();
				this.AddChild(moon);
			}
		}
	}
}