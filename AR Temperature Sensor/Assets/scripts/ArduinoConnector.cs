using UnityEngine;
using System.IO.Ports;

public class ArduinoConnector : MonoBehaviour
{
    SerialPort serial = new SerialPort("COM4", 9600);
    public string data = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        serial.Open();
        serial.ReadTimeout = 100; // The time the serial will wait before reading the value is 100 milliseconds
    }

    // Update is called once per frame
    void Update()
    {
        string data = serial.ReadLine();
    }
}
