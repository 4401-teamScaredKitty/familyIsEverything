﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMove : MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    NavMeshAgent _navMeshAgent;

    static Animator enemyAnimator;

    // Use this for initialization
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to" + gameObject.name);
        }
        else
        {
            SetDestination();
        }
    }

    private void SetDestination()
    {
        if (_destination != null)
        {
            enemyAnimator.SetBool("isWalking", true);
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
        else
        {
            enemyAnimator.SetBool("isWalking", false);
            enemyAnimator.SetBool("Idle", true);
        }

    }
}