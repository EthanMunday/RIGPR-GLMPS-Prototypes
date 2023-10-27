using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlacedBlock : MonoBehaviour
{
    void Start()
    {
        FindFirstObjectByType<ZoneCreator>().RefreshZones();
    }

    public void ContainsList(ref List<PlacedBlock> currentList)
    {
        currentList.Add(this);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
        string currentTag = GetComponent<BlockInformation>().colour;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Block") && hitCollider.transform.position - transform.position != Vector3.zero)
            {
                string checkTag = hitCollider.GetComponent<BlockInformation>().colour;
                if (checkTag != currentTag) continue;
                PlacedBlock checkBlock = hitCollider.GetComponent<PlacedBlock>();
                if (currentList.Contains(checkBlock)) continue;
                checkBlock.ContainsList(ref currentList);
            }
        }
    }
}   
