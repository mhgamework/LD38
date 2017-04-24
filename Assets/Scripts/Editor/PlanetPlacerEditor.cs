using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlanetPlacer))]
public class PlanetPlacerEditor : Editor
{
    void OnSceneGUI()
    {
        var script = (PlanetPlacer)target;
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
