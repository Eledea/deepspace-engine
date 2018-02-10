using UnityEngine;
using UnityEngine.Networking;

namespace DeepSpace.Networking
{
	public class PlayerConnection : NetworkBehaviour
	{
		void Start()
		{
			if (isLocalPlayer == false)
				return;

			Debug.Log(string.Format("{0} connected to the server.", "Sam"));

			//TODO: Check to see if this is a new Player joining or an existing one.

			SolarSystem ss = GalaxyManager.Instance.Galaxy.SolarSystems[0];
			PlayerManager.Instance.OnNewPlayerConnect(this, "Sam", ss);
		}
	}
}