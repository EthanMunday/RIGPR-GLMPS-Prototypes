using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragClass : MonoBehaviour
{
    [Header("Positions")]
    public Vector3 mousePos;
    public Vector3 gridPos;


    


    public void onMouseClick(){

    }

    public void onMouseOff(){

    }

    private Vector3 bindToGrid(Vector3 vector, float increment){
        
        //float x = Mathf.Round(vector.x);
        float x = vector.x - (vector.x % increment);

       // float z = Mathf.Round(vector.z);
        float z = vector.z - (vector.z % increment);

        return new Vector3(x,0.5f,z);
    }

    public  void onUpdate(Vector3 grid, Vector3 exactPos){
        mousePos = exactPos;
        gridPos = grid;

    }
}
