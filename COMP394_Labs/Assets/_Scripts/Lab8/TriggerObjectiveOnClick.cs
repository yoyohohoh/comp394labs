using UnityEngine;
using UnityEngine.EventSystems;

// Triggers an objective when an object is clicked.
public class TriggerObjectiveOnClick : MonoBehaviour, IPointerClickHandler
{
    // The objective to trigger, and how to trigger it.
    [SerializeField] ObjectiveTrigger objective = new ObjectiveTrigger();

    // Called when the player clicks on this object
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // We just completed or failed this objective!
        objective.Invoke();

        // Disable this component so that it doesn't get run twice
        this.enabled = false;
    }
}
