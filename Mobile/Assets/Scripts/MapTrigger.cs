using UnityEngine;

public class MapTrigger : MonoBehaviour, ITrigger {

    public void Trigger() {
        GameObject.FindGameObjectWithTag("MapGeneration").GetComponent<MapGeneration>().ChangeMapPrefab();
        GameObject.FindGameObjectWithTag("MapGeneration").GetComponent<MapGeneration>().GenerateMap(transform.parent.transform.position);

        if (GameObject.FindGameObjectWithTag("MapParent").GetComponent<MapShiftManager>() != null) {
            GameObject.FindGameObjectWithTag("MapParent").GetComponent<MapShiftManager>().MapShiftCheck();
        }
    }
}
