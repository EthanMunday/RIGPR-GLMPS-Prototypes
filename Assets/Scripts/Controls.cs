using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Controls : MonoBehaviour
{
    [SerializeField] Transform plane;
    [SerializeField] Camera cam;
    [SerializeField] GameObject redBlock;
    [SerializeField] GameObject blueBlock;
    [SerializeField] GameObject yellowBlock;
    [SerializeField] GameObject whiteBlock;
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
                placeBlockBasedOnKeyDown(targetPos);
            }
            else if (Physics.Raycast(ray, out hit, float.PositiveInfinity, ~SynergySpheres) && hit.transform.gameObject.CompareTag("Block"))
            {
                DestroyImmediate(hit.transform.gameObject);
                FindFirstObjectByType<ZoneCreator>().RefreshZones();
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

    void placeBlockBasedOnKeyDown(Vector3 targetPos)
    {
        GameObject theBlock;
        if (Input.GetKey(KeyCode.Alpha1))
        {
            theBlock = Instantiate(redBlock, targetPos, Quaternion.identity);
            theBlock.GetComponent<BlockInformation>().colour = "Red";
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            theBlock = Instantiate(blueBlock, targetPos, Quaternion.identity);
            theBlock.GetComponent<BlockInformation>().colour = "Blue";
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            theBlock = Instantiate(yellowBlock, targetPos, Quaternion.identity);
            theBlock.GetComponent<BlockInformation>().colour = "Yellow";
        }
        else
        {
            theBlock = Instantiate(whiteBlock, targetPos, Quaternion.identity);
            theBlock.GetComponent<BlockInformation>().colour = "White";
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