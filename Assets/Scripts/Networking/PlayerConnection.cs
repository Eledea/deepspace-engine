using UnityEngine;
using UnityEngine.Networking;

namespace DeepSpace.Networking
{
	public class PlayerConnection : NetworkBehaviour
	{
		void Start()
		{
			//If this isn't the GlobalPlayer object for the corresponding Player, bail here. 
			if (isLocalPlayer == false)
				return;

			Debug.Log(string.Format("{0} connected to the server.", "Sam"));

			//TODO: Check to see if this is a new Player joining or an existing one.

			var solarSystem = GalaxyManager.Galaxy.SolarSystems[0] as SolarSystem;
			PlayerManager.OnNewPlayerConnect(this, "Sam", solarSystem);
		}
	}
}