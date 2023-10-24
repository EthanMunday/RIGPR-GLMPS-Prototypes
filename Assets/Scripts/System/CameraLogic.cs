using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraLogic : MonoBehaviour
{

   private Vector3 START_ROTATION =  Vector3.zero;
    
   public Vector3 localMoveDirection =  Vector3.zero;
   private Vector3 globalMoveDirection =  Vector3.zero;
   public float cameraRotation = 0.0f;

   public float cameraRotateSpeed = 90.0f; // 90 degrees per second
   private float axis = 0.0f;

   [SerializeField] float wheelSensitivity = .5f;
   [SerializeField] float moveSpeed = 4f;

    float cameraDistance;

    public GameObject PlayerPoint; 
    public PlayerInput PlayerInput;
    
   

   private CinemachineVirtualCamera _virtualCamera;
   private CinemachineTransposer _transposer;


   public float MAX_ZOOM = 80.0f;
   public float MIN_ZOOM = 20.0f;
   
   private float currentZoom = 20.0f;


   private Vector3 OffsetDirection;


   private void Awake() {
      _virtualCamera = GetComponent<CinemachineVirtualCamera>();
      _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
   
      if (!_virtualCamera)
         Debug.LogError("NO VIRTUAL CAMERA COMPONENT");

      if(!_transposer)
         Debug.LogError("NO COMPOSER COMPONENT");


      OffsetDirection = _transposer.m_FollowOffset.normalized;
   }

    // Start is called before the first frame update
    void Start()
    {


    }


   public void OnMovement (InputAction.CallbackContext value)
   {
      Vector2 inputMovement = value.ReadValue<Vector2>();
      localMoveDirection = new Vector3(inputMovement.x,0,inputMovement.y);
   }


   public void OnRotate (InputAction.CallbackContext value)
   {
      cameraRotation = value.ReadValue<float>();

   }

   public void OnScroll (InputAction.CallbackContext value)
   {
      
      axis = value.ReadValue<float>();

      axis = Mathf.Clamp(axis,-1,1);
   }


    // Update is called once per frame
    void Update()
    {



        //Vector3 lookVector = transform.TransformDirection(localMoveDirection).normalized;
      Vector3 lookVector = Vector3.Scale(localMoveDirection, new Vector3(1.0f,0.0f,1.0f));





        // using this, we can now move the target based on the camera's look
        // direction

        
        PlayerPoint.transform.Translate(lookVector * Time.deltaTime * moveSpeed );


         // Calculate rotation using Rotation Speed

         float activeRotation = cameraRotation * (cameraRotateSpeed * Time.deltaTime);

         // Apply this to our target

         PlayerPoint.transform.Rotate(0.0f,activeRotation,0.0f,Space.World);


         currentZoom = Mathf.Clamp(currentZoom + (-axis * wheelSensitivity),20,60);


  
         
         _transposer.m_FollowOffset = currentZoom * OffsetDirection;



    }

}
