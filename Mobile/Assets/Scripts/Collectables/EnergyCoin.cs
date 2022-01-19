using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCoin : MonoBehaviour, ITrigger {
    public void Trigger() {
        Debug.Log("[EnergyCoin] Energy coin aqcuired");
        Destroy(gameObject);
    }
}
