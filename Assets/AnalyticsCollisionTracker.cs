using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class AnalyticsCollisionTracker : MonoBehaviour
{
    int _collisionTime;

    bool _canCollide = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_canCollide)
        {
            AnalyticsResult result = Analytics.CustomEvent("Collided");
            StartCoroutine(BetweenCollision());
            print(result);
        }
        
    }

    IEnumerator BetweenCollision()
    {
        _canCollide = false;
        yield return new WaitForSeconds(5);
        _canCollide = true;
    }

}
