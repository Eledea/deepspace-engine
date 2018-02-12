using DeepSpace.Controllers;
using UnityEngine;

namespace DeepSpace.Core
{
	/// <summary>
	/// Holds the Controller Data for a Character when it is spawned.
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

		public EntityController EntityController { get; private set; }
		public OverlayController OverlayController { get; private set; }
		public BuildController BuildController { get; private set; }
		public Camera Camera { get; private set; }
	}
}