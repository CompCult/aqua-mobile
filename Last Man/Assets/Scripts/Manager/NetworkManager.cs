using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour 
{
	private const string typeName = "Last Man";
	private const string gameName = "Global";
	private HostData[] hostList;
	private HostData globalServer;

	public Object playerPrefab;

	public void StartServer()
	{
	    Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
	    MasterServer.RegisterHost(typeName, gameName);
	}

	public void OnServerInitialized()
	{
	    Debug.Log("Server Initializied");
	    SpawnPlayer();
	}

	public void JoinServer()
	{
		if (globalServer != null)
	    	Network.Connect(globalServer);
	    else 
	    	Debug.Log("No Global Server Found");
	}
	 
	public void OnConnectedToServer()
	{
	    Debug.Log("Server Joined");
	    SpawnPlayer();
	}

	public void RefreshHostList()
	{
	    MasterServer.RequestHostList(typeName);
	}
	 
	public void OnMasterServerEvent(MasterServerEvent msEvent)
	{
	    if (msEvent == MasterServerEvent.HostListReceived)
	    {
	        hostList = MasterServer.PollHostList();

	        if (hostList.Length == 0)
	        {
	        	Debug.Log("Global Server Offline");
	        	return;
	        }

	        if (hostList[0] != null)
            {	
        		Debug.Log("Global Server Online");
        		globalServer = hostList[0];
        		return;
            }
	    }
	}

	private void SpawnPlayer()
	{
	    Network.Instantiate(playerPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
	}
}
