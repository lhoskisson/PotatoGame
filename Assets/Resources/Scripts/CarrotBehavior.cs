using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CarrotBehavior : MonoBehaviour
{
    
    public int carrotHealth = 25;
    public int carrotDamage = 1;
    public static int carrotCost = 1;

    public float carrotSpeed = 2f;
    private float time = 0.0f;
    public float timeDelay = 1.0f; 
    
    public GameObject targetPotato;
    public GameObject potatoManager;
    public GameObject Farmer;

    public bool hasTouchedPotato;
    public static bool pathingMode; // false = pathing to crops, true = pathing to player

    // Start is called before the first frame update
    void Start()
    {
		potatoManager = GameObject.Find("Potato Manager");
        Farmer = GameObject.Find("Farmer");
    }

    // Update is called once per frame
    void Update()
    {
        moveToPotato();
    }
    // fixed update used for more stable damage updates
    private void FixedUpdate()
    {
        if (hasTouchedPotato == true)
            doDamage(carrotDamage);
    }

    private void moveToPotato()
    {
        if(pathingMode == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, Farmer.transform.position, (carrotSpeed * Time.smoothDeltaTime));
        }
		else if(targetPotato == null)
		{
            // setting target potato to the next closest potato if target potato hasn't been set or the first has been destroyed
			targetPotato = potatoManager.GetComponent<PotatoManager>().GetClosestPotato(transform.position);
			// setting hasTouchedPotato to false while carrots search for another potato
			hasTouchedPotato = false;
		}
        if(hasTouchedPotato != true && targetPotato != null)
        {
            // moving this carrot towards the targetPotato
            transform.position = Vector3.MoveTowards(transform.position, targetPotato.transform.position,
                (carrotSpeed * Time.smoothDeltaTime));
        }
    }

    // method is called when the gameobject is destroyed, we can add sounds and animations
    // for their death here
    private void OnDestroy()
    {
        EnemyManager.carrotCount--;
    }

    // this method runs while carrots are in contact with a potato crop (if statement in update)
    // using Time, this applies damage once per second. The delay can be modified through timeDelay.
    private void doDamage(int damage)
    {
        time += Time.deltaTime;
        if (time >= timeDelay && targetPotato != null)
        {
            time = 0f;
            targetPotato.GetComponent<PotatoCrop>().ApplyDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            // carrots are a low level enemy and get killed with one projectile.
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Crop")
        {
            // they hath touched the tater
            hasTouchedPotato = true;
        }
    }
}
