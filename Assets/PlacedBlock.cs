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
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Block") && hitCollider.transform.position - transform.position != Vector3.zero)
            {
                closeBlocks.Add(hitCollider.transform.position);
            }
            if (hitCollider.CompareTag("SynergySphere"))
            {
                existingSphere = hitCollider.gameObject;
            }
        }
        if(closeBlocks.Count >= 2)
        {
            Destroy(existingSphere);
            Vector3 centralPoint = Vector3.zero;
            foreach (Vector3 v in closeBlocks)
            {
                centralPoint += v;
            }
            centralPoint += transform.position;
            centralPoint = centralPoint / (closeBlocks.Count + 1);
            Instantiate(checkSphere,centralPoint,Quaternion.identity);
        }
    }

    void pingedByOtherBlock()
    {

    }
}
