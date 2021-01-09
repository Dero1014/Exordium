#if UNITY_EDITOR 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEditor;
using UnityEditor.Build;

public class AnayliticStartUp : MonoBehaviour
{

    private void Start()
    {
        AnalyticsResult result = Analytics.CustomEvent(
            "GameStarted", 
            new Dictionary<string, object> {

                { "Platform", EditorUserBuildSettings.activeBuildTarget},
                { "Local Time", LocalTime()}

            }
        );

    }
   
    int LocalTime()
    {
        int hour = System.DateTime.Now.Hour;
        return hour;
    }

}

#endif
