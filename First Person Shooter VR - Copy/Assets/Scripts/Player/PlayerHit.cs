using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class PlayerHit : MonoBehaviour {

    PlayerController player;

    public float health = 4;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        //check if enemy
        if (other.CompareTag("Enemy"))
        {
            if(health <= 0)
            {
                GameOver();
            }
            health--;
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("Game");
    }


}
