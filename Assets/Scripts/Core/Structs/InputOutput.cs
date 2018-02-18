using DeepSpace.Controllers;
using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// Sets up and contains the Controller Data for a spawned Character.
	/// </summary>
	public struct InputOutput
	{
		public InputOutput (GameObject go, Character c)
		{
			go.GetComponentInChildren<EntityController>().Character = c;
			go.GetComponentInChildren<OverlayController>().Character = c;
			go.GetComponentInChildren<BuildController>().Character = c;

			EntityController = go.GetComponentInChildren<EntityController>();
			OverlayController = go.GetComponentInChildren<OverlayController>();
			BuildController = go.GetComponentInChildren<BuildController>();
			Camera = go.GetComponentInChildren<Camera>();
		}

		public readonly EntityController EntityController;
		public readonly OverlayController OverlayController;
		public readonly BuildController BuildController;
		public readonly Camera Camera;
	}
}