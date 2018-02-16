using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeepSpace
{
	/// <summary>
	/// The Galaxy class defines a Galaxy and the SolarSystems within it.
	/// </summary>
	public class Galaxy
	{
		public Galaxy()
		{
			SolarSystems = new List<SolarSystem>();
		}

		public UInt64 timeSinceStart { get; private set; }
		public List<SolarSystem> SolarSystems { get; private set; }

		/// <summary>
		/// Generate a new galaxy with number of solar systems.
		/// </summary>
		public void GenerateGalaxy(int numSolarSystems, int numPlanets, int maxMoons)
		{
			for (int i = 0; i < numSolarSystems; i++)
			{
				SolarSystem ss = new SolarSystem();
				ss.GenerateSolarSystem(numPlanets, maxMoons);

				SolarSystems.Add(ss);
			}
		}

		/// <summary>
		/// Advances the time in this Galaxy by 1 second.
		/// </summary>
		public void AdvanceTime()
		{
			timeSinceStart += 1;
		}
	}
}