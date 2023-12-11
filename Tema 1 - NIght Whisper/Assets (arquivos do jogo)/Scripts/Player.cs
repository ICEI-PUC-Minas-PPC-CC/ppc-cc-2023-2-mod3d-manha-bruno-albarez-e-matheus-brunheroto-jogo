using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public int health;

    public GameObject fogo;
    public Transform firePoint;

    private bool doubleJump;
    private bool isJumping;
    private bool isFire;
    private float movement;

    private Rigidbody2D rig;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        GameController.instance.UpdateLives(health);
    }

    // Update is called once per frame
    void Update()
    { 
        Jump();
    }

    void FixedUpdate(){
        Move();
        Fire();
    }

    void Move(){
        movement = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if(movement > 0){
            anim.SetInteger("transition", 1);
            transform.eulerAngles = new Vector3(0,0,0);
        }

        if(movement < 0){
            anim.SetInteger("transition", 1);
            transform.eulerAngles = new Vector3(0,180,0);
        }

        if(movement == 0){
            anim.SetInteger("transition", 2);
        }

    }

    void Jump(){
        if(Input.GetButtonDown("Jump")){
            if(!isJumping){
                rig.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
                isJumping = true;
                doubleJump = true;
            } else {
                if(doubleJump){
                    rig.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.layer == 6){
            isJumping = false;
        }

        if(coll.gameObject.layer == 7){
            GameController.instance.GameOver();
            
        }
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.E)) 
        {
            if (!isFire)
            {
                StartCoroutine(FireCoroutine());
            }
        }
        else
        {
            isFire = false; 
        }
    }

    IEnumerator FireCoroutine(){
        isFire = true;
        GameObject fireInstance = Instantiate(fogo, firePoint.position, firePoint.rotation);

        if(transform.rotation.y == 0){
            fireInstance.GetComponent<Fire>().isRight = true;
        } 
        
        if(transform.rotation.y == 180){
            fireInstance.GetComponent<Fire>().isRight = false;
            Debug.Log("teste");
        }

        yield return new WaitForSeconds(1f);
        isFire = false;
    }

    public void Damage(int dmg){
        health -= dmg;
        GameController.instance.UpdateLives(health);
         
         if(transform.rotation.y == 0){
            transform.position += new Vector3(-0.5f,0,0);
         }

         if(transform.rotation.y == 180){
            transform.position += new Vector3(0.5f,0,0);
         }

         if (health <= 0){
            GameController.instance.GameOver();
         }

    }
}
