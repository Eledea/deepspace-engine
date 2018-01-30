using DeepSpace.Core;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The SolarSystenView class manages the spawning, rendering and removing of GameObjects representing Orbitals
/// and Entities for the SolarSystem that the Player is in.
/// </summary>
public class SolarSystemView : MonoBehaviour
{
	//TODO: Replace Prefab system with asset streaming later on.

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
	float loadRange;

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
		SpawnGameObjectForPlayer ();

		//TODO: Implement loading range.

		foreach (Orbital o in mySolarSystem.OrbitalsInSystem)
			SpawnGameObjectForOrbital (o);

		foreach (Entity e in mySolarSystem.EntitiesInSystem)
			SpawnGameObjectForEntity (e);
	}

	void Update()
	{
		UpdateGameObjectForPlayer ();
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
	/// Spawns the GameObject for this Player.
	/// </summary>
	void SpawnGameObjectForPlayer()
	{
		playerGO = (GameObject)Instantiate (player);
		playerGO.GetComponentInChildren<MovementController>().Player = Player;
		playerGO.GetComponentInChildren<InventoryController>().Player = Player;

		playerGO.transform.position = (Player.Position - floatingOrigin).ToVector3();
		playerGO.transform.rotation = Player.Rotation;
	}

	/// <summary>
	/// Updates the GameObject for this Player.
	/// </summary>
	void UpdateGameObjectForPlayer()
	{
		playerGO.transform.position = (Player.Position - floatingOrigin).ToVector3();
		playerGO.transform.rotation = Player.Rotation;
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
	/// Spawns a GameObject for this Orbital.
	/// </summary>
	void SpawnGameObjectForOrbital(Orbital o)
	{
		GameObject myGO = (GameObject)Instantiate (sphere);
		myGO.transform.position = (o.Position - floatingOrigin).ToVector3();
		myGO.transform.parent = this.transform;
		myGO.name = o.Name;

		orbitalToGameObject [o] = myGO;
		gameObjectToOrbital [myGO] = o;
	}

	/// <summary>
	/// Updates the GameObject for this Orbital.
	/// </summary>
	void UpdateGameObjectForOrbital(Orbital o)
	{
		GameObject myGO = OrbitalToGameObject (o);
		myGO.transform.position = (o.Position - floatingOrigin).ToVector3();
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
	/// Spawns a GameObject for this Entity.
	/// </summary>
	void SpawnGameObjectForEntity(Entity e)
	{
		GameObject myGO = (GameObject)Instantiate (cube);
		myGO.transform.position = (e.Position - floatingOrigin).ToVector3();
		myGO.transform.parent = this.transform;

		entityToGameObject [e] = myGO;
		gameObjectToEntity [myGO] = e;
	}

	/// <summary>
	/// Updates the GameObject for this Entity.
	/// </summary>
	void UpdateGameObjectForEntity(Entity e)
	{
		GameObject myGO = EntityToGameObject (e);
		myGO.transform.position = (e.Position - floatingOrigin).ToVector3();
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