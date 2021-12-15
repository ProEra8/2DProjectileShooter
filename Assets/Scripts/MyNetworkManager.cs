using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class MyNetworkManager : NetworkManager
{
    private Dictionary<NetworkConnection, int> playerConns = new Dictionary<NetworkConnection, int>();
    private int currentTeamNumber = 1;
    [SerializeField] private string startLevelName = "Level_Test";
    public override void OnStartServer()
    {
       Debug.Log("Server Started");
       base.OnStartServer();
    }

    public override void OnStopServer()
    {
        Debug.Log("Server Stopped");
        base.OnStopServer();
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("Connected to server");
        playerConns.Add(conn, currentTeamNumber);
        currentTeamNumber++;
        base.OnClientConnect(conn);
        LoadLevel();
        Application.targetFrameRate = 60;
    }


    public override void OnClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("Disconnected from server");
        playerConns.Remove(conn);
        base.OnClientDisconnect(conn);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(startLevelName, LoadSceneMode.Additive);
    }


}
