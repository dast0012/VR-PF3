using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;

public class click : MonoBehaviour
{
    public ArduinoConnector arduinoConnector;
    string temperatureValue = "Loading...";
    string humidityValue = "Loading...";

    public Button Vb_on;

    public InputField Hum;
    public InputField field;

    void Start()
    {
        Vb_on.onClick.AddListener(() => OnButtonPressed_on(Vb_on));

        Debug.Log("Arduino connector is: " + arduinoConnector);
        Debug.Log("Hum field is: " + Hum);
        Debug.Log("Temp field is: " + field);
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
        field.text = "Loading...";
        temperatureValue = "Temperature: " + arduinoConnector.temperature + " Celcius";

        Debug.Log(temperatureValue);
        field.text = temperatureValue;
        yield break;
        // yield return new WaitForSeconds(5);
    }
    IEnumerator GetData_Coroutine()
    {
        Debug.Log("Getting Data");
        Hum.text = "Loading...";
        humidityValue = $"Humidity {arduinoConnector.humidity}%";

        Debug.Log(humidityValue);
        Hum.text = humidityValue;
        yield break;
    }
}