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

    [HideInInspector] public bool randomizeGravityOn;
    [HideInInspector] public bool gravityOn;
    [HideInInspector] public bool randomizeMovement;
}

#if UNITY_EDITOR
[CustomEditor(typeof(Spawnable))]
public class Spawnable_Editor : Editor {

    public override void OnInspectorGUI() {

        DrawDefaultInspector();

        Spawnable script = (Spawnable)target;

        script.randomizeGravityOn = EditorGUILayout.Toggle("Random Gravity", script.randomizeGravityOn);

        if (!script.randomizeGravityOn) {
            script.gravityOn = EditorGUILayout.Toggle("Gravity On", script.gravityOn);
            script.randomizeMovement = EditorGUILayout.Toggle("Randomize Movement", script.randomizeMovement);
        }
    }
}


#endif
