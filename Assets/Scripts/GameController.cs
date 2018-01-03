using UnityEngine;

public class GameController : MonoBehaviour
{
	void OnEnable()
	{
	 	GenerateGalaxy ("Testworld");
	}

	/// <summary>
	/// Generate a galaxy with fileName.
	/// </summary>
	public void GenerateGalaxy(string fileName)
	{
		solarSystem = new SolarSystem();
		solarSystem.Generate (5);
	}

	/// <summary> The SolarSystem that we currently have loaded. </summary>
	public SolarSystem solarSystem { get; set; }

	/// <summary>
	/// The game time in seconds.
	/// </summary>
	public ulong GameTime = 0;

	void Update()
	{
		AdvanceTime (1);
	}

	/// <summary>
	/// Advances the time.
	/// </summary>
	public void AdvanceTime(int numSeconds)
	{
		GameTime += (uint)numSeconds;

		solarSystem.Update(GameTime);
	}
}