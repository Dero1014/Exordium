using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PinchZoom : MonoBehaviour
{

    public float MaxZoom;
    public float MinZoom;

    public CinemachineVirtualCamera CM;
    public float ZoomDamp;

    void Start()
    {
        
    }

    float _oldDistance;
    bool _oneShot = false;
    void Update()
    {
        if (Input.touchCount >= 2)
        {
            Vector2 touch0, touch1;
            float distance;
            touch0 = Input.GetTouch(0).position;
            touch1 = Input.GetTouch(1).position;
            distance = Vector2.Distance(touch0, touch1);
            if (!_oneShot)
            {
                _oldDistance = distance;
                _oneShot = true;
            }

            CM.m_Lens.OrthographicSize += (distance - _oldDistance) * ZoomDamp;

            if (CM.m_Lens.OrthographicSize>MaxZoom)
            {
                CM.m_Lens.OrthographicSize = MaxZoom;
            }
            else if (CM.m_Lens.OrthographicSize < MinZoom)
            {
                CM.m_Lens.OrthographicSize = MinZoom;
            }

            _oldDistance = distance;
        }
        else
        {
            _oneShot = false;
        }
    }
}
