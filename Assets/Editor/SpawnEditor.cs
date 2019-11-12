using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spawn))]
public class SpawnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Spawn spawn = (Spawn)target;
        if (GUILayout.Button("Generate World"))
        {
            spawn.GenerateWorld();
        }
    }
}
