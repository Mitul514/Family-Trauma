using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIdReciever : MonoBehaviour
{
    public bool CanFetchTriggerID(List<DialogueTrigger> tiggersList, out DialogueTrigger trigger)
    {
        trigger = null;

        if (!TryGetComponent(out EntityType entityType))
            return false;

        foreach (DialogueTrigger tig in tiggersList)
        {
            if (entityType.EnititySO == tig.enititySO &&
                !PlayerPrefController.Instance.hasIdInListForKey(KeyConstants.NARRATIVE_KEY, tig.dialogueId))
            {
                trigger = tig;
                return true;
            }
        }

        return false;
    }
}
