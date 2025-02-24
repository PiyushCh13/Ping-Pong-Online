using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;

public class SocketManager : Singleton<SocketManager>
{
    public SocketIOUnity socket;
    public bool shouldReconnect = false;

    public Queue<Action> executeonMainThreadQueue = new Queue<Action>();

    void Start()
    {
    var uri = new Uri(GameManager.Instance.connectionURL);
        socket = new SocketIOUnity(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
            {
                {"token", "UNITY" }
            },
            EIO = 4,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket,
            Reconnection = false // ✅ Disable automatic reconnect
        });

        socket.JsonSerializer = new NewtonsoftJsonSerializer();

        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("Socket Connected");
        };

        socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log("Socket Disconnected: " + e);

            if (shouldReconnect)
            {
                Debug.Log("Attempting Manual Reconnect...");
                socket.Connect();
            }
        };

        socket.OnReconnectAttempt += (sender, e) =>
        {
            Debug.Log($"Reconnect Attempt {e} - Stopping further attempts.");
        };

        socket.On("matchFound", response =>
        {
            lock (executeonMainThreadQueue)
            {
                executeonMainThreadQueue.Enqueue(() =>
                {
                    GameManager.Instance.StartMatch(response);                   
                });
            }
        });

        Debug.Log("Connecting...");
        socket.Connect();
    }

    void Update()
    {
        while(executeonMainThreadQueue.Count > 0)
        {
            executeonMainThreadQueue.Dequeue().Invoke();
        }
    }

    public void StartMatchmaking()
    {
       var matchmakingData = new JObject
       {
           { "jwt", GameManager.Instance.JWT_Token }
       };

       socket.Emit("searchPlayer", matchmakingData); 
    }
}
