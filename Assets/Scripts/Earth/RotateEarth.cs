using Meteo3D.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Meteo3D.Earth
{
    public class RotateEarth : MonoBehaviour
    {
        [SerializeField]
        private GameObject _cameraOffset;
        [SerializeField]
        private GameObject _pin;
        [SerializeField]
        private Transform _earthTransform;
        private Vector3 _mousePos;
        [SerializeField]
        private SpinPlanet spinBool;

        public static event Action<Vector3> OnRotate;
        // Start is called before the first frame update
        void Start()
        {

        }
        private void OnEnable()
        {
            WebRequest.OnCityFound += RotatePlanet;
            WebRequest.OnWeatherFound += RotatePlanet;
        }
        private void OnDisable()
        {
            WebRequest.OnCityFound -= RotatePlanet;
            WebRequest.OnWeatherFound -= RotatePlanet;
        }
        // Update is called once per frame
        void Update()
        {
            var ms = Mouse.current;
         
            if (ms.rightButton.isPressed)
            {
                Vector2 delta = ms.delta.ReadValue();
                if (spinBool.spin != false)
                    spinBool.ToggleSpin();
               

                transform.Rotate(Vector3.up * -delta.x);
                _cameraOffset.transform.Rotate(Vector3.forward * -delta.y);
                
                Vector3 rot = _cameraOffset.transform.rotation.eulerAngles;
               
                //rot.z = Mathf.Clamp(rot.z, -50f, 50f);
                //Debug.Log("apres : " + rot.z);
                //_cameraOffset.transform.rotation = Quaternion.Euler(rot);
                 
            }
        }
        public void RotatePlanet(WebRequest.CityInfo cityInfo)
        {
            RotatePlanet(cityInfo.results[0].latitude, cityInfo.results[0].longitude);
        }
        public void RotatePlanet(WebRequest.RootWeather weather)
        {
            RotatePlanet((float)weather.latitude, (float)weather.longitude);
        }
        public void RotatePlanet(float latitude, float longitude)
        {
            Vector3 euleursZ = new Vector3(0f, 0f, latitude);
            Vector3 euleursY = new Vector3(0f, longitude, 0f);

            transform.rotation = Quaternion.Euler(euleursY);
            _cameraOffset.transform.rotation = Quaternion.Euler(euleursZ);

            Vector3 dir = transform.position - Camera.main.transform.position;

           // Camera.main.transform.parent.rotation = Quaternion.identity;

            if (Physics.Raycast(Camera.main.transform.position, dir, out RaycastHit hit))
            {
                OnRotate?.Invoke(hit.point);
            }
            //float radius = GetComponentInChildren<SphereCollider>().radius;
            //Vector3 vect = new Vector3(
            //    x : Mathf.Cos(latitude * Mathf.Deg2Rad) * Mathf.Cos(longitude * Mathf.Deg2Rad),
            //    y : radius * Mathf.Sin(latitude * Mathf.Deg2Rad),
            //    z : radius * Mathf.Cos(latitude * Mathf.Deg2Rad) * Mathf.Sin(longitude * Mathf.Deg2Rad));
            

            //OnRotate?.Invoke(vect);
            
        }

        

    }
}
