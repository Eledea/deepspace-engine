using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The SolarSystenView class manages the spawning, rendering and removing of GameObjects representing Orbitals
/// and Entities for the SolarSystem that the Player is in.
/// </summary>
public class SolarSystemView : MonoBehaviour
{
	//TODO: Replace Prefab system with asset streaming later.

	public GameObject[] prefabs;

	void Start()
	{
		//TODO: Implement floating point origin system.

		SetSolarSystem ();
	}

	SolarSystem mySolarSystem;

	Dictionary<Orbital, GameObject> orbitalToGameObject;
	Dictionary<Player, GameObject> entityToGameObject;

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

		orbitalToGameObject = new Dictionary<Orbital, GameObject> ();
		entityToGameObject = new Dictionary<Player, GameObject> ();

		mySolarSystem = GameController.Instance.Galaxy.CurrentSolarSystem;

		SpawnGameObjectForOrbital (mySolarSystem.Star, this.transform);

		for (int i = 0; i < PlayerManager.Instance.PlayerCount; i++)
			SpawnGameObjectForPlayer(PlayerManager.Instance.GetPlayerInManager (i));
	}

	/// <summary>
	/// Spawn a GameObject for this Orbital.
	/// </summary>
	void SpawnGameObjectForOrbital(Orbital orbital, Transform parent)
	{
		GameObject go = (GameObject)Instantiate (prefabs[0]);
		go.transform.position = orbital.Position;
		go.transform.parent = parent;

		orbitalToGameObject [orbital] = go;

		if (orbital.Children != null)
			for (int i = 0; i < orbital.Children.Count; i++)
				SpawnGameObjectForOrbital (orbital.Children[i], go.transform);
	}

	/// <summary>
	/// Spawns a GameObject for this player.
	/// </summary>
	void SpawnGameObjectForPlayer(Player player)
	{
		GameObject go = (GameObject)Instantiate (prefabs [1]);
		go.transform.position = player.Position.ToVector3();

		entityToGameObject [player] = go;
		go.GetComponentInChildren<MovementController>().Player = player;
		go.GetComponentInChildren<InventoryController>().Player = player;
	}

	void Update()
	{
		if (GameController.Instance.Galaxy != null)
		{
			if (mySolarSystem != null)
				UpdateGameObjectForOrbital (mySolarSystem.Star);
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
		GameObject go = entityToGameObject [player];
		go.transform.position = player.Position.ToVector3();
		go.transform.rotation = player.Rotation;
	}
}