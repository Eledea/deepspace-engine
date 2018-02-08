using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnection : NetworkBehaviour
{
	void Start()
	{
		if (isLocalPlayer == false)
			return;

		Debug.Log(string.Format("{0} connected to the server.", "Sam"));

		SolarSystem ss = GalaxyManager.Instance.Galaxy.SolarSystems[0];
		PlayerManager.Instance.OnNewPlayerConnect(this, "Sam", ss);
	}
}