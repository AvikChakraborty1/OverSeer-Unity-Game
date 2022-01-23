using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2Ground : MonoBehaviour {

    Rigidbody2D playerRb;

	// Use this for initialization
	void Start () {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovements player = collision.gameObject.GetComponent<PlayerMovements>();
        

        if (player != null)
        {
            playerRb.gravityScale = 0;
            StartCoroutine(player.Die());
        }
    }
}
