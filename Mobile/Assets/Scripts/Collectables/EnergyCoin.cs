using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyCoin : MonoBehaviour, ITrigger {

    [SerializeField] private float amount;

    public void Trigger() {
        Debug.Log("[EnergyCoin] Energy coin acquired");
        GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<PlayerStatsManager>().AddEnergy(amount);
        Destroy(gameObject);
    }
}
