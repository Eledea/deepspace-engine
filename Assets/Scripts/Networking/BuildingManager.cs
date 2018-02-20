using DeepSpace.Core;
using System;
using UnityEngine;

namespace DeepSpace.Networking
{
	public class BuildingManager : MonoBehaviour
	{
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
			b.OnBuildableCreated(new BuildData(definition, id, request.Position, request.Orientation));
			request.SolarSystem.AddEntityToSolarSystem(b);
		}
	}
}