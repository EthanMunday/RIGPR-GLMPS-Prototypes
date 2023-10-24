using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerScript : MonoBehaviour
{

    public Camera currentCamera;
    public float increment;

    // Start is called before the first frame update
    void Start()
    {

    }

    private Vector3 bindToGrid(Vector3 vector, float increment){
        
        //float x = Mathf.Round(vector.x);
        float x = vector.x - (vector.x % increment);

       // float z = Mathf.Round(vector.z);
        float z = vector.z - (vector.z % increment);

        return new Vector3(x,0.5f,z);
    }


    // Update is called once per frame
    void Update()
    {

            Vector3 mousePos = Input.mousePosition;
            RaycastHit hitResult;

            Ray ray = currentCamera.ScreenPointToRay(mousePos);
            if (Physics.Raycast(ray.origin, ray.direction * 103, out hitResult)){
                transform.position = bindToGrid(hitResult.point,increment);
            };



    }
}
