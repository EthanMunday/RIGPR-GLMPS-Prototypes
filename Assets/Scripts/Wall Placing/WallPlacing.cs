using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class WallPlacing  : DragClass
{


    [Header("ActiveData")]
    public Vector3 startPos;

    private bool isDragging = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public new void onMouseClick()
    {
        startPos = gridPos + new Vector3(0,0.5f,0);
        isDragging = true;
    }



    public new void onMouseOff()
    {
        Vector3 endPos = gridPos + new Vector3(0,0.5f,0);
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.parent = GameObject.FindWithTag("Workspace").transform.Find("Walls");

        isDragging = false;

        Vector3 wallPosition = (endPos + startPos);

        wallPosition.Scale(new Vector3(0.5f,0.5f,0.5f));

        wall.transform.position = wallPosition ;
        float thickness = 0.05f;
        wall.transform.localScale = new Vector3(thickness,1f,Vector3.Distance(endPos,startPos) - thickness);

        wall.transform.LookAt(endPos);



    }


    
}
