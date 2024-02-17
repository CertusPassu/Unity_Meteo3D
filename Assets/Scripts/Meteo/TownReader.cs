using Meteo3D.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Meteo3D.Request.WebRequest;
using static UnityEngine.Rendering.DebugUI;

namespace Meteo3d.Meteo
{
    public class TownReader : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _inputField;

        [SerializeField]
        private List<GameObject> _buttons;

        [SerializeField]
        private CityLoader _cityLoader;
        [SerializeField]
        private SpinPlanet spinBool;

        public static event Action<string> OnTownSubmitted;
        public static event Action<float, float> OnCoordTownSubmitted;

        // Start is called before the first frame update
        void Start()
        {
        }

        private void OnEnable()
        {
            WebRequest.OnCityFound += FillTown;

        }
        private void OnDisable()
        {
            WebRequest.OnCityFound -= FillTown;
        }

        public void ReadTown(string value)
        {
            OnTownSubmitted?.Invoke(value);
        }
        public void FillTown(CityInfo city)
        {
            Debug.Log("Complete");
            //if (city.results.Count > 0)
            //{
            //    _inputField.text = city.results[0].name;
            //    Debug.Log("Result : " + city.results[0].name);
            //}
        }

        public void FillField(TMP_Text text)
        {
            _inputField.text = text.text;
            List<City> cities = _cityLoader.ListOfCities.City;
            List<Vector2> vec = cities.Where(
                                            town => town.name.CompareTo(text.text.Split(", ")[0]) == 0 && town.country.CompareTo(text.text.Split(", ")[1]) == 0)
                                        .Select(town => new Vector2(town.lat, town.lng)).ToList();
            if (vec.Count > 0)
            {
                OnCoordTownSubmitted?.Invoke(vec[0].x, vec[0].y);
                if (spinBool.spin != false)
                    spinBool.ToggleSpin();
            }
        }

        public void AutoComplete(string value)
        {
            //Debug.Log("Auto complete : " + value);

            // List<string> result = _autoComplete.Where(city => city.Contains(value, StringComparison.OrdinalIgnoreCase)).OrderBy(c => c).ToList();
            if (value.Length >= 3)
            {
                List<City> cities = _cityLoader.ListOfCities.City;
                List<City> result2 = cities.Where(town => town.name.StartsWith(value, StringComparison.OrdinalIgnoreCase)).OrderBy(c => c.name).ToList();
               
                for (int i = 0; i < _buttons.Count; i++)
                {
                    if (result2.Count > i && !string.IsNullOrEmpty(value))
                    {
                        _buttons[i].SetActive(true);
                        _buttons[i].GetComponentsInChildren<TMP_Text>()[0].text = result2[i].name + ", " + result2[i].country;
                    }
                    else
                    {
                        _buttons[i].SetActive(false);
                    }
                }
            }
            else
            {
                for (int i = 0; i < _buttons.Count; i++)
                {
                    _buttons[i].SetActive(false);
                }
            }
        }

    }

}