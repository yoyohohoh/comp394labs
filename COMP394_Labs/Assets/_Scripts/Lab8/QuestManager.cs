using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Procedural;

public class QuestStatus
{
    // The underlying data object that describes the quest.
    public Quest questData;
    // The map of objective identifiers.
    public Dictionary<int, Quest.Status> objectiveStatuses;
    // The constructor. Pass a Quest to this to set it up.
    public QuestStatus(Quest questData)
    {
        // Store the quest info
        this.questData = questData;
        // Create the map of objective numbers to their status
        objectiveStatuses =new Dictionary<int, Quest.Status>();
        for(int i =0; i < questData.objectives.Count; i +=1)
        {
            var
            objectiveData = questData.objectives[i];
            objectiveStatuses[i] = objectiveData.initialStatus;
        }
    }
    // Returns the state of the entire quest.
    // If all nonoptional objectives are complete, the quest is complete.
    // If any nonoptional objective is failed, the quest is failed.
    // Otherwise, the quest is not yet complete.
    public Quest.Status questStatus{
        get{
            for(int i =0; i < questData.objectives.Count; i +=1)
            {
            
            var objectiveData = questData.objectives[i];
            // Optional objectives do not matter to the quest status
            if
            (objectiveData.optional)
            {
                continue;
            }
            var objectiveStatus = objectiveStatuses[i];
            // this is a mandatory objective
            if(objectiveStatus == Quest.Status.Failed)
            {
                // if a mandatory objective fails, the whole quest fails
                return Quest.Status.Failed;
            }
            else if
            (objectiveStatus != Quest.Status.Complete)
            {
                // if a mandatory objective is not yet complete,
                // the whole quest is not yet complete
                return Quest.Status.NotYetComplete;
            }
        }
// All mandatory objectives are complete, so the quest is complete
        return Quest.Status.Complete;
    }
}
// Returns a string containing the list of objectives, their
// statuses, and the status of the quest.
public override string ToString()
{
    var stringBuilder = new System.Text.StringBuilder();
    for(int i =0; i < questData.objectives.Count; i +=1   )
    {
        // Get the objective and its status
        var        objectiveData = questData.objectives[i];
        var        objectiveStatus = objectiveStatuses[i];
        // Don't show hidden objectives that haven't been finished
        if        (objectiveData.visible ==false        && objectiveStatus == Quest.Status.NotYetComplete)
        {
            continue            ;
        }
        // If this objective is optional, display "(Optional)" after its name
        if        (objectiveData.optional)
        {
            stringBuilder.AppendFormat(            "{0} (Optional) - {1}\n"
            , objectiveData.name, objectiveStatus.ToString());
        }
        else
        {
            stringBuilder.AppendFormat(            "{0} - {1}\n"
            , objectiveData.name, objectiveStatus.ToString());
        }
    }
    // Add a blank line followed by the quest status
    stringBuilder.AppendLine();
    stringBuilder.AppendFormat(    "Status: {0}"    ,    this    .questStatus.ToString());
    return    stringBuilder.ToString();
}
}
// Manages a quest.
public class QuestManager : MonoBehaviour
{
    // The quest that starts when the game starts.
    [SerializeField] Quest startingQuest;
    // A label to show the state of the quest in.
    [SerializeField] UnityEngine.UI.Text objectiveSummary = null;
    // Tracks the state of the current quest.
    QuestStatus activeQuest;
    // Start a new quest when the game starts
    void Start()
    {
        if (startingQuest != null)
        {
            Debug.Log("startingQuest != null");
            StartQuest(startingQuest); 
        }
    }
    // Begins tracking a new quest
    public void StartQuest(Quest quest)
    {
        activeQuest = new QuestStatus(quest);
        UpdateObjectiveSummaryText();
        Debug.LogFormat("Started quest {0}", activeQuest.questData.name);
    }
    // Updates the quest summary label
    void UpdateObjectiveSummaryText()
    {
        string label;
        if (activeQuest == null)
        {
            label = "No active quest.";
        }
        else
        {
            label = activeQuest.ToString();
        }
        objectiveSummary.text = label;
    }
    // Called by other objects when an objective has changed status
    public void UpdateObjectiveStatus(Quest quest, int objectiveNumber, Quest.Status status)
    {
        if (activeQuest == null)
        {
            Debug.LogError("UpdateObjectiveStatus: no quest is active");
            return;
        }
        if (activeQuest.questData != quest)
        {
            Debug.LogWarningFormat($"UpdateObjectiveStatus: quest{quest.questName}is not active.Ignoring.");
            return;
        }
        // Update the objective status
        activeQuest.objectiveStatuses[objectiveNumber] = status;
        // Update the display label
        UpdateObjectiveSummaryText();
    }
}
