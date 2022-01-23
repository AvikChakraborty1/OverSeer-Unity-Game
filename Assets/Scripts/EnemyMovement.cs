using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour {

    public float startHealth = 3;
    public float currentHealth;
    public Image healthBar;

    public GameObject player;

    private float timeBtwAttack;

    private int damageToPlayer = 1;
    public float speed ;
    private Transform target;   
    public GameObject death;
    private Rigidbody2D rbEnemy;
    SpriteRenderer sr;

    private bool facingRight;


	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rbEnemy = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        currentHealth = startHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) < 8)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (target.position.x > transform.position.x && !facingRight) 
                Flip();
            if (target.position.x < transform.position.x && facingRight)
                Flip();
        }

        timeBtwAttack -= Time.deltaTime;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovements player = collision.gameObject.GetComponent<PlayerMovements>();
       
    if (player != null)
        { 
        if (timeBtwAttack <= 0)
            {
                timeBtwAttack = 1f;
                player.DamageTaken(damageToPlayer);

                if (transform.position.x > target.position.x)
                {
                    StartCoroutine(player.KnockBack(-10f));
                }
                else
                {
                    StartCoroutine(player.KnockBack(10f));
                }
                
            }
                
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;        
        StartCoroutine(ColorChange(.2f));
        healthBar.fillAmount = currentHealth / startHealth;

        if (currentHealth <= 0)
        {
            Death();
        }

    }

    IEnumerator ColorChange(float time)
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(time);
        sr.color = Color.white;
    }

    void Death()
    {
        Instantiate(death, transform.position, Quaternion.identity);
        Destroy(gameObject);
        

    }


    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }




    public IEnumerator KnockBack(float thrust)
    {
        float timer = 0;

        while (0.5 > timer)
        {
            timer += Time.deltaTime;
            rbEnemy.AddForce(transform.right * thrust);
            rbEnemy.AddForce(transform.up * Mathf.Abs((thrust / 2)));
        }

        yield return 0;
    }






    }
