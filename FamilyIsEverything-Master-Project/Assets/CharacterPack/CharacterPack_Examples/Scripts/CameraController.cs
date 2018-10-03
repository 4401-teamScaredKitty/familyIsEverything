using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

  public GameObject ObjectToFollow;

  Vector3 Offset;

	// Use this for initialization
	void Start ()
  {
    Offset = transform.position - ObjectToFollow.transform.position;
  }
	
	// Update is called once per frame
	void Update ()
  {
    transform.position = ObjectToFollow.transform.position + Offset;
  }
}
