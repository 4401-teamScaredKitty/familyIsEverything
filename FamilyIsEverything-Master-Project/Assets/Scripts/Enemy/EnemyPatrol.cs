using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    //Dictates whether the agent waits on each node.
    [SerializeField]
    bool _patrolWaiting;

    //Total time we wait at each pint
    [SerializeField]
    float _totalWaitTime = 3f;

    //The chance of switching direction
    [SerializeField]
    float _switchProbability = 0.2f;

    //The list of all patrol nodes to visit
    [SerializeField]
    List<Waypoints> _patrolPoints;

    //Private variables for base behaviour
    NavMeshAgent _navMeshAgent;
    int _currentpatrolIndex;
    bool _travelling;
    bool _waiting;
    bool _patrolForward;
    float _waitTimer;

    static Animator enemyAnimator;

    public void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to" + gameObject.name);
        }
        else
        {
            if (_patrolPoints != null && _patrolPoints.Count >= 2)
            {
                _currentpatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficient patrol points for basic patrolling behaviour");
            }
        }
    }

    public void Update()
    {
        //Check if we are close to destination
        if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            _travelling = false;

            //If we are going to wait then wait
            if (_patrolWaiting)
            {
                enemyAnimator.SetBool("isWalking", false);
                enemyAnimator.SetBool("isScanning", true);
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
                enemyAnimator.SetBool("isScanning", false);
                enemyAnimator.SetBool("isWalking", true);
                ChangePatrolPoint();
                SetDestination();
            }
        }

        //Instead if we are waiting
        if (_waiting)
        {
            _waitTimer += Time.deltaTime;
            if (_waitTimer >= _totalWaitTime)
            {
                _waiting = false;

                ChangePatrolPoint();
                SetDestination();

            }

        }
    }

    private void SetDestination()
    {
        if (_patrolPoints != null)
        {
            Vector3 targetVector = _patrolPoints[_currentpatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;
        }
    }


    //if you want it to not switch comment out section below

    private void ChangePatrolPoint()
    {
        if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
        {
            _patrolForward = !_patrolForward;
        }
        if (_patrolForward)
        {
            _currentpatrolIndex = (_currentpatrolIndex + 1) % _patrolPoints.Count;
        }
        else
        {
            if (--_currentpatrolIndex < 0)
            {
                _currentpatrolIndex = _patrolPoints.Count - 1;
            }
        }
    }
}
