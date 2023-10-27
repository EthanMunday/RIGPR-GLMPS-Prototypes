using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlacedBlock : MonoBehaviour
{
    [SerializeField] GameObject checkSphere;
    void Start()
    {
        SynergeticRange(transform.position, 3);
    }

    void SynergeticRange(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        List<Vector3> closeBlocks = new List<Vector3>();
        GameObject existingSphere = null;
        string currentTag = GetComponent<BlockInformation>().colour;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Block") && hitCollider.transform.position - transform.position != Vector3.zero)
            {
                string checkTag = hitCollider.GetComponent<BlockInformation>().colour;
                if (checkTag == currentTag)
                {
                    closeBlocks.Add(hitCollider.transform.position);
                }
            }
            if (hitCollider.CompareTag("SynergySphere"))
            {
                existingSphere = hitCollider.gameObject;
            }
        }
        if(closeBlocks.Count >= 2)
        {
            Destroy(existingSphere);
            Vector3 centralPoint = transform.position;
            float minX = transform.position.x; 
            float maxX = transform.position.x;
            float minZ = transform.position.z; 
            float maxZ = transform.position.z;
            float sphereScale = 0;
            foreach (Vector3 v in closeBlocks)
            {
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
<<<<<<< Updated upstream
            centralPoint += transform.position;
            centralPoint = centralPoint / (closeBlocks.Count + 1);
            Instantiate(checkSphere,centralPoint,Quaternion.identity);
=======
            float difX = maxX - minX;
            float difZ = maxZ - minZ;
            Debug.Log(difX + " z " + difZ);
            if (difX >= difZ)
            {
                sphereScale = difX * 2;
            }
            else
            {
                sphereScale = difZ * 2;
            }
            Debug.Log(sphereScale);
            centralPoint = centralPoint / (closeBlocks.Count + 1);
            GameObject theSphere = Instantiate(checkSphere,centralPoint,Quaternion.identity);
            theSphere.transform.localScale = new Vector3(sphereScale, sphereScale, sphereScale);
>>>>>>> Stashed changes
        }
    }
}
