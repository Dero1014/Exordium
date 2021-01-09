using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsMovementTracker : MonoBehaviour
{
    private Vector2 _distance;
    private Vector2 _lastPosition;

    private void Start()
    {
        _lastPosition = transform.position;
    }

    int _movedUnit = 0;
    int _eventsSent = 0;
    void Update()
    {
        _distance = _lastPosition - (Vector2)transform.position;

        if (_distance.magnitude >= 1)
        {
            _movedUnit++;
            _lastPosition = transform.position;
        }

        if (_movedUnit >= 10 && _eventsSent < 5)
        {
            _eventsSent++;
            AnalyticsOnMoved();
            _movedUnit = 0;
            _lastPosition = transform.position;
        }

    }

    void AnalyticsOnMoved()
    {
        AnalyticsResult result = Analytics.CustomEvent("Player Moved");

    }
}
