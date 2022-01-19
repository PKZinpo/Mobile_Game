using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCoin : MonoBehaviour {


    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") return;

        Debug.Log("[EnergyCoin] Energy coin aqcuired");

        Destroy(gameObject);
    }

}
