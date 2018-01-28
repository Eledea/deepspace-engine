using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Graphical class responsible for visually displaying changes to data in our data classing.
/// </summary>
public class SolarSystemView : MonoBehaviour
{
	public GameObject[] gameObjects;

	void Start()
	{
		//TODO: Dramatic changed needed in this class to make it work with the (half-implemnented) Entity system.

		SetSolarSystem ();
	}

	SolarSystem mySolarSystem;

	Dictionary<Orbital, GameObject> orbitalGameObjectMap;
	Dictionary<Player, GameObject> playerGameObjectMap;

	/// <summary>
	/// Sets a new SolarSystem to display.
	/// </summary>
	public void SetSolarSystem()
	{
		while (transform.childCount > 0)
		{
			Transform child = transform.GetChild (0);
			child.SetParent (null);
			Destroy (child.gameObject);
		}

		orbitalGameObjectMap = new Dictionary<Orbital, GameObject> ();
		playerGameObjectMap = new Dictionary<Player, GameObject> ();

		mySolarSystem = GameController.Instance.Galaxy.CurrentSolarSystem;

		SpawnGameObjectForOrbital (mySolarSystem, this.transform);

		for (int i = 0; i < PlayerManager.Instance.PlayerCount; i++)
			SpawnGameObjectForPlayer(PlayerManager.Instance.GetPlayerInManager (i));
	}

	/// <summary>
	/// Spawn a GameObject for this Orbital.
	/// </summary>
	void SpawnGameObjectForOrbital(Orbital orbital, Transform parent)
	{
		GameObject go = (GameObject)Instantiate (gameObjects[0]);
		go.transform.position = orbital.Position;
		go.transform.parent = parent;

		orbitalGameObjectMap [orbital] = go;

		if (orbital.Children != null)
			for (int i = 0; i < orbital.Children.Count; i++)
				SpawnGameObjectForOrbital (orbital.Children[i], go.transform);
	}

	/// <summary>
	/// Spawns a GameObject for this player.
	/// </summary>
	void SpawnGameObjectForPlayer(Player player)
	{
		GameObject go = (GameObject)Instantiate (gameObjects [1]);
		go.transform.position = player.Position;

		playerGameObjectMap [player] = go;
		go.GetComponentInChildren<MovementController>().Player = player;
		go.GetComponentInChildren<InventoryController>().Player = player;
	}

	void Update()
	{
		if (GameController.Instance.Galaxy != null)
		{
			if (mySolarSystem != null)
				UpdateGameObjectForOrbital (mySolarSystem);
			else
				Debug.LogError ("We have a Galaxy loaded, but no SolarSystem!");

			for (int i = 0; i < PlayerManager.Instance.PlayerCount; i++)
				UpdateGameObjectForPlayer(PlayerManager.Instance.GetPlayerInManager (i));
		}
	}

	/// <summary>
	/// Updates the GameObject for this Orbital.
	/// </summary>
	void UpdateGameObjectForOrbital (Orbital orbital)
	{
		//TODO: Right now this function does nothing. Later we'll use it for Dynamic Rendering.

		/*GameObject go = orbitalGameObjectMap [orbital];

		if (orbital.Children != null)
			for (int i = 0; i < orbital.Children.Count; i++)
				UpdateGameObjectForOrbital (orbital.Children[i]);*/
	}

	/// <summary>
	/// Updates the GameObject for this Player.
	/// </summary>
	void UpdateGameObjectForPlayer(Player player)
	{
		GameObject go = playerGameObjectMap [player];
		go.transform.position = player.Position;
		go.transform.rotation = player.Rotation;
	}
}