using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyZone : MonoBehaviour
{
    public List<GameObject> artifacts;
    public string artifactType;

    public void UpdateSphere()
    {
        Vector3 centralPoint = transform.position;
        float minX = transform.position.x;
        float maxX = transform.position.x;
        float minZ = transform.position.z;
        float maxZ = transform.position.z;
        float sphereScale = 0;
        foreach (GameObject currentObject in artifacts)
        {
            Vector3 v = currentObject.transform.position;
            centralPoint += v;
            if (minX > v.x)
            {
                minX = v.x;
            }
            else if (maxX < v.x)
            {
                maxX = v.x;
            }
            if (minZ > v.z)
            {
                minZ = v.z;
            }
            else if (maxZ < v.z)
            {
                maxZ = v.z;
            }
        }
        float difX = maxX - minX;
        float difZ = maxZ - minZ;
        //Debug.Log(difX + " z " + difZ);
        if (difX >= difZ)
        {
            sphereScale = difX * 2;
        }
        else
        {
            sphereScale = difZ * 2;
        }
        //Debug.Log(sphereScale);
        transform.position = centralPoint / (artifacts.Count + 1);
        transform.localScale = new Vector3(sphereScale, sphereScale, sphereScale);
    }

    public SynergyZone()
    {
        artifacts = new List<GameObject>();
    }
}
