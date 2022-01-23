using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public GameObject lvl2Spawn;
    private GameMaster gm;

	// Use this for initialization
	void Start () {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovements player = collision.gameObject.GetComponent<PlayerMovements>();

        if (player != null)
        {
            player.transform.position = lvl2Spawn.transform.position;
            gm.lastCheckpointPos = lvl2Spawn.transform.position;
          //  player.jumpForce = 8f;
        }
    }
}
