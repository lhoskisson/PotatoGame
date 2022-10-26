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
    }

    // Update is called once per frame
    void Update()
    {
        moveToPotato(testDummy.transform.position);
    }

    private void moveToPotato(Vector3 potatoPosition)
    {
        if(hasTouchedPotato != true)
        {
            transform.position = Vector3.MoveTowards(transform.position, potatoPosition,
                (carrotSpeed * Time.smoothDeltaTime));
        }
    }
 
    // method is called when the gameobject is destroyed
    private void OnDestroy()
    {
        carrotCount--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "potatoProjectile")
        {
            carrotHealth -= 25;
        }
        if(collision.gameObject.name == "Potato")
        {
            // how are we going to know when to flip this bit again?
            // it cannot be accessed in other scripts because the variable 
            // cannot be static
            hasTouchedPotato = true;
        }
    }
}
