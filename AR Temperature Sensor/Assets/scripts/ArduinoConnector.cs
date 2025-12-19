using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Globalization;

public class ArduinoConnector : MonoBehaviour
{
    public int port = 8888;

    UdpClient client;
    Thread thread;
    volatile bool running;
    public string humidity = "";
    public string temperature = "";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Opret UDP klient
        client = new UdpClient(port);
        client.EnableBroadcast = true;

        // Start modtage-tråd
        running = true;
        thread = new Thread(ReceiveLoop);
        thread.IsBackground = true;
        thread.Start();

    }

    void ReceiveLoop()
    {
        IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, port);

        while (running)
        {
            try
            {
                // Modtag data fra Arduino
                byte[] data = client.Receive(ref anyIP);
                string msg = Encoding.ASCII.GetString(data).Trim();

                // Forventer: "humidity,temperature"
                string[] parts = msg.Split(',');

                if (parts.Length >= 2)
                {
                    humidity = parts[0];
                    temperature = parts[1];
                }
            }
            catch
            {
                // Ignorer fejl (fx når Unity lukkes)
            }
        }
    }

    void OnDestroy()
    {
        running = false;
        client?.Close();
    }
}