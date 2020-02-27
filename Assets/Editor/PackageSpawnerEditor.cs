using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PackageSpawner), true)]
    public class PackageSpawnerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var packageSpawner = (PackageSpawner) target;

            if (GUILayout.Button(packageSpawner.Spawning ? "Stop" : "Start"))
            {
                if (packageSpawner.Spawning)
                {
                    packageSpawner.StopSpawner();
                }
                else
                {
                    packageSpawner.StartSpawner();
                }
            }

            GUI.enabled = false;
            EditorGUILayout.Toggle("Spawning", packageSpawner.Spawning);
            GUI.enabled = true;
        }
    }
}
