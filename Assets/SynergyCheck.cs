using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockGroup {
    public List<GameObject> children;
}

public class mergeTable {
    public int group1;
    public int group2;
}


public class SynergyCheck : MonoBehaviour
{
  [SerializeField] GameObject checkSphere;

    
    public void UpdateZone(string currentTag,float radius)
    {


        List<BlockGroup> groups = new List<BlockGroup>();

        GameObject existingSphere = null;



        List<GameObject> children = new List<GameObject>();

   

    foreach (Transform child in GameObject.Find("Zones").transform){

        string colour = child.gameObject.GetComponent<ZoneInformation>().colour;

        if (colour == currentTag) {

            Destroy(child.gameObject);
        }
    }


        foreach (Transform child in GameObject.Find("Blocks").transform){

            // Pre-screen the list by only selecting blocks of a given tag
            string colour = child.gameObject.GetComponent<BlockInformation>().colour;

            if (colour == currentTag) {

                children.Add(child.gameObject);
            }
        }
        // Once we've screened for just a specific kind of block, we can now iterate through all of these blocks

        for (int x = 0; x < children.Count; x++){
            List<GameObject> group = new List<GameObject>();

            GameObject childA = children[x];
            Vector3 childPositionA = childA.transform.position;

            group.Add(childA); // Add the first object 

            for (int y = 0; y < children.Count; y++){
                
                GameObject childB = children[y];

                if(childB == childA){
                    continue;
                };

                Vector3 childPositionB = childB.transform.position;
                //Debug.Log(Vector3.Distance(childPositionA,childPositionB));

                if(Vector3.Distance(childPositionA,childPositionB) < radius){
                    group.Add(childB);


                }

            }

             // Then we need to merge these all into groups.

            int addToGroup = -1;

            List<mergeTable> tablesToMerge = new List<mergeTable>();

            for (int i = 0; i< groups.Count; i++){

                
                foreach(GameObject child in groups[i].children){ // For all existing groups

                    if (groups[i].children.Contains(childA)){
                        if (addToGroup != -1) {

                            mergeTable newTable = new mergeTable();

                            newTable.group1 = addToGroup;
                            newTable.group2 = i;

                            tablesToMerge.Add(newTable);


                        }


                        addToGroup = i;

                    }

                }

            }

            BlockGroup newGroup3 = new BlockGroup();
            newGroup3.children = group;

            if (tablesToMerge.Count == 0){

               groups.Add(newGroup3);

            }
            else
            {
                
            List<int> indexesToRemove = new List<int>();

            foreach(mergeTable MergeTable in tablesToMerge){
                int group1 =  MergeTable.group1;
                int group2 = MergeTable.group2;



                if(indexesToRemove.Contains(group1) == false){
                    List<GameObject> newGroup = group;

                    for (int i = 0; i < groups[group1].children.Count; i++){

                        if(group.Contains(groups[group1].children[i])){
                            continue;
                        }

                        newGroup.Add(groups[group1].children[i]);

                    }

                    group = newGroup;

                    indexesToRemove.Add(group1);
                }

                if(indexesToRemove.Contains(group2) == false){
                    List<GameObject> newGroup = group;

                    for (int i = 0; i< groups[group2].children.Count; i++){

                        if(group.Contains(groups[group2].children[i])){
                            continue;
                        }

                        newGroup.Add(groups[group2].children[i]);

                    }

                    group = newGroup;
                    indexesToRemove.Add(group2);
                }
            }

            List<BlockGroup> newGroupList = new List<BlockGroup>();

            for (int i = 0; i< groups.Count; i++){
                if(indexesToRemove.Contains(i)){


                    continue;
                }
                newGroupList.Add(groups[i]); 
            }

            groups = newGroupList;

            groups.Add(newGroup3);
        }
        
    }
        Debug.Log(groups.Count);







    foreach (BlockGroup group in groups)
    {
        
        if (group.children.Count == 0 ){
            continue;
        }

        Vector3 circleCenter = new Vector3(0.0f,0.0f,0.0f);

        foreach(GameObject block in group.children) {
            circleCenter = circleCenter + block.transform.position;
        }

        circleCenter = circleCenter/group.children.Count;


        float biggestMagnitude = 0;


        foreach(GameObject block in group.children) {
            float magnitude = Vector3.Distance(circleCenter,block.transform.position);

            if(magnitude > biggestMagnitude){
                biggestMagnitude = magnitude;
            }
        }

        // BiggestMagnitude becomes our radius, but we'll add .5 to it to make sure everything is covered

        biggestMagnitude = biggestMagnitude + 0.5f;

        // because biggestMagnitude is a radius, we need to transform it into the circumference


        float circumference = biggestMagnitude*2;

        GameObject theSphere = Instantiate(checkSphere,circleCenter,Quaternion.identity);
        theSphere.transform.localScale = new Vector3(circumference, circumference, circumference);
        theSphere.GetComponent<ZoneInformation>().colour = currentTag;
        theSphere.transform.parent = GameObject.Find("Zones").transform;
    }
    }

 
}
