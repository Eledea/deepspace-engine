using DeepSpace.Core;
using UnityEngine;

namespace DeepSpace.Networking
{
	public class DefinitionsManager : MonoBehaviour
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
	}
}