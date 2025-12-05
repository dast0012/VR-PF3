using UnityEngine;
using System.IO.Ports;

public class ArduinoConnector : MonoBehaviour
{
    SerialPort serial = new SerialPort("COM4", 9600);
    public string data = "";
    public string humidity = "";
    public string temperature = "";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            if (!serial.IsOpen)
            {
                serial.DtrEnable = true;   // <--- REQUIRED for Arduino
                serial.RtsEnable = true;   // <--- REQUIRED for Arduino
                serial.Open();
                serial.ReadTimeout = 100; // The time the serial will wait before reading the value is 100 milliseconds
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Not working Com: " + e.Message);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!serial.IsOpen) return;
        try
        {
            Debug.Log("Incomming data: " + data);
            data = serial.ReadLine();
            string[] parts = data.Split(',');
            humidity = parts[0];
            temperature = parts[1];
        }
        catch (System.TimeoutException)
        {
            // no data yet = ignore
        }
        catch (System.Exception e)
        {
            Debug.Log("Serial read error: " + e.Message);
        }
        
    }
}