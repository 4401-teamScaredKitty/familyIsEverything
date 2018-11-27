using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class damagePlayer : MonoBehaviour
{

    public int playerHealth = 30;
    int damage = 10;



    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            //most likely need to change way player is damaged for animator
            SceneManager.LoadScene("Game_Over");
        }
    }

   

}
