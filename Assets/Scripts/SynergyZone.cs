using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SynergyZone : MonoBehaviour
{
    public List<GameObject> artifacts;
    public string artifactType;

    public void UpdateSphere()
    {
        foreach (GameObject currentObject in artifacts)
        {
            PlacedBlock block = currentObject.GetComponent<PlacedBlock>();
            block.synergyZone = this;
        }
        Vector3 centralPoint = transform.position;
        float maxDistance = 0f;
        foreach (GameObject currentObject in artifacts)
        {
            Vector3 v = currentObject.transform.position;
            centralPoint += v;
        }
        transform.position = centralPoint / artifacts.Count;

        foreach (GameObject currentObject in artifacts)
        {
            float f = Vector3.Distance(currentObject.transform.position, transform.position);
            if (f > maxDistance) maxDistance = f;
        }
        maxDistance += 0.5f;
        maxDistance *= 2;
        maxDistance += 1f;
        //Debug.Log(sphereScale);
        transform.localScale = new Vector3(maxDistance, maxDistance, maxDistance);
    }

    public SynergyZone()
    {
        artifacts = new List<GameObject>();
    }
}
