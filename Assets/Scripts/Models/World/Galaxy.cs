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
	public SolarSystem CurrentSolarSystem;

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

		CurrentSolarSystem = SolarSystems [0];

		PlayerManager.Instance.CreatePlayerInManager (CurrentSolarSystem);

		//TODO: Wtf is this doing? Fix it asap!
		SolarSystemView.Instance.Player = PlayerManager.Instance.GetPlayerWithId (37331);

		SolarSystemView.Instance.OnSolarSystemChange ();
	}

	/// <summary>
	/// Sets the current SolarSystem we are showing.
	/// </summary>
	public void SetSolarSystem(int id)
	{
		CurrentSolarSystem = SolarSystems [id];
	}

	float advanceTimeTimer = 1f;

	/// <summary>
	/// Updates the time timer.
	/// </summary>
	public void UpdateTime()
	{
		advanceTimeTimer -= Time.deltaTime;

		if (advanceTimeTimer < 0)
			AdvanceTime ();
	}

	/// <summary>
	/// Advances the time in this Galaxy by 1 second.
	/// </summary>
	public void AdvanceTime()
	{
		timeSinceStart += 1;
		advanceTimeTimer = 0f;
	}
}