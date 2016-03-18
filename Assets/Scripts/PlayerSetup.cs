using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;

	[SerializeField]
	string remoteLayerName = "RemotePlayer"; 

	Camera sceneCamera;

	void Start() 
	{
		if (!isLocalPlayer) {
			DisableComponents();
			AssignRemotelayer();
		} else 
		{
			sceneCamera = Camera.main;
			if (sceneCamera != null)
			{
				sceneCamera.gameObject.SetActive(false);
			}
		}

		string _ID = "Player " + GetComponent<NetworkIdentity> ().netId;
		transform.name = _ID;

	}

	void AssignRemotelayer()
	{
		gameObject.layer = LayerMask.NameToLayer (remoteLayerName);
	}

	void DisableComponents()
	{
		for (int i = 0; i < componentsToDisable.Length; i++) 
		{
			componentsToDisable[i].enabled = false;
		}
	}

	void OnDisable()
	{
		if (sceneCamera != null) 
		{
			sceneCamera.gameObject.SetActive(true);
		}
	}

}
