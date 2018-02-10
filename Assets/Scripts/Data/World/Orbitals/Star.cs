using DeepSpace.Core;

namespace DeepSpace
{
	/// <summary>
	/// The Star class defines a Star in a SolarSystem.
	/// </summary>
	public class Star : Orbital
	{
		public void GenerateStar(int numPlanets, int maxMoons)
		{
			Name = "Star";
			OrbitalAngle = Utility.RandomizedPointAngle;
			OrbitalDistance = 0;
			Position = OrbitalPosition;

			for (int i = 0; i < numPlanets; i++)
			{
				Planet planet = new Planet();
				planet.GeneratePlanet(i, maxMoons);
				this.AddChild(planet);
			}
		}
	}
}