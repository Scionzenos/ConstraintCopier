using UnityEngine;
using UnityEditor;
#pragma warning disable IDE0090 // Use 'new(...)'
[CustomEditor(typeof(ConstraintCopier))]
public class RotationConstraintCopyCustomInspector : Editor 
{
    int constraintType;
    string[] constraintTypeArray = { "Parent Constraint", "Rotation Constraint", "Position Constraint" };
    public override void OnInspectorGUI()
    {
        constraintType = GUILayout.Toolbar(constraintType, constraintTypeArray);
        ConstraintCopier script = (ConstraintCopier)target;
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("sourceRoot"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("targetRoot"));
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Create Constraints"))
        {
            script.CreateConstraintRelationship(constraintType);
        }
    }

    [MenuItem("Tools/Scionzenos/Constraint Copier")]
    public static void EnableConstraintCopier()
    {
        GameObject setupGameobject = new GameObject("Constraint Copier");
        setupGameobject.AddComponent<ConstraintCopier>();
    }
}