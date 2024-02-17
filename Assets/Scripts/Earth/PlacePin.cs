using Meteo3D.Earth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePin : MonoBehaviour
{
    private Renderer _renderer;
    private void Start()
    {
       _renderer = GetComponentInChildren<Renderer>();
        _renderer.enabled = false;
    }
    private void OnEnable()
    {
        GetCoordOnClick.OnClickCoord += Place;
        RotateEarth.OnRotate += Place;
    }
    private void OnDisable()
    {
        GetCoordOnClick.OnClickCoord -= Place;
        RotateEarth.OnRotate -= Place;
    }


    public void Place(Vector3 pos)
    {
     
        _renderer.enabled = true;
        transform.position = pos;
        
        transform.LookAt(transform.parent.position);
        transform.rotation *= Quaternion.Euler(-90f, 0f, 0f);
    }
}
