using DeepSpace.Core;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The SolarSystenView class manages the spawning, rendering and removing of GameObjects representing Orbitals
/// and Entities for the SolarSystem that the Player is in.
/// </summary>
public class SolarSystemView : MonoBehaviour
{
	//TODO: Replace hardcoded Prefabs with asset streaming later on.

	public GameObject player;
	public GameObject sphere;
	public GameObject cube;

	public static SolarSystemView Instance;

	void OnEnable()
	{
		Instance = this;
	}

	/// <summary>
	/// The Player data class that this controller is linked to.
	/// </summary>
	public Player Player { get; set;}

	SolarSystem mySolarSystem;

	GameObject playerGO;

	Dictionary<Orbital, GameObject> orbitalToGameObject;
	Dictionary<GameObject, Orbital> gameObjectToOrbital;

	Dictionary<Entity, GameObject> entityToGameObject;
	Dictionary<GameObject, Entity> gameObjectToEntity;

	Vector3D floatingOrigin;
	double loadRange;

	/// <summary>
	/// Sets a new SolarSystem.
	/// </summary>
	public void SetSolarSystem()
	{
		while (transform.childCount > 0)
		{
			Transform child = transform.GetChild (0);
			child.SetParent (null);
			Destroy (child.gameObject);
		}
			
		orbitalToGameObject = new Dictionary<Orbital, GameObject>();
		gameObjectToOrbital = new Dictionary<GameObject, Orbital>();

		entityToGameObject = new Dictionary<Entity, GameObject>();
		gameObjectToEntity = new Dictionary<GameObject, Entity>();

		mySolarSystem = GameController.Instance.Galaxy.CurrentSolarSystem;

		floatingOrigin = Player.Position;
		UpdateGameObjectForPlayer ();

		//TODO: Implement loading range.

		//We want to only show Orbitals and Entities that are within a loading range.
		//We'll need a way to determine if the position of an Orbital/Entity is within our load range...

		foreach (Orbital o in mySolarSystem.OrbitalsInSystem)
			SpawnGameObjectForOrbital (o);

		foreach (Entity e in mySolarSystem.EntitiesInSystem)
			SpawnGameObjectForEntity (e);
	}

	void Update()
	{
		//TODO: Replace with an Event System so that we are not updating every frame.

		foreach (Orbital o in mySolarSystem.OrbitalsInSystem)
			UpdateGameObjectForOrbital (o);

		foreach (Entity e in mySolarSystem.EntitiesInSystem)
			UpdateGameObjectForEntity (e);
	}

	/// <summary>
	/// Determines if this Orbital has a GameObject spawned.
	/// </summary>
	public bool GameObjectForOrbital(Orbital o)
	{
		if (orbitalToGameObject != null)
			return true;
		else
			return false;
	}

	/// <summary>
	/// Returns an Orbital from a GameObject.
	/// </summary>
	public Orbital GameObjectToOrbital(GameObject go)
	{
		return gameObjectToOrbital [go];
	}

	/// <summary>
	/// Returns a GameObject from an Orbital.
	/// </summary>
	public GameObject OrbitalToGameObject(Orbital o)
	{
		return orbitalToGameObject [o];
	}

	/// <summary>
	/// Determines if this Entity has a GameObject spawned.
	/// </summary>
	public bool GameObjectForEntity(Entity e)
	{
		if (entityToGameObject != null)
			return true;
		else
			return false;
	}

	/// <summary>
	/// Returns an Entity from a GameObject.
	/// </summary>
	public Entity GameObjectToEntity(GameObject go)
	{
		return gameObjectToEntity [go];
	}

	/// <summary>
	/// Returns a GameObject from an Entity.
	/// </summary>
	public GameObject EntityToGameObject(Entity e)
	{
		return entityToGameObject [e];
	}

	/// <summary>
	/// Updates the GameObject for this Player.
	/// </summary>
	void UpdateGameObjectForPlayer()
	{
		if (playerGO == null)
			SpawnGameObjectForPlayer ();

		playerGO.transform.position = (Player.Position - floatingOrigin).ToVector3();
		playerGO.transform.rotation = Player.Rotation;
	}

	/// <summary>
	/// Spawns the GameObject for this Player.
	/// </summary>
	void SpawnGameObjectForPlayer()
	{
		playerGO = (GameObject)Instantiate (player);
		playerGO.GetComponentInChildren<MovementController>().Player = Player;
		playerGO.GetComponentInChildren<InventoryController>().Player = Player;
	}

	/// <summary>
	/// Destroys the GameObject for a Player using a GameObject reference.
	/// </summary>
	void DestroyGameObjectForPlayer()
	{
		Destroy (playerGO);
		playerGO = null;
	}

	/// <summary>
	/// Updates the GameObject for this Orbital.
	/// </summary>
	void UpdateGameObjectForOrbital(Orbital o)
	{
		if (GameObjectForOrbital (o) == false)
			SpawnGameObjectForOrbital (o);

		GameObject myGO = OrbitalToGameObject (o);
		myGO.transform.position = (o.Position - floatingOrigin).ToVector3();
	}

	/// <summary>
	/// Spawns a GameObject for this Orbital.
	/// </summary>
	void SpawnGameObjectForOrbital(Orbital o)
	{
		GameObject myGO = (GameObject)Instantiate (sphere);
		myGO.transform.parent = this.transform;
		myGO.name = o.Name;

		orbitalToGameObject [o] = myGO;
		gameObjectToOrbital [myGO] = o;
	}

	/// <summary>
	/// Destroys the GameObject for an Orbital using a GameObject reference.
	/// </summary>
	void DestroyGameObjectForOrbital(GameObject go)
	{
		Orbital o = GameObjectToOrbital (go);
		orbitalToGameObject.Remove (o);
		gameObjectToOrbital.Remove (go);

		Destroy (go);
	}

	/// <summary>
	/// Updates the GameObject for this Entity.
	/// </summary>
	void UpdateGameObjectForEntity(Entity e)
	{
		if (GameObjectForEntity (e) == false)
			SpawnGameObjectForEntity (e);

		GameObject myGO = EntityToGameObject (e);
		myGO.transform.position = (e.Position - floatingOrigin).ToVector3();
	}

	/// <summary>
	/// Spawns a GameObject for this Entity.
	/// </summary>
	void SpawnGameObjectForEntity(Entity e)
	{
		GameObject myGO = (GameObject)Instantiate (cube);
		myGO.transform.parent = this.transform;

		entityToGameObject [e] = myGO;
		gameObjectToEntity [myGO] = e;
	}

	/// <summary>
	/// Destroys the GameObject for an Entity using a GameObject reference.
	/// </summary>
	void DestroyGameObjectForEntity(GameObject go)
	{
		Entity e = GameObjectToEntity(go);
		entityToGameObject.Remove (e);
		gameObjectToEntity.Remove (go);

		Destroy (go);
	}
}