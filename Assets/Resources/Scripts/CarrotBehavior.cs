using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotBehavior : MonoBehaviour
{
    // carrotCount allows us to keep track of how many carrots are loaded in the scene, 
        // may or may not be needed.
    public static int carrotCount;
    public int carrotHealth = 100;
    public float carrotSpeed = 1f;
    public GameObject testDummy;
    public GameObject carrotPrefab;
    public bool hasTouchedPotato;

    // private bool nearPotato = false;
    // Start is called before the first frame update
    void Start()
    {
        carrotPrefab = Resources.Load<GameObject>("Prefabs/Carrot");
        // finds an object in the scene named potato to path to. How will
        // this work with multiple potatoes? will we need to check for 
        // Potato, Potato2, etc or can we work through this another way depending
        // on how we implement the potato plots.
        testDummy = GameObject.Find("Potato");
    }

    // Update is called once per frame
    void Update()
    {
        moveToPotato(testDummy.transform.position);
        // IF STATEMENT ONLY HERE FOR TESTING, SHOULD BE REMOVED LATER
        if(Input.GetKeyDown("k"))
        {
            destroyCarrot();
        }
    }

    private void moveToPotato(Vector3 potatoPosition)
    {
        if(hasTouchedPotato != true)
        {
            transform.position = Vector3.MoveTowards(transform.position, potatoPosition,
                (carrotSpeed * Time.smoothDeltaTime));
        }
    }
 
    // this method is only here for testing.
    private void destroyCarrot()
    {
        Destroy(gameObject);
    }

    // method is called when the gameobject is destroyed, we can add sounds and animations
    // for their death here
    private void OnDestroy()
    {
        carrotCount--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            // carrots are a low level enemy and get killed with one potato.
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Potato")
        {
            // how are we going to know when to flip this bit again?
            // it cannot be accessed in other scripts because the variable 
            // cannot be static
            hasTouchedPotato = true;
        }
    }
}
