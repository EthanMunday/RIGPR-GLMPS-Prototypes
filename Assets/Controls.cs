using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Controls : MonoBehaviour
{
    [SerializeField] Transform plane;
    [SerializeField] Camera cam;
    [SerializeField] GameObject block;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] NavMeshSurface surface;
    [SerializeField] LayerMask SynergySpheres;
    bool meshUpdate = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, ~SynergySpheres) && hit.transform == plane)
            {
                Vector3 targetPos = hit.point;
                targetPos.x = Mathf.Round(targetPos.x);
                targetPos.y += 0.5f;
                targetPos.z = Mathf.Round(targetPos.z);
                Instantiate(block, targetPos, Quaternion.identity);
            }
            else if (Physics.Raycast(ray, out hit, float.PositiveInfinity, ~SynergySpheres) && hit.transform.gameObject.CompareTag("Block"))
            {
                Destroy(hit.transform.gameObject);
            }
            meshUpdate = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, ~SynergySpheres) && hit.transform == plane)
            {
                agent.SetDestination(hit.point);
            }
        }
    }
    void LateUpdate()
    {
        if (meshUpdate)
        {
            surface.BuildNavMesh();
            meshUpdate = false;
        }
    }
}