using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Independent data class for storing a Galaxy.
/// </summary>
public class Galaxy
{
	public Galaxy()
	{
		SolarSystems = new List<SolarSystem> ();
	}

	public UInt64 timeSinceStart;
	public List<SolarSystem> SolarSystems;

	/// <summary>
	/// Generate a new galaxy with number of solar systems.
	/// </summary>
	public void GenerateGalaxy(int numSolarSystems, int numPlanets, int maxMoons)
	{
		for (int i = 0; i < numSolarSystems; i++)
		{
			SolarSystem ss = new SolarSystem ();
			ss.GenerateSolarSystem (numPlanets, maxMoons);

			SolarSystems.Add (ss);
		}

		PlayerManager.Instance.OnNewPlayerConnect("Sam", SolarSystems[UnityEngine.Random.Range(0, SolarSystems.Count)]);
	}

	float m_advanceTimeTimer = 1f;

	/// <summary>
	/// Updates the time timer.
	/// </summary>
	public void UpdateTime()
	{
		m_advanceTimeTimer -= Time.deltaTime;

		if (m_advanceTimeTimer < 0)
			AdvanceTime ();
	}

	/// <summary>
	/// Advances the time in this Galaxy by 1 second.
	/// </summary>
	public void AdvanceTime()
	{
		timeSinceStart += 1;
		m_advanceTimeTimer = 0f;
	}
}