using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 10f;
    
    
    
    void Update(){
        Movement();
		MouseRotation();


    }
    public void Movement(){

        //Movement using WASD keys
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 moveFarmer = new Vector3(x, y, 0f);
        transform.position += moveFarmer * (speed * Time.smoothDeltaTime);   
    }
    public void MouseRotation(){
        //Rotating Camera
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        transform.up = mouse - transform.position;
    }
    void OnTriggerEnter2D(Collider2D collision){

        float force = 2f;

        if (collision.gameObject.CompareTag("Enemy")){

            rb = GetComponent<Rigidbody2D>();
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
            if (enemy != null){
                enemy.isKinematic = false;
                Vector3 distance = transform.position - enemy.transform.position;
                distance = distance.normalized * force;
                rb.AddForce(distance, ForceMode2D.Impulse);
                StartCoroutine(timer(rb));
                enemy.isKinematic = true;
                
            }
        }
    }
    private IEnumerator timer(Rigidbody2D farmer){

        float pushTimer = .5f;
        yield return new WaitForSeconds(pushTimer);
        farmer.velocity = Vector3.zero;
    }
   
}
