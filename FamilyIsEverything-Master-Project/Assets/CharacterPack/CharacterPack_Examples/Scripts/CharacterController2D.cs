using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
  Animator      mAnimator;

  Camera        MainCamera;

  Vector3       CameraOffset;
  Vector3       CameraTarget;

  float         CameraPitch;

  Rigidbody     Rigidbody;

  public float  CameraSensitivity = 1.0f;


	// Use this for initialization
	void Start ()
  {
		mAnimator = GetComponentInChildren<Animator>();

    MainCamera = Camera.main;

    CameraTarget                  = transform.position + 1.0f * Vector3.up;
    CameraOffset                  = 2.0f * Vector3.up + 23.0f * transform.right;

    CameraPitch                   = 0.0f;

    Rigidbody                     = GetComponent<Rigidbody>();

    MainCamera.transform.position = transform.position + CameraOffset;
    MainCamera.transform.LookAt(  CameraTarget, Vector3.up );
	}
	
	// Update is called once per frame
	void Update ()
  {
    // Camera first
    UpdateCamera();

    float horizontalAxisValue = Input.GetAxis("Horizontal");
    float verticalAxisValue = Input.GetAxis("Vertical");

    // Crouch
    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
    {
      mAnimator.SetBool("Crouch", true);
      horizontalAxisValue *= 0.5f;
      verticalAxisValue   *= 0.5f;
    }
    else
    {
      mAnimator.SetBool("Crouch", false);
    }

    // Walk / Run
    if ( Mathf.Abs( horizontalAxisValue ) + Mathf.Abs( verticalAxisValue ) > 0.1f )
    {
      mAnimator.SetBool("Walk", true );
      if( Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
      {
        mAnimator.SetBool("Run", true );
        horizontalAxisValue *= 2.0f;
        verticalAxisValue *= 2.0f;
      }
      else
      {
        mAnimator.SetBool("Run", false );
      }

      Move( new Vector3( horizontalAxisValue, 0.0f, verticalAxisValue ) );
    }
    else
    {
      mAnimator.SetBool("Walk", false );
      mAnimator.SetBool("Run", false );
    }

    // Friction (horizontal only)
    float friction = 4.0f * Time.deltaTime;
    friction = Mathf.Clamp( friction, 0.0f, 1.0f );
    Vector3 rigidbodyVelocity = Rigidbody.velocity;
    rigidbodyVelocity.x *= 1.0f - friction;
    rigidbodyVelocity.z *= 1.0f - friction;
    Rigidbody.velocity = rigidbodyVelocity;
		
    

    // Jump
    if( Input.GetKeyDown(KeyCode.Space) )
    {
      mAnimator.SetTrigger("Jump" );
      Rigidbody.AddForce( 3.0f * transform.up, ForceMode.VelocityChange );
    }

    // Attack
    if( Input.GetKey(KeyCode.F) )
    {
      if( !mAnimator.GetBool( "Fight" ) ){
        mAnimator.SetTrigger("FightTrigger" );
        mAnimator.SetBool("Fight", true );
      }
    }
    else{
      mAnimator.SetBool("Fight", false );
    }
	}

  void UpdateCamera()
  {
     // Rotate camera
    float cameraRotationX         = Input.GetAxis( "Mouse X" );
    float cameraRotationY         = Input.GetAxis( "Mouse Y" );
    float zoomIncrease            = Input.GetAxis( "Mouse ScrollWheel" );



    CameraTarget                  = transform.position + 1.0f * Vector3.up;
    CameraOffset                  = Quaternion.AngleAxis(  CameraSensitivity * cameraRotationX, Vector3.up ) * CameraOffset;

    // Zoom
    CameraOffset *= 1.0f - zoomIncrease;
    // Clamp magnitude
    float magnitude = CameraOffset.magnitude;
    if( magnitude > 50.0f ){
      CameraOffset *= 50 / magnitude;
    }
    else if(magnitude < 3.0f){
      CameraOffset *= 3.0f / magnitude;
    }

    // Pitch
    CameraPitch += -CameraSensitivity * cameraRotationY;
    CameraPitch = Mathf.Clamp( CameraPitch, -30.0f, 15.0f ); // Limit pitch
    Vector3 CameraOffsetWithPitch = Quaternion.AngleAxis( CameraPitch, MainCamera.transform.right ) * CameraOffset;

    MainCamera.transform.position = transform.position + CameraOffsetWithPitch;
    MainCamera.transform.LookAt(  CameraTarget, Vector3.up );
  }

  void Move( Vector3 movement )
  {
    // Rotate movement vector according to camera
    float rotationY = MainCamera.transform.rotation.eulerAngles.y;

    movement = Quaternion.AngleAxis( rotationY, Vector3.up ) * movement;

    transform.rotation = Quaternion.Slerp( transform.rotation, Quaternion.LookRotation( movement, Vector3.up ), 10.0f * Time.deltaTime );

    Rigidbody.AddForce( 25.0f * movement, ForceMode.Acceleration );
  }
}
