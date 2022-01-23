using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Portal : MonoBehaviour {

    private GameMaster gm;
    public GameObject lvl3Spawn;

    // Use this for initialization
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovements player = collision.gameObject.GetComponent<PlayerMovements>();

        if (player != null)
        {
            player.transform.position = lvl3Spawn.transform.position;
            gm.lastCheckpointPos = lvl3Spawn.transform.position;
            
        }
    }
}
