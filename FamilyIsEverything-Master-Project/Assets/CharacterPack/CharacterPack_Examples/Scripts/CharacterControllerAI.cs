using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterControllerAI : MonoBehaviour
{
  bool Dead;
  Animator      mAnimator;

  NavMeshAgent  mNavMeshAgent;

  float         mTimeBetweenDestinationUpdates = 2.0f;
  float         mNextDestinationAssignment;

	// Use this for initialization
	void Start ()
  {
    Dead = false;
    mAnimator     = GetComponentInChildren<Animator>();
    mNavMeshAgent = GetComponent<NavMeshAgent>();

    GetComponent<NavMeshAgent>().enabled = true;

    mNextDestinationAssignment = 0.0f;

    mNavMeshAgent.Warp( transform.position );
	}
	
	// Update is called once per frame
	void Update ()
  {
    if (!Dead)
    {
      if (Time.time > mNextDestinationAssignment && mNavMeshAgent.remainingDistance < 0.5f)
      {
        mNextDestinationAssignment = Time.time + mTimeBetweenDestinationUpdates;

        Vector3 destination = new Vector3(Random.Range(-90.0f, 90.0f), 0.0f, Random.Range(-100.0f, 50.0f));
        mNavMeshAgent.SetDestination(destination);
      }

      if (mNavMeshAgent.velocity.magnitude > 0.2f)
      {
        mAnimator.SetBool("Walk", true);
      }
      else
      {
        mAnimator.SetBool("Walk", false);
      }
    }
	}

  void OnTriggerStay(Collider other)
  {
    Animator otherAnimator = other.gameObject.GetComponent<Animator>();
    if( !Dead && otherAnimator != null)
    {
      if( otherAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Fight") && otherAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f ) {
        Dead = true;
        if (Random.Range(0.0f, 1.0f) > 0.4f)
        {
          GetComponent<Animator>().SetTrigger("Dead");
        }
        else
        {
          GetComponent<Animator>().SetTrigger("Dead2");
        }
        GetComponent<NavMeshAgent>().isStopped = true;
        GetComponent<NavMeshAgent>().enabled = false;
      }
    }
  }
}
