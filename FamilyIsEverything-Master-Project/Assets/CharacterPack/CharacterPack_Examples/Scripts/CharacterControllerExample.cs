using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatorControllerExample : MonoBehaviour
{
  Animator anim;

  // Use this for initialization
  void Start()
  {
    anim = gameObject.GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown("space"))
    {
      anim.SetTrigger("next");
    }
  }
}
