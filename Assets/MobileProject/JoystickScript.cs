using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickScript : MonoBehaviour
{
    public float MaxDistance;

    private List<RaycastResult> _hitObjects = new List<RaycastResult>();
    private Transform _objectToDrag;

    private Vector2 _origin;
    private Vector2 _distance;

    private bool _dragable= false;

    private float _horizontal = 0;
    private float _vertical = 0;

    public float HorizontalValue
    {
        get { return _horizontal; }
        set
        {
            _horizontal = _distance.normalized.x;
        }
    }

    public float VerticalValue
    {
        get { return _vertical; }
        set
        {
            _vertical = value;
        }
    }


    void Start()
    {
        _origin = transform.position;
    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            _objectToDrag = GetDraggableTransformUnderMouse();
            if (_objectToDrag != null)
            {
                _dragable = true;
            }
        }

        if (_dragable)
        {
            _distance = -(transform.position - Input.mousePosition);
            HorizontalValue = _distance.normalized.x;
            VerticalValue = _distance.normalized.y;

            if (_distance.magnitude > MaxDistance)
            {
                _objectToDrag.position = (_distance.normalized * MaxDistance) + (Vector2)transform.position;
            }
            else
            {
                _objectToDrag.position = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            

            if (_objectToDrag != null)
            {
                _distance = Vector2.zero;
                HorizontalValue = 0;
                VerticalValue = 0;
                _dragable = false;
                _objectToDrag.GetComponent<RectTransform>().localPosition = new Vector2(0,0);
            }
        }
    }

    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, _hitObjects);

        if (_hitObjects.Count <= 0) return null;
        else
        {
            if (_hitObjects[0].gameObject.tag == "Joy")
            {
                return _hitObjects[0].gameObject;
            }
        }


        return null;
    }

    private Transform GetDraggableTransformUnderMouse()
    {
        var clickedObject = GetObjectUnderMouse();

        // get top level object hit
        if (clickedObject != null)
        {
            return clickedObject.transform;
        }

        return null;
    }

}
