using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public Sprite[] spriteArray = new Sprite[4];

    //Projectile Lifespan
    public float lifeSpan = 5f;
    public float timeCounter = 0f;

    //Movement speed
    public float moveSpeed = 10f;
    
    //Damage
    public int damage = 1;

    //Pierce
    public int pierceNum = 2;

    //Mode
    public int mode = 0;

    // Start is called before the first frame update
    void Start()
    {
        Sprite curr = spriteArray[0];

        if(mode == 0) {
            //Default PotatoGun
            damage = 20;
            gameObject.GetComponent<Animator>().enabled = true;    
        } else if(mode == 1) {
            //MachineGun

            damage = 15;
            lifeSpan = 7f;
            moveSpeed = 20f;
            pierceNum = 1;
            curr = spriteArray[1];
        } else if(mode == 2) {
            //Heater

            damage = 50;
            lifeSpan = 10f;
            moveSpeed = 15f;
            pierceNum = 10;
            curr = spriteArray[2];
        } else if(mode == 3) {
            //Fry Shotgun
        
            damage = 3;
            lifeSpan = 0.125f;
            moveSpeed = 10f;
            pierceNum = 1;
            curr = spriteArray[3];
        }

        SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();
        rend.sprite = curr;
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
            pierceNum--;

            if(pierceNum <= 0) {
                deleteEgg();
            }
        }
    }

    public void deleteEgg() {
        Destroy(gameObject);
    }
}
