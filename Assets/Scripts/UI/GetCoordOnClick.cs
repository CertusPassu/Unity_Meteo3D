using System;
using UnityEngine;

public class GetCoordOnClick : MonoBehaviour
{

    public static event Action<float, float> OnClick;
    public static event Action<Vector3> OnClickCoord;

    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GetCoordOnClick rotEarth = hit.collider.gameObject.GetComponent<GetCoordOnClick>();
            if (rotEarth != null)
            {
                OnClickCoord?.Invoke(hit.point);
                float rayon = hit.point.magnitude;
                Vector3 localHitPoint = transform.InverseTransformPoint(hit.point);

                float latitude = Mathf.Asin(localHitPoint.y / rayon) * Mathf.Rad2Deg;
                float longitude = Mathf.Atan2(localHitPoint.x, -localHitPoint.z) * Mathf.Rad2Deg;

                OnClick?.Invoke(latitude, longitude);
            }
        }
    }
}
