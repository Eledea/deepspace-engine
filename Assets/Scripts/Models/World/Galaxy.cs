using DeepSpace.InventorySystem;
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
		Players = new List<Player> ();
	}

	/// <summary>
	/// The elapsed time in game since Galaxy creation in seconds.
	/// </summary>
	public UInt64 timeSinceStart { get; set; }

	/// <summary>
	/// The SolarSystems in this Galaxy.
	/// </summary>
	public List<SolarSystem> SolarSystems { get; set; }

	/// <summary>
	/// The SolarSystem we are currently showing.
	/// </summary>
	public SolarSystem CurrentSolarSystem { get; set; }

	/// <summary>
	/// The players in this Galaxy.
	/// </summary>
	public List<Player> Players { get; set; }

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

		CreatePlayer (SolarSystems[0]);
		CurrentSolarSystem = SolarSystems [0];
	}

	/// <summary>
	/// Create a player for this Galaxy.
	/// </summary>
	void CreatePlayer(SolarSystem mySolarSystem)
	{
		//TODO: Move player creation to a PlayerManager class.

		Player p = new Player();
		p.Name = "Sam";
		p.SolarSystem = mySolarSystem;
		p.Position = new Vector3 (50000, 0, 50000);
		p.Rotation = Quaternion.Euler (0, 0, 0);
		p.Health = 100;
		p.Oxygen = 100;

		Players.Add (p);

		WoodStack w = new WoodStack (p, 5);
		StoneStack s = new StoneStack (p, 11);

		p.AddItemStackAt (w, 0, 0);
		p.AddItemStackAt (s, 2, 3);

		InventoryManager.Instance.AddPlayerToManager (p);
	}

	/// <summary>
	/// Sets the current SolarSystem we are showing.
	/// </summary>
	public void SetSolarSystem(int id)
	{
		CurrentSolarSystem = SolarSystems [id];
	}

	/// <summary>
	/// Advances game time by a number of seconds.
	/// </summary>
	void AdvanceTime(int numSeconds)
	{
		//TODO: Make this work in REAL seconds.

		timeSinceStart += (uint)numSeconds;
	}

	/// <summary>
	/// Updates all the SolarSystem in this Galaxy.
	/// </summary>
	public void UpdateGalaxy()
	{
		AdvanceTime (1);
	}
}