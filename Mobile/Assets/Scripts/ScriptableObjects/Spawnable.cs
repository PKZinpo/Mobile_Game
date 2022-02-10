using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Spawnable", menuName = "SpawnableObject", order = 1)]
public class Spawnable : ScriptableObject {

    public GameObject[] objects;

    public int parentSpawnIndexMin;
    public int parentSpawnIndexMax;
    
    public bool gravityOn;
    public bool randomGravity;
}

#if UNITY_EDITOR
[CustomEditor(typeof(Spawnable))]
public class Spawnable_Editor : Editor {

    public override void OnInspectorGUI() {

        DrawDefaultInspector();

        Spawnable script = (Spawnable)target;

        script.randomGravity = EditorGUILayout.Toggle("Random Gravity", script.randomGravity);

        if (script.randomGravity) {
            //script.gravityOn = EditorGUILayout.
        }

    }


}


#endif
