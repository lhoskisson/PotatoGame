using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    //Projectile Lifespan
    public float lifeSpan = 5f;
    public float timeCounter = 0f;

    //Movement speed
    public float moveSpeed = 10f;
    
    //Damage
    public float damage = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        transform.position += transform.up * (moveSpeed * Time.smoothDeltaTime);

        //Projectile Lifespan
        timeCounter = timeCounter + Time.smoothDeltaTime;

        if(timeCounter > lifeSpan) {
            deleteEgg();
        } 
    }

    //For triggering collision
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy") {
            deleteEgg();
        }
    }

    public void deleteEgg() {
        Destroy(gameObject);
    }
}
