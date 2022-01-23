using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovements : MonoBehaviour {

    public float startHealth;
    public float currentHealth;
    public Image healthBar;
    private bool isGrounded = true;
    private float timeBtwAttack;
    bool dead;
    public float jumpForce;

    private GameMaster gm;
  
   [SerializeField] GameObject attackCollider;
    private PolygonCollider2D coll;

    public Transform lvl1Spawn;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = attackCollider.GetComponent<PolygonCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckpointPos;

        coll.enabled = false;
        currentHealth = startHealth;
        rb.gravityScale = 1;
        jumpForce = 7f;

    }

    // Update is called once per frame
    void Update()
    {
        

        //movement with animation control
        if (Input.GetKey("d") && !dead)
        {
            rb.velocity = new Vector2(3, rb.velocity.y);
            transform.localScale = new Vector3(-3.0f, 3.0f, 1.0f);
            animator.SetInteger("AnimState", 2);

        }
        else if (Input.GetKey("a") && !dead)
        {
            rb.velocity = new Vector2(-3, rb.velocity.y);
            transform.localScale = new Vector3(3.0f, 3.0f, 1.0f);
            animator.SetInteger("AnimState", 2);
        }
        else
        {
            animator.SetInteger("AnimState", 1);
        }


        //Jumping with animation control
        isGrounded = IsGrounded();
        animator.SetBool("Grounded", isGrounded);
       
        if (Input.GetKeyDown("w") && isGrounded && !dead)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");

        }

        //attack with animation control 
        timeBtwAttack -= Time.deltaTime;
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !dead)
            {
                animator.SetTrigger("Attack");                
                StartCoroutine(AttackAfterTime(.4f));
                timeBtwAttack = .7f;
            }
        }
                          

        if (currentHealth <= 0)
        {
           StartCoroutine(Die());
        }

    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////


    public void DamageTaken(int enemyDamage)
    {
        currentHealth -= enemyDamage;
        animator.SetTrigger("Hurt");
        healthBar.fillAmount = currentHealth / startHealth;
        StartCoroutine(ColorChange(.2f));       
    }

    public IEnumerator Die()
    {
        animator.SetBool("Dead", true);
        dead = true;        
        yield return new WaitForSeconds(1.5f);
        LoseLife();               
        yield return 0;       
    }

    void LoseLife()
    {
        gm.livesLeft -= 1;
        if (gm.livesLeft == 0)
        {
            gm.lastCheckpointPos = lvl1Spawn.position;
            SceneManager.LoadScene("DeathScreen");                        
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator ColorChange( float time)
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(time);
        sr.color = Color.white;
    }

    IEnumerator AttackAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        coll.enabled = true;
        

        yield return new WaitForSeconds(.15f);
        coll.enabled = false;
        
    }
    
    public IEnumerator KnockBack(float thrust)
    {
        float timer = 0;        

        while (0.5 > timer)
        {
            timer += Time.deltaTime;
            rb.AddForce(transform.right * thrust);
            rb.AddForce(transform.up * Mathf.Abs(thrust));
        }

        yield return 0;
    }



    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector3.up, 0.03f);
    }



}