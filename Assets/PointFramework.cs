using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointFramework : MonoBehaviour
{
    [Header("Position and Grid")]
    public Vector3 worldPosition;
    public Vector3 gridPosition;
    public float increment;



    [Header("State")]
    public bool isDragging = false;
    public GameObject brushObject;

    public bool Enabled = false;


    [Header("Objects")]
    public PlayerInput inputManager;
    public Camera currentCamera;


    private Vector3 bindToGrid(Vector3 vector, float increment){
        
        //float x = Mathf.Round(vector.x);
        float x = vector.x - (vector.x % increment);

       // float z = Mathf.Round(vector.z);
        float z = vector.z - (vector.z % increment);

        return new Vector3(x,0.5f,z);
    }

    
    void MouseMove(){
        
            Vector3 mousePos = Input.mousePosition;
            RaycastHit hitResult;

            Ray ray = currentCamera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray.origin, ray.direction * 103, out hitResult)){
               // transform.position = bindToGrid(hitResult.point,increment);

               worldPosition = hitResult.point;
            };

    }


    // Start is called before the first frame update
    void Start()
    {

    }


    public void HandleClick(InputAction.CallbackContext context)
{

     if (context.started)
    {
 
        brushObject.GetComponent<WallPlacing>().onMouseClick();
    }
    else if (context.canceled)
    {
 
        brushObject.GetComponent<WallPlacing>().onMouseOff();
    }
}


    // Update is called once per frame
    void Update()
    {
        MouseMove();
        gridPosition = bindToGrid(worldPosition,increment);
        brushObject.GetComponent<DragClass>().onUpdate(gridPosition,worldPosition);
        brushObject.GetComponent<WallPlacing>().onUpdate(gridPosition,worldPosition);


    }
}
