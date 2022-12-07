using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector3 moveFarmer;
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float accelerationTime = 3f;
    [SerializeField] private float maxSpeed = 7f;

    public float knockImmunity = .75f;
    public float knockTime = 0f;

    private bool isKnocked = false;
    public PotatoGunScript ammoCountLink;
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){
        knockTime += Time.smoothDeltaTime;
        
        //If statement to check if the farmer is being knocked back by the enemy.
        //If it is false it allows the farmer to move and sets the friction to 6.
        /*If it is true then the farmer can not move and the friction is set to 0
          to prevent friction from affecting the push back feature*/
        if(!isKnocked){
            rb.GetComponent<Rigidbody2D>().sharedMaterial.friction = 6f;
            gameObject.GetComponent<Collider2D>().sharedMaterial.friction = 6f;
            Movement();
            MouseRotation();
        }else {
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
        if (((collision.gameObject.tag == "Enemy") || (collision.gameObject.name != "Projectile")) && knockTime > knockImmunity){
            if (enemy != null){
                knockTime = 0f;
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
