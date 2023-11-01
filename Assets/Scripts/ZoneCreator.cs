using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCreator : MonoBehaviour
{
    [SerializeField] GameObject checkSphere;
    public void RefreshZones(string colour)
    {
        foreach(SynergyZone synergyZone in FindObjectsOfType<SynergyZone>())
        {
            if (synergyZone.artifactType != colour) continue;
            Destroy(synergyZone.gameObject);
        }

        List<PlacedBlock> blockArray = new List<PlacedBlock>(FindObjectsOfType<PlacedBlock>());

        foreach (PlacedBlock block in FindObjectsOfType<PlacedBlock>())
        {
            if (block.GetComponent<BlockInformation>().colour == colour) continue;
            blockArray.Remove(block);
        }
        List<List<PlacedBlock>> sortedBlocks = new List<List<PlacedBlock>>();
        while (blockArray.Count > 0)
        {
            bool continueWhile = false;
            for (int i = 0; i < sortedBlocks.Count; i++)
            {
                if (sortedBlocks[i].Contains(blockArray[0]))
                {
                    blockArray.RemoveAt(0);
                    continueWhile = true;
                    break;
                }
            }
            if (continueWhile) continue;

            List<PlacedBlock> newZoneList = new List<PlacedBlock>();
            blockArray[0].ContainsList(ref newZoneList);
            newZoneList.ForEach(block => blockArray.Remove(block));
            if (newZoneList.Count < 3) continue;
            sortedBlocks.Add(newZoneList);
        }

        for (int i = 0;i < sortedBlocks.Count;i++)
        {
            GameObject newSphere = Instantiate(checkSphere, Vector3.zero, Quaternion.identity);
            SynergyZone newZone = newSphere.GetComponent<SynergyZone>();
            newZone.artifactType = sortedBlocks[i][0].GetComponent<BlockInformation>().colour;
            sortedBlocks[i].ForEach(block => newZone.artifacts.Add(block.gameObject));
            newZone.UpdateSphere();
        }
    }
}
