using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    Grid grid;
    bool[,,] gridData;
    GameObject box;
    Ray ray;
    RaycastHit rayhit;
    Camera camera;
    public LayerMask mask;

    void Start()
    {
        grid = FindFirstObjectByType<Grid>();
        box = FindFirstObjectByType<BoxCollider>().gameObject;
        camera = FindFirstObjectByType<Camera>();
        Debug.Log(grid);
        Debug.Log(box);
        Debug.Log(camera);
        gridData = new bool[20, 20, 20];
    }

    // Update is called once per frame
    void Update()
    {
        ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayhit, 1000f, mask))
        {
            box.transform.position = grid.CellToWorld(grid.WorldToCell(rayhit.point)) + new Vector3(0f,1f);

            if (Input.GetMouseButtonDown(0) && gridData[grid.WorldToCell(rayhit.point).x + 5, grid.WorldToCell(rayhit.point).y + 5, grid.WorldToCell(rayhit.point).z + 5] == false)
            {
                Debug.Log(grid.WorldToCell(rayhit.point));
                gridData[grid.WorldToCell(rayhit.point).x + 5, grid.WorldToCell(rayhit.point).y + 5, grid.WorldToCell(rayhit.point).z + 5] = true;
                GameObject x = GameObject.CreatePrimitive(PrimitiveType.Cube);
                x.transform.position = grid.CellToWorld(grid.WorldToCell(rayhit.point)) + new Vector3(0f, 1f);
            }
        }

    }
}
