using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector3 moveFarmer;
    public float speed = 5f;
    private bool isKnocked = false;
    public PotatoGunScript ammoCountLink;
    
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

        // checking for collision with broccoli projectiles so the player can be hit by them, the broccoli
        // pathing mode can be used in each of these if statements because all enemies should have the same pathing.
        if(collision.gameObject.name == "BroccoliProjectile(Clone)" && BroccoliBehavior.pathingMode == true)
        {
            int newAmmo = ammoCountLink.GetComponent<PotatoGunScript>().getAmmoCount() - 5;
            ammoCountLink.GetComponent<PotatoGunScript>().setAmmoCount(newAmmo);
        }
        // checking for collision with any enemy, as well as the pathing mode of the broccoli so the 
        // collision only occurs when crops are destroyed.
        if (collision.gameObject.tag == "Enemy" && BroccoliBehavior.pathingMode == true)
        {
            int newAmmo = ammoCountLink.GetComponent<PotatoGunScript>().getAmmoCount() - 5;
            ammoCountLink.GetComponent<PotatoGunScript>().setAmmoCount(newAmmo);
        }
        // checking for collision with the tomato explosion and taking 3x the crops
        if(collision.gameObject.name == "TomatoExplosion(Clone)" && BroccoliBehavior.pathingMode == true)
        {
            int newAmmo = ammoCountLink.GetComponent<PotatoGunScript>().getAmmoCount() - 15;
            ammoCountLink.GetComponent<PotatoGunScript>().setAmmoCount(newAmmo);
        }
    }
    private IEnumerator timer(Rigidbody2D farmer){

        float pushTimer = .5f;
        yield return new WaitForSeconds(pushTimer);
        farmer.velocity = Vector3.zero;
        isKnocked = false;  
    }
}
