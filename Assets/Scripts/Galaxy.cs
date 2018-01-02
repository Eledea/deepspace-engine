using System.Collections.Generic;
using UnityEngine;

public class Galaxy
{
	public Galaxy()
	{
		SolarSystems = new List<SolarSystem>();
	}

	public List<SolarSystem> SolarSystems;

	public void Generate(int numStars)
	{
		SolarSystem solarSystem = new SolarSystem();
		solarSystem.Generate();

		SolarSystems.Add (solarSystem);
	}

	public void LoadFromFile(string fileName)
	{
		//TODO: Load a Galaxy from a folder using fileName.
	}
}