using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCreator : MonoBehaviour
{
    [SerializeField] GameObject checkSphere;
    public void RefreshZones()
    {
        foreach(SynergyZone synergyZone in FindObjectsOfType<SynergyZone>())
        {
            Destroy(synergyZone.gameObject);
        }

        List<PlacedBlock> blockArray = new List<PlacedBlock>(FindObjectsOfType<PlacedBlock>());
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
            for (int i = 0; i < newZoneList.Count; i++)
            {
                blockArray.Remove(newZoneList[i]);
            }
            if (newZoneList.Count < 3) continue;
            sortedBlocks.Add(newZoneList);
        }

        for (int i = 0;i < sortedBlocks.Count;i++)
        {
            GameObject newSphere = Instantiate(checkSphere, sortedBlocks[i][0].transform.position, Quaternion.identity);
            SynergyZone newZone = newSphere.GetComponent<SynergyZone>();
            newZone.artifactType = sortedBlocks[i][0].GetComponent<BlockInformation>().colour;
            foreach(PlacedBlock block in sortedBlocks[i])
            {
                newZone.artifacts.Add(block.gameObject);
            }
            newZone.UpdateSphere();
        }
    }
}
