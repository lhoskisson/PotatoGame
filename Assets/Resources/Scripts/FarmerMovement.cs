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
    [SerializeField] private float accelerationTime = 3f;
    [SerializeField] private float maxSpeed = 7f;

    private bool isKnocked = false;

    void start(){
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        
        //If statement to check if the farmer is being knocked back by the enemy.
        //If it is false it allows the farmer to move and sets the friction to 6.
        /*If it is true then the farmer can not move and the friction is set to 0
          to prevent friction from affecting the push back feature*/
        if(!isKnocked){
            rb.GetComponent<Rigidbody2D>().sharedMaterial.friction = 6f;
            gameObject.GetComponent<Collider2D>().sharedMaterial.friction = 6f;
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

        /*Checks to see if the farmer is moving to calculate the farmers
          acceleration and set the farmers speed*/
        if ( (x == 1 || x == -1) || (y == 1 || y == -1)){
            acceleration = maxSpeed / accelerationTime;
            speed += accelerationTime * Time.smoothDeltaTime;
        }

        //If farmer is not moving, this sets the speed back to 1.
        if ( (x == 0) && (y == 0)){
            
            speed = 1f;
        }
        
        //Checks the farmers speed and sets it to the max speed.
        if ( speed > maxSpeed){
            speed = maxSpeed;
        }

        //Vector to calculate the farmers velocity
        Vector3 velocity = new Vector3( speed * x * Time.smoothDeltaTime, speed * y * Time.smoothDeltaTime, 0f);
        
        //Sets the farmers vector to use the inputs from each axis.
        moveFarmer = new Vector3(x, y, 0f);

        //Allows the farmer to move using WASD keys
        transform.position += moveFarmer * (speed * Time.smoothDeltaTime);

        //Sets the farmers rigidbody velocity.
        rb.velocity = velocity;

    

    }
    public void MouseRotation(){
        //Rotating Camera
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        transform.up = mouse - transform.position;
    }
    void OnTriggerEnter2D(Collider2D collision){

        float force = speed * 2;
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
