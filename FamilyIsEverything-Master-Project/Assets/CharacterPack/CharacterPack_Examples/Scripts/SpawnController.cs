using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
  public GameObject[] ObjectsToSpawn;
  public GameObject[] SpawnPositions;
  public float        SpawnFrequency;
  public int          SpawnMaximum;

  int                 ObjectsSpawned;
  float               NextSpawn;

	// Use this for initialization
	void Start ()
  {
		ObjectsSpawned  = 0;
    NextSpawn       = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
  {
		if( ObjectsSpawned < SpawnMaximum ) {
      if( Time.time >= NextSpawn ){
        NextSpawn = Time.time + SpawnFrequency;
        ObjectsSpawned++;

        int randomObjectIndex   = Random.Range( 0, ObjectsToSpawn.Length );
        int randomPositionIndex = Random.Range( 0, SpawnPositions.Length );
        if( ObjectsToSpawn[randomObjectIndex] != null ){
          GameObject.Instantiate( ObjectsToSpawn[randomObjectIndex], SpawnPositions[randomPositionIndex].transform.position, SpawnPositions[randomPositionIndex].transform.rotation );
        }
      }
    }
	}
}
