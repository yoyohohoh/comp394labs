// A Quest stores information about a quest: its name, and its objectives.
// CreateAssetMenu makes the Create Asset menu contain an entry that
// creates a new Quest asset.
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest", order = 100)]
public class Quest : ScriptableObject
{
    // Represents the status of objectives and/or quests
    public enum Status
    {
        NotYetComplete,  // the objective or quest has not yet been completed
        Complete,        // the objective or quest has been successfully completed
        Failed           // the objective or quest has failed
    }

    public string questName; // The name of the quest

    // The list of objectives that form this quest
    public List<Objective> objectives;

    // Objectives are the specific tasks that make up a quest.
    [System.Serializable]
    public class Objective
    {
        // The visible name that's shown to the player.
        public string name = "New Objective";

        // If true, the quest can be completed without this objective
        public bool optional = false;

        // If false, the objective will not be shown to the user if it's
        // not yet complete. (It will be shown if it's Complete or Failed.)
        public bool visible = true;

        // The status of the objective when the quest begins. Usually this
        // will be "not yet complete," but you might want an objective that
        // starts as Complete and can be Failed.
        public Status initialStatus = Status.NotYetComplete;
    }
}
