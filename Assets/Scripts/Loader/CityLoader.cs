using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Meteo3D.Request.WebRequest;
using UnityEngine.Networking;

public class CityLoader : MonoBehaviour
{

    private Cities _cities;
    public Cities ListOfCities { get => _cities; }
    // Start is called before the first frame update
    void Start()
    {
        LoadCitiesJSON("cities.json");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void ReadJson(string json)
    {
        _cities = JsonUtility.FromJson<Cities>(json);
    }
    private void LoadCitiesJSON(string jsonFile)
    {
#if UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, jsonFile);
        Debug.Log(path);
        StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        reader.Close();
        ReadJson(json);
        Debug.Log(_cities.City[0].name);
#elif UNITY_WEBGL
        StartCoroutine(LoadCitiesJsonWR(jsonFile));
#endif
    }

    public IEnumerator LoadCitiesJsonWR(string jsonFile)
    {
        string uri = Path.Combine(Application.streamingAssetsPath, jsonFile);
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();
        byte[] result = webRequest.downloadHandler.data;
        string cityJSON = System.Text.Encoding.Default.GetString(result);
        ReadJson(cityJSON);
    }
}
