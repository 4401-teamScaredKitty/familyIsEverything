using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControllerClick : MonoBehaviour
{
  Animator      mAnimator;

  NavMeshAgent  mNavMeshAgent;

  Camera        mMainCamera;

	// Use this for initialization
	void Start ()
  {
		mAnimator     = GetComponentInChildren<Animator>();
    mNavMeshAgent = GetComponent<NavMeshAgent>();

    mMainCamera   = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
  {
    if( Input.GetMouseButton(0) ){
      RaycastHit hit;
      Ray ray = mMainCamera.ScreenPointToRay(Input.mousePosition);
        
      if (Physics.Raycast(ray, out hit)) {
        mNavMeshAgent.SetDestination( hit.point );
      }
    }

    if( mNavMeshAgent.velocity.magnitude > 0.2f)
    {
      mAnimator.SetBool("Walk", true );
    }
    else
    {
      mAnimator.SetBool("Walk", false );
    }
	}
}
