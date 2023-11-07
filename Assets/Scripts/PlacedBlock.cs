using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlacedBlock : MonoBehaviour
{
    public SO_ArtefactBase artefactData;
    [HideInInspector] public SynergyZone synergyZone;
    float radius = 3.0f;

    private void Awake()
    {
        if (artefactData == null)
        {
            Destroy(gameObject);
            return;
        }
        GetComponent<MeshFilter>().mesh = artefactData.artefactMesh;
        GetComponent<MeshRenderer>().material = artefactData.artefactMaterial;
        transform.position += artefactData.objectPositionOffset;
        transform.localScale = artefactData.objectScaleSize;
        transform.rotation = Quaternion.Euler(artefactData.objectScaleSize);
    }
    public void ContainsList(ref List<PlacedBlock> currentList)
    {
        currentList.Add(this);
        List<PlacedBlock> blocks = new List<PlacedBlock>(FindObjectsOfType<PlacedBlock>());
        string currentTag = artefactData.artefactType;
        foreach (var currentBlock in blocks)
        {
            float distance = Vector3.Distance(currentBlock.transform.position, transform.position);
            string checkTag = currentBlock.artefactData.artefactType;
            if (distance > radius || distance == 0.0f) continue;
            if (checkTag != currentTag) continue;
            if (currentList.Contains(currentBlock)) continue;
            currentBlock.ContainsList(ref currentList);
        }
    }

    public float GetValue()
    {
        if (synergyZone == null) return artefactData.revenueGenerated;
        float value = artefactData.revenueGenerated;
        float multiplier = 1.3f;
        for (int i = 0; i < synergyZone.artifacts.Count - 1; i++) 
        {
            multiplier *= 1.3f;
        }
        return value * multiplier;
    }
}   
