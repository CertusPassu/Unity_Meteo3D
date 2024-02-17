using Meteo3D.Request;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using static Meteo3D.Request.WebRequest;

namespace Meteo3D.UI
{
    public class WeatherDisplay : MonoBehaviour
    {

        [SerializeField]
        private TMP_Text _text;

        private string _city;
        private string latlong;
        private bool isByCity = false;

        // Start is called before the first frame update
        void Start()
        {
            _text.text= string.Empty;
        }

        private void OnEnable()
        {
            GetCoordOnClick.OnClick += ByCoord;
            WebRequest.OnCityFound += DisplayCity;
            WebRequest.OnFetchCityFound += DisplayCity;
            WebRequest.OnWeatherFound += DisplayWeather;
        }
        private void OnDisable()
        {
            GetCoordOnClick.OnClick -= ByCoord;
            WebRequest.OnCityFound -= DisplayCity;
            WebRequest.OnFetchCityFound -= DisplayCity;
            WebRequest.OnWeatherFound -= DisplayWeather;
        }
        // Update is called once per frame
        void Update()
        {

        }

        public void DisplayCity(OpenStreetData city)
        {
            if (city != null)
            {
                _city = city.address.city;
                isByCity = true;
            }
        }
        public void DisplayCity(CityInfo city)
        {
            if (city != null && city.results.Count > 0)
            {
                _city = city.results[0].name;
                isByCity = true;
            }
        }
        public void ByCoord(float latiitude, float longitude)
        {
            isByCity = false;
            latlong = "Latitude : " + latiitude + ", Longitude : " + longitude;
        }
        public void DisplayWeather(WebRequest.RootWeather weather)
        {
            string bycity = isByCity ? "in " + _city : "at " + latlong;
            string weatherInfo = $"Today {bycity}:\n\n{weather.current_weather.ToString()}\nTemperature : {weather.current_weather.temperature} °C";
            _text.text = weatherInfo;
        }
    }
}