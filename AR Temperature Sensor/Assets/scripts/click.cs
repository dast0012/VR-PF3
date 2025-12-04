using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;

public class click : MonoBehaviour
{
    ArduinoConnector arduinoConnector;
    string temperatureValue = "Loading...";
    string humidityValue = "Loading...";

    public Button Vb_on;

    public InputField Hum;
    public InputField field;

    void Start()
    {
        Vb_on.onClick.AddListener(() => OnButtonPressed_on(Vb_on));
    }

    public void OnButtonPressed_on(Button Vb_on)
    {
        GetData_tem();
        GetData_hum();
        Debug.Log("Click");
    }

    void GetData_tem() => StartCoroutine(GetData_Coroutine1());
    void GetData_hum() => StartCoroutine(GetData_Coroutine());

    IEnumerator GetData_Coroutine1()
    {
        Debug.Log("Getting Data");
        field.text = "";
        temperatureValue = arduinoConnector.data;

        Debug.Log(temperatureValue);
        field.text = temperatureValue;
        yield break;
    }
    IEnumerator GetData_Coroutine()
    {
        Debug.Log("Getting Data");
        Hum.text = "";
        humidityValue = arduinoConnector.data;

        Debug.Log(humidityValue);
        Hum.text = humidityValue;
        yield break;
    }
}