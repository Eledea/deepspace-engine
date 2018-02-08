using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Server side class that manages data based operations for the Galaxy we are currently in.
/// </summary>
public class GalaxyManager : NetworkBehaviour
{
	public static GalaxyManager Instance { get; protected set; }

	void Start()
	{
		Instance = this;

		NewGalaxy ("Testworld");
	}

	/// <summary>
	/// The Galaxy we currently have loaded.
	/// </summary>
	public Galaxy Galaxy { get; set; }

	/// <summary>
	/// Generates a new galaxy with filename.
	/// </summary>
	public void NewGalaxy(string fileName)
	{
		this.Galaxy = new Galaxy ();

		Galaxy.GenerateGalaxy (1, 4, 3);
	}

	/// <summary>
	/// Loads a galaxy from file with filename.
	/// </summary>
	public void LoadGalaxy(string fileName)
	{
		if (Galaxy != null)
		{
			Debug.LogError("ERROR: A galaxy is loaded while on the title screen.");
			return;
		}
	}

	void Update()
	{
		if (Galaxy != null)
			Galaxy.AdvanceTime();
	}
}