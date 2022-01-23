using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHit : MonoBehaviour {

    public int damage = 1;

   
   
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    void OnTriggerExit2D(Collider2D collision)
    {
         EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
         Transform enemylocation = collision.GetComponent<Transform>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            if (transform.position.x > enemylocation.position.x)
            {
                StartCoroutine(enemy.KnockBack(-10f));
            }
            else
            {
                StartCoroutine(enemy.KnockBack(10f));
            }

        }

        }

    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  


