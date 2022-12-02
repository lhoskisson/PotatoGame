using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PolygonCollider2D cd;
    Vector3 moveFarmer;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float accelerationTime = 2f;
    [SerializeField] private float maxSpeed = 7f;


    private bool isKnocked = false;
    void start(){
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        
        if(!isKnocked){
            rb.GetComponent<Rigidbody2D>().sharedMaterial.friction = 5f;
            gameObject.GetComponent<Collider2D>().sharedMaterial.friction = 5f;
            Movement();
            MouseRotation();
        } else {
            rb.GetComponent<Rigidbody2D>().sharedMaterial.friction = 0f;
            gameObject.GetComponent<Collider2D>().sharedMaterial.friction = 0f;
        }
    }
    
    public void Movement(){
        
        //Movement using WASD keys
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if ( (x == 1 || x == -1) || (y ==1 || y == -1)){
            acceleration = maxSpeed / accelerationTime;
            speed += accelerationTime * Time.smoothDeltaTime;
        }

        

        if ( speed > maxSpeed){
            speed = maxSpeed;
        }

        Vector3 velocity = new Vector3( speed * x * Time.smoothDeltaTime, speed * y * Time.smoothDeltaTime, 0f);
        
        moveFarmer = new Vector3(x, y, 0f);
        transform.position += moveFarmer * (speed * Time.smoothDeltaTime);
        rb.velocity = velocity;

    

    }
    public void MouseRotation(){
        //Rotating Camera
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        transform.up = mouse - transform.position;
    }
    void OnTriggerEnter2D(Collider2D collision){

        float force = 5f;
        Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        if ((collision.gameObject.tag == "Enemy") && (collision.gameObject.name != "Projectile")){
            if (enemy != null){
                isKnocked = true;
                Vector3 distance = transform.position - enemy.transform.position;
                distance = distance.normalized * force;
                rb.AddForce(distance, ForceMode2D.Impulse);
                StartCoroutine(timer(rb));
            }
        }
    }
    private IEnumerator timer(Rigidbody2D farmer){

        float pushTimer = .5f;
        yield return new WaitForSeconds(pushTimer);
        farmer.velocity = Vector3.zero;
        isKnocked = false;  
    }
}
