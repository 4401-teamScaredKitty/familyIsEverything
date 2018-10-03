using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AnimatedPropWithParent : MonoBehaviour
{
  GameObject          target;
  SkinnedMeshRenderer targetRenderer;

  void Start()
  {
    
  }

  private void Update()
  {
    // The first time the gameObject is attached to a parent it must replace its bones and destroy this script
    if( transform.parent != null ){
      target          = transform.parent.gameObject;

      targetRenderer  = target.GetComponent<SkinnedMeshRenderer>();

      Dictionary<string, Transform> boneMap = new Dictionary<string, Transform>();
      foreach (Transform bone in targetRenderer.bones) boneMap[bone.gameObject.name] = bone;

      SkinnedMeshRenderer myRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
      Transform[] newBones = new Transform[myRenderer.bones.Length];

      // myRenderer.rootBone ?
      for (int i = 0; i < myRenderer.bones.Length; ++i)
      {
        GameObject bone = myRenderer.bones[i].gameObject;
        if (!boneMap.TryGetValue(bone.name, out newBones[i]))
        {
          Debug.Log("Unable to map bone \"" + bone.name + "\" to target skeleton.");
          break;
        }
      }
      myRenderer.bones = newBones;

      // Remove character rig from this object as it is going to use the parent rig
      for( int i = 0; i < transform.childCount; ++i)
      {
        GameObject child = transform.GetChild(i).gameObject;
        if( child.name.StartsWith("CharacterRig") ){
          // Remove this rig as we are going to use the one from our parent
          child.transform.parent = null;
          DestroyImmediate(child);
          break;
        }
      }

      // Local position should be zero
      transform.localPosition = Vector3.zero;

      //Debug.LogFormat("Bones assigned to {0} from {1} inside {2}", name, target.name, target.transform.parent.name);

      // This script won't be used any more
      DestroyImmediate(this);
    }
  }
}
