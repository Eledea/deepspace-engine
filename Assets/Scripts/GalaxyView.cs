using System.Collections.Generic;
using UnityEngine;

public class GalaxyView : MonoBehaviour
{
	void Start()
	{
		gc = GameObject.FindObjectOfType<GameController>();
		ShowSolarSystem();
	}

	GameController gc;
	Galaxy galaxy;

	public GameObject[] gameObjects;

	Dictionary<Orbital, GameObject> orbitalGameObjectMap;

	/// <summary>
	/// Render a SolarSystem from a data class.
	/// </summary>
	public void ShowSolarSystem()
	{
		while (transform.childCount > 0)
		{
			Transform child = transform.GetChild (0);
			child.SetParent (null);
			Destroy (child.gameObject);
		}
			
		orbitalGameObjectMap = new Dictionary<Orbital, GameObject>();

		galaxy = gc.galaxy;

		for (int i = 0; i < galaxy.SolarSystems.Count; i++)
			for (int j = 0; j < galaxy.SolarSystems[i].Children.Count; j++)
				SpawnGameObjectForOrbital (galaxy.SolarSystems[i].Children[j], gameObjects[0], this.transform);
	}

	/// <summary>
	/// Spawns the GameObject for this Orbital.
	/// </summary>
	public void SpawnGameObjectForOrbital(Orbital orbital, GameObject prefab, Transform transformParent)
	{
		GameObject go = (GameObject)Instantiate (prefab);
		go.transform.position = orbital.Position;
		go.transform.parent = transformParent;

		orbitalGameObjectMap[orbital] = go;

		for (int i = 0; i < orbital.Children.Count; i++)
		{
			SpawnGameObjectForOrbital(orbital.Children[i], prefab, go.transform);
		}
	}

	void Update()
	{
		for (int i = 0; i < galaxy.SolarSystems.Count; i++)
			for (int j = 0; j < galaxy.SolarSystems[i].Children.Count; j++)
				UpdateGameObjectsForOrbital (galaxy.SolarSystems[i].Children[j]);
	}

	/// <summary>
	/// Updates the GameObject's position for this orbital.
	/// </summary>
	public void UpdateGameObjectsForOrbital (Orbital orbital)
	{
		GameObject go = orbitalGameObjectMap[orbital];
		go.transform.position = orbital.Position;

		for (int i = 0; i < orbital.Children.Count; i++)
		{
			UpdateGameObjectsForOrbital(orbital.Children[i]);
		}
	}
}