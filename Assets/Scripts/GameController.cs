using UnityEngine;

public class GameController : MonoBehaviour
{
	void OnEnable()
	{
		BuildGalaxy ("Testworld");
	}

	/// <summary>
	/// Build a galaxy with fileName.
	/// </summary>
	public void BuildGalaxy(string fileName)
	{
		galaxy = new Galaxy();
		galaxy.GenerateGalaxy (1);
	}

	/// <summary> The SolarSystem that we currently have loaded. </summary>
	public Galaxy galaxy { get; set; }

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

		galaxy.UpdateGalaxy(GameTime);
	}
}