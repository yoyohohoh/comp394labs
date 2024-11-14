#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
using UnityEngine;
#endif

// Combines a quest, a quest's objective, and status
[System.Serializable]
public class ObjectiveTrigger
{
    // The quest that we're referring to
    public Quest quest;

    // The status we want to apply to the objective
    public Quest.Status statusToApply;

    // The location of this objective in the quest's objective list
    public int objectiveNumber;

    public void Invoke()
    {
        var manager = Object.FindObjectOfType<QuestManager>();

        if (manager == null)
        {
            Debug.LogError("ObjectiveTrigger: No QuestManager found in the scene.");
            return;
        }

        if (quest == null)
        {
            Debug.LogError("ObjectiveTrigger: No quest assigned to ObjectiveTrigger.");
            return;
        }

        Debug.Log($"ObjectiveTrigger: Invoking UpdateObjectiveStatus on quest '{quest.name}', objective number '{objectiveNumber}', with status '{statusToApply}'.");
        manager.UpdateObjectiveStatus(quest, objectiveNumber, statusToApply);
    }

}

#if UNITY_EDITOR
// Custom property drawers override how a type of property appears in the Inspector.
[CustomPropertyDrawer(typeof(ObjectiveTrigger))]
public class ObjectiveTriggerDrawer : PropertyDrawer
{
    // Called when Unity needs to draw an ObjectiveTrigger property in the Inspector.
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Wrap this in Begin/EndProperty to ensure that undo works on the entire ObjectiveTrigger property
        EditorGUI.BeginProperty(position, label, property);

        // Get a reference to the three properties in the ObjectiveTrigger
        var questProperty = property.FindPropertyRelative("quest");
        var statusProperty = property.FindPropertyRelative("statusToApply");
        var objectiveNumberProperty = property.FindPropertyRelative("objectiveNumber");

        // Spacing between lines
        var lineSpacing = 2;

        // Calculate the rectangle for each line
        var firstLinePos = position;
        firstLinePos.height = base.GetPropertyHeight(questProperty, label);

        var secondLinePos = position;
        secondLinePos.y = firstLinePos.y + firstLinePos.height + lineSpacing;
        secondLinePos.height = base.GetPropertyHeight(statusProperty, label);

        var thirdLinePos = position;
        thirdLinePos.y = secondLinePos.y + secondLinePos.height + lineSpacing;
        thirdLinePos.height = base.GetPropertyHeight(objectiveNumberProperty, label);

        // Draw the quest and status properties
        EditorGUI.PropertyField(firstLinePos, questProperty, new GUIContent("Quest"));
        EditorGUI.PropertyField(secondLinePos, statusProperty, new GUIContent("Status"));

        // Draw a custom property for the objective
        thirdLinePos = EditorGUI.PrefixLabel(thirdLinePos, new GUIContent("Objective"));

        var quest = questProperty.objectReferenceValue as Quest;

        if (quest != null && quest.objectives.Count > 0)
        {
            // Get the name of each objective as an array
            var objectiveNames = quest.objectives.Select(o => o.name).ToArray();

            // Get the index of the currently selected objective
            var selectedObjective = objectiveNumberProperty.intValue;

            // Reset to the first objective if out of bounds
            if (selectedObjective >= quest.objectives.Count)
            {
                selectedObjective = 0;
            }

            // Draw the popup and update the selection if changed
            var newSelectedObjective = EditorGUI.Popup(thirdLinePos, selectedObjective, objectiveNames);
            if (newSelectedObjective != selectedObjective)
            {
                objectiveNumberProperty.intValue = newSelectedObjective;
            }
        }
        else
        {
            // Draw a disabled popup as a visual placeholder
            using (new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUI.Popup(thirdLinePos, 0, new[] { "-" });
            }
        }

        EditorGUI.EndProperty();
    }

    // Calculate the height of this property.
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Number of lines and spacing
        var lineCount = 3;
        var lineSpacing = 2;
        var lineHeight = base.GetPropertyHeight(property, label);

        return (lineHeight * lineCount) + (lineSpacing * (lineCount - 1));
    }
}
#endif
