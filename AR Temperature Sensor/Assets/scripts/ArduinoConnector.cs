using Android.BLE;
using Android.BLE.Commands;
using System.Text;
using System.IO.Ports;
using UnityEngine;

public class ArduinoConnector : MonoBehaviour
{
    // SerialPort serial = new SerialPort("COM4", 9600);
    private BleManager _bleManager;
    public string data = "";
    public string humidity = "";
    public string temperature = "";

    // Definér UUID'er (SKAL matche Arduino-koden)
    private const string ServiceUuid = "19B10000-E8F2-537E-4F6C-D104768A1214";
    private const string CharacteristicUuid = "19B10001-E8F2-537E-4F6C-D104768A1214";
    private const string DeviceName = "UNO R4 Sensor"; // Navn på Arduino så vi kan genkende den på telefonen

    private string _targetDeviceAddress; // Gem adresse efter scanning
    private bool _isConnected = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        try
        {
            _bleManager = BleManager.Instance; // Få singleton fra plugin
            StartScanning();

            /*if (!serial.IsOpen)
            {
                serial.DtrEnable = true;   // <--- REQUIRED for Arduino to work
                serial.RtsEnable = true;   // <--- REQUIRED for Arduino to work
                serial.Open();
                serial.ReadTimeout = 100; // The time the serial will wait before reading the value is 100 milliseconds
            }*/
        }
        catch (System.Exception e)
        {
            Debug.Log("Not working bluetooth: " + e.Message);
        }
        
    }

    private void StartScanning()
    {
        // Scan i 10 sekunder (kan ændres)
        var discoverCmd = new DiscoverDevices(OnDeviceDiscovered, OnFinishedDiscovering, 10000);
        _bleManager.QueueCommand(discoverCmd);
        Debug.Log("Started scanning for BLE devices...");
    }

    private void OnDeviceDiscovered(string address, string name)
    {
        Debug.Log($"Discovered device: {name} ({address})");

        if (name == DeviceName || name.Contains("UNO R4")) // Ekstra tjek hvis navnet varierer lidt
        {
            _targetDeviceAddress = address;
            Debug.Log($"Found target device: {DeviceName} at {address}. Connecting...");
            // INGEN stop scan her – den stopper automatisk
            ConnectToDevice();
        }
    }

    private void OnFinishedDiscovering()
    {
        Debug.Log("Scanning færdig.");

        if (string.IsNullOrEmpty(_targetDeviceAddress))
        {
            Debug.Log("Target device ikke fundet. Scanner igen om 2 sekunder...");
            Invoke("StartScanning", 2f); // Genstart scanning efter kort pause
        }
    }

    private void ConnectToDevice()
    {
        if (string.IsNullOrEmpty(_targetDeviceAddress)) return;

        var connectCmd = new ConnectToDevice(_targetDeviceAddress, OnConnected, OnDisconnected);
        _bleManager.QueueCommand(connectCmd);
        Debug.Log($"Forbinder til {_targetDeviceAddress}...");
    }

    private void OnConnected(string address)
    {
        _isConnected = true;
        Debug.Log($"Forbundet til {address}. Abonnerer på data...");
        SubscribeToCharacteristic();
    }

    private void OnDisconnected(string address)
    {
        _isConnected = false;
        Debug.Log($"Afbrudt fra {address}. Starter ny scanning...");
        _targetDeviceAddress = null;
        Invoke("StartScanning", 2f); // Genstart scanning
    }

    private void SubscribeToCharacteristic()
    {
        var subscribeCmd = new SubscribeToCharacteristic(_targetDeviceAddress, ServiceUuid, CharacteristicUuid, OnDataReceived);
        _bleManager.QueueCommand(subscribeCmd);
        Debug.Log("Abonneret på characteristic – venter på data fra Arduino...");
    }

    private void OnDataReceived(byte[] bytes)
    {
        data = Encoding.UTF8.GetString(bytes).Trim(); // Trim fjerner eventuelle \n eller mærkelige tegn
        Debug.Log($"Modtaget data via BLE: {data}");

        string[] parts = data.Split(',');
        if (parts.Length >= 2)
        {
            humidity = parts[0].Trim();
            temperature = parts[1].Trim();
            Debug.Log($"Opdateret: Fugtighed = {humidity}%, Temperatur = {temperature}°C");
        }
        else
        {
            Debug.LogWarning("Ugyldigt dataformat modtaget: " + data);
        }
    }
}