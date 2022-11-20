using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector3 moveFarmer;
    public float speed = 5f;
    private bool isKnocked = false;
    
    void start(){
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){

        if(!isKnocked){
            Movement();
            MouseRotation();
        }
    }
    public void Movement(){
        
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
}
