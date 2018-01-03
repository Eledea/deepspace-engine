using System.Collections.Generic;
using UnityEngine;

public class SolarSystemView : MonoBehaviour
{
	void Start()
	{
		gc = GameObject.FindObjectOfType<GameController>();
		ShowSolarSystem();
	}

	GameController gc;
	SolarSystem solarSystem;

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

		solarSystem = gc.solarSystem;

		for (int i = 0; i < solarSystem.Orbitals.Count; i++)
			SpawnGameObjectForOrbital (gameObjects[0], this.transform, solarSystem.Orbitals[i]);
	}

	/// <summary>
	/// Spawns the GameObject for this Orbital.
	/// </summary>
	public void SpawnGameObjectForOrbital(GameObject prefab, Transform transformParent, Orbital orbital)
	{
		GameObject go =
		(GameObject)Instantiate (
			prefab,
			orbital.Position,
			Quaternion.identity,
			transformParent
		);

		orbitalGameObjectMap[orbital] = go;

		for (int i = 0; i < orbital.Children.Count; i++)
		{
			SpawnGameObjectForOrbital(prefab, go.transform, orbital.Children[i]);
		}
	}
}