using BitManipulation;
using LiteNetLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager I { get { return instance; } }

    public TurrisClientListener listener;
    public NetManager client;
    public EntityManager entityManager;

    public GameObject player;

    public bool connected = false;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        listener = new TurrisClientListener();
        client = new NetManager(listener);
        client.Start();
        client.Connect("75.0.193.55", 7777, "Turris");
    }

    private void Update()
    {
        client.PollEvents();
    }

    public void Send(byte[] data, DeliveryMethod deliveryMethod)
    {
        if (!connected) return;
        client.FirstPeer.Send(data, DeliveryMethod.Sequenced);
    }
}
