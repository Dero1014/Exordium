using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEditor;
public class AnayliticStartUp : MonoBehaviour
{

    private void Start()
    {
        AnalyticsResult result = Analytics.CustomEvent(
            "GameStarted", 
            new Dictionary<string, object> {

                { "Platform", GameStarted()},
                { "Local Time", LocalTime()}

            }
        );

    }
   
    BuildTarget GameStarted()
    {
        return EditorUserBuildSettings.activeBuildTarget;
    }

    int LocalTime()
    {
        int hour = System.DateTime.Now.Hour;
        return hour;
    }

}
