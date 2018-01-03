using UnityEngine;

public class GameController : MonoBehaviour
{
	void OnEnable()
	{
		solarSystem = new SolarSystem();
		solarSystem.Generate (5);
	}

	/// <summary>
	/// The SolarSystem that we currently have loaded.
	/// </summary>
	public SolarSystem solarSystem;
}