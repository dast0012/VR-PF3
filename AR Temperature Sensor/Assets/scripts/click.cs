using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vuforia;

public class click : MonoBehaviour
{
    public Button Vb_on;

    public InputField Hum;
    public InputField field;

    void Start()
    {

        Vb_on.onClick.AddListener(() => OnButtonPressed_on(Vb_on));
        
        // Vb_on.RegisterOnButtonPressed(OnButtonPressed_on);
        // GameObject.Find("GetButton").GetComponent<Button>().onClick.AddListener(GetData);
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
        /*
        // Getting the data of the temperature from blynk server
        string uri = "http://blynk-cloud.com/vKqIp55UdG2GoZcvY3un4sPyQpoxgnG3/get/v0";
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                field.text = request.error;
            else
            {

                // Display the results as text
                field.text = request.downloadHandler.text;
                field.text = field.text.Substring(2, 2);
            }
        }
        */
        string dummyTemperature = "Example value 20";
        Debug.Log(dummyTemperature);
        field.text = dummyTemperature;
        yield break;
    }
    IEnumerator GetData_Coroutine()
    {
        Debug.Log("Getting Data");
        Hum.text = "Loading...";
        /*
        // Getting the data of the humidity from blynk server
        string uri = "http://blynk-cloud.com/vKqIp55UdG2GoZcvY3un4sPyQpoxgnG3/get/v1";
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Hum.text = request.error;
            else
            {
                // Display the results as text
                Hum.text = request.downloadHandler.text;
                Hum.text = Hum.text.Substring(2, 2);
            }
        }
        */
        string dummyHumidity = "Example value 20";
        Debug.Log(dummyHumidity);
        Hum.text = dummyHumidity;
        yield break;
    }
}