using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class UnlockablesManager : MonoBehaviour
{
    private static UnlockablesManager instance = null;


    public static List<BoolWrapper> unlockableStates;

    [DataContract]
    public class BoolWrapper
    {
        [DataMember]
        public bool _bool = false;
    }

    public List<TriggerSpot> triggerSpotsUnlockables;

    public static UnlockablesManager Instance { get => instance; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        if (unlockableStates == null)
        {
            unlockableStates = new List<BoolWrapper>(); //false means its locked

            foreach (var item in triggerSpotsUnlockables)
            {
                unlockableStates.Add(new BoolWrapper());
            }


         //   Debug.Log("unlockableStates lenght " + triggerSpotsUnlockables.Count);
        }
        else
        {
           // Debug.Log("unlockableStates.length " + unlockableStates.Count);
            for (int i = 0; i < triggerSpotsUnlockables.Count; i++)
            {
                triggerSpotsUnlockables[i].gameObject.SetActive(!unlockableStates[i]._bool);
            }
        }
    }

    public void SignalTriggerSpotUnlocked(TriggerSpot triggerSpot)
    {
        unlockableStates[triggerSpotsUnlockables.IndexOf(triggerSpot)]._bool = true;
    }
}
