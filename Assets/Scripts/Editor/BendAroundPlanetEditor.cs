using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BendAroundPlanet))]
public class BendAroundPlanetEditor : Editor
{
    void OnSceneGUI()
    {
        var script = (BendAroundPlanet)target;
        Event e = Event.current;
        switch (e.type)
        {
            case EventType.keyDown:
                {
                    if (Event.current.keyCode == (KeyCode.A))
                    {
                        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                        script.PlaceObject(ray);
                    }
                    break;
                }
        }

    }
}
