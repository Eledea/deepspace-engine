using DeepSpace.Core;
using System;
using UnityEngine;

namespace DeepSpace.Networking
{
	public class EntityManager : MonoBehaviour
	{
		public MyEntityDefinitionId[] EntityDefinitions;
		public GameObject[] EntityPrefabs;
		public GameObject LocalCharacterPrefab;

		void OnEnable()
		{
			_EntityDefinitions = EntityDefinitions;
			_EntityPrefabs = EntityPrefabs;
			_LocalCharacterPrefab = LocalCharacterPrefab;
		}

		public static MyEntityDefinitionId[] _EntityDefinitions;
		public static GameObject[] _EntityPrefabs;
		public static GameObject _LocalCharacterPrefab;

		public static void InstantiateBuildable(BuildRequest request)
		{
			if (request.BuildCheckResult != BuildCheckResult.OK)
				return;

			if (request.Definition is MyEntityDefinitionId == false)
			{
				Debug.LogError(string.Format("ERROR: Entity definition is invalid!"));
				return;
			}

			var definition = (MyEntityDefinitionId)request.Definition;

			long id = DateTime.Now.Ticks;

			//TODO: Create an Instance of the class that corresponds to the Buildable we want to create using MyEntityDefinitionId.
			var b = new Storage();
			b.OnBuildableCreated(new BuildData(definition.Name, id, request.Position, request.Orientation));
			request.SolarSystem.AddEntityToSolarSystem(b);
		}
	}
}