using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlacedBlock : MonoBehaviour
{
    float radius = 3.0f;
    public void ContainsList(ref List<PlacedBlock> currentList)
    {
        currentList.Add(this);
        List<PlacedBlock> blocks = new List<PlacedBlock>(FindObjectsOfType<PlacedBlock>());
        string currentTag = GetComponent<BlockInformation>().colour;
        foreach (var currentBlock in blocks)
        {
            float distance = Vector3.Distance(currentBlock.transform.position, transform.position);
            string checkTag = currentBlock.GetComponent<BlockInformation>().colour;
            if (distance > radius || distance == 0.0f) continue;
            if (checkTag != currentTag) continue;
            if (currentList.Contains(currentBlock)) continue;
            currentBlock.ContainsList(ref currentList);
        }
    }
}   
