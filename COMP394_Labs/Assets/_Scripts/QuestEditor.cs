using UnityEngine;
using UnityEditor;


#if UNITY_EDITOR
// Draw a custom editor that lets you build the list of objectives.
[CustomEditor(typeof(Quest))]
public class QuestEditor: Editor { 
    // Called when Unity wants to draw the Inspector for a quest.
    public override void OnInspectorGUI() {
        // Update the current object's (referred to as "serializedObject") pending changes (if any).
        serializedObject.Update();
        // Draw the name of the quest (PropertyField)
        EditorGUILayout.PropertyField(serializedObject.FindProperty("questName")
        , new GUIContent("Name"));
        // Draw a header (LabelField) for the list of objectives
        EditorGUILayout.LabelField("Objectives");
        // Get the property that contains the list of objectives
        var objectiveList = serializedObject.FindProperty("objectives");
        EditorGUI.indentLevel += 1;
        // Indent the objectives
        // For each objective in the list, draw an entry
        for
        (int i = 0; i < objectiveList.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            // Draw a single line of controls
            // Draw the objective itself (its name, and its flags)
            EditorGUILayout.PropertyField(objectiveList.GetArrayElementAtIndex(i)
            , includeChildren: true);
            // Draw a button that moves the item up in the list
            if(GUILayout.Button("Up", EditorStyles.miniButtonLeft, GUILayout.Width(25)))
            {
                objectiveList.MoveArrayElement(i, i - 1);
            }
            // Draw a button that moves the item down in the list
            if(GUILayout.Button("Down", EditorStyles.miniButtonMid, GUILayout.Width(40)))
            {
                objectiveList.MoveArrayElement(i, i + 1);
            }
            // Draw a button that removes (deletes) the item from the list
            if
            (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(25)))
            { objectiveList.DeleteArrayElementAtIndex(i); }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUI.indentLevel -= 1; // Remove the indentation
         // Draw a button at adds a new objective to the list
        if (GUILayout.Button("Add Objective")) { objectiveList.arraySize += 1; }
        serializedObject.ApplyModifiedProperties(); // Save any changes
    }
}
#endif
