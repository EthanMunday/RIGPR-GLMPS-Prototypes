using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlacedBlock : MonoBehaviour
{
    [SerializeField] GameObject checkSphere;
    SynergyZone zone;
    void Start()
    {
        SynergeticRange(transform.position, 3);
    }

    void SynergeticRange(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        List<GameObject> closeBlocks = new List<GameObject>();
        string currentTag = GetComponent<BlockInformation>().colour;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Block") && hitCollider.transform.position - transform.position != Vector3.zero)
            {
                string checkTag = hitCollider.GetComponent<BlockInformation>().colour;
                if (checkTag == currentTag)
                {
                    closeBlocks.Add(hitCollider.gameObject);
                }
            }
            if (hitCollider.CompareTag("SynergySphere"))
            {
                SynergyZone currentZone = hitCollider.GetComponent<SynergyZone>();
                if (currentTag == currentZone.artifactType)
                {
                    currentZone.artifacts.Add(this.gameObject);
                    currentZone.artifactType = currentTag;
                    currentZone.UpdateSphere();
                    return;
                }    
            }
        }
        if(closeBlocks.Count >= 2)
        {
            GameObject theSphere = Instantiate(checkSphere,transform.position,Quaternion.identity);
            SynergyZone currentZone = theSphere.GetComponent<SynergyZone>();
            currentZone.artifacts.Add(this.gameObject);
            currentZone.artifactType = currentTag;
            foreach (GameObject x in closeBlocks)
            {
                currentZone.artifacts.Add(x);
                x.GetComponent<PlacedBlock>().zone = currentZone;
            }
            currentZone.UpdateSphere();
        }
    }
}   
