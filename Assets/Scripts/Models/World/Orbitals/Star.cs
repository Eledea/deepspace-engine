using DeepSpace.Core;

/// <summary>
/// Sub-class of an Orbital for storing a Star.
/// </summary>
public class Star : Orbital
{
	public void GenerateStar(int numPlanets, int maxMoons)
	{
		Name = "Star";
		OrbitalAngle = 0;
		OrbitalDistance = 0;

		for (int i = 0; i < numPlanets; i++)
		{
			Planet planet = new Planet ();
			planet.GeneratePlanet (i, maxMoons);
			this.AddChild (planet);
		}
	}
}