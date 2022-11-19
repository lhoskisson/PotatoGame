using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector3 moveFarmer;
    public float speed;
    // private float maxSpeed = 100f;
    // private float acceleration = 5f;
    private bool isKnocked = false;
    
    void Update(){

        if(!isKnocked){
            Movement();
            MouseRotation();
        }
    }
    public void Movement(){

        rb = GetComponent<Rigidbody2D>();

        //Movement using WASD keys
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        moveFarmer = new Vector3(x, y, 0f);
        transform.position += moveFarmer * (speed * Time.smoothDeltaTime);
        
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
    // public float farmerAcceleration(float x, float y){
    //     if ((y != 0 || x != 0) && (speed <= maxSpeed)){
    //         speed = speed + acceleration * Time.smoothDeltaTime;
    //     } else {
    //         speed = 0f;
    //     }
    //     return speed;
    // }
}
