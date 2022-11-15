using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliBehavior : MonoBehaviour
{
    public int broccoliHealth = 100;
    public static int broccoliCost = 3;

    public float broccoliSpeed = 1f;
    private float time = 0.0f;
    public float timeDelay = 1.0f;

    public GameObject targetPotato;
    public GameObject potatoManager;
    public GameObject broccoliProjectile;
    
    public bool isInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        broccoliProjectile = Resources.Load<GameObject>("Prefabs/BroccoliProjectile");
        potatoManager = GameObject.Find("Potato Manager");
    }

    // Update is called once per frame
    void Update()
    {
        // if statement moves broccoli towards a potato until one is in range, then attacks if one is.
        if (isInRange == true && targetPotato != null)
            throwBroccoli();
        else
            moveBroccoli();
    }

    private void moveBroccoli()
    {
        if (targetPotato == null)
        {
            // setting target potato to the next closest potato if target potato hasn't been set or the first has been destroyed
            targetPotato = potatoManager.GetComponent<PotatoManager>().GetClosestPotato(transform.position);
            // setting isInRange to false while broccoli enemies search for another potato
            isInRange = false;
        }
        else
        {
            // moving this broccoli towards the targetPotato
            transform.position = Vector3.MoveTowards(transform.position, targetPotato.transform.position,
                (broccoliSpeed * Time.smoothDeltaTime));
            // checking if the broccoli is in range of a potato, this is where we can adjust the distance they attack at
            if(Vector3.Distance(gameObject.transform.position, targetPotato.transform.position) < 2.0f)
            {
                isInRange = true;
            }
        }
    }

    // method that causes broccoli enemies to throw broccoli
    private void throwBroccoli()
    {
        time += Time.deltaTime;
        if (time >= timeDelay)
        {   
            GameObject projectile = Instantiate(broccoliProjectile);
            if(projectile != null)
            {
                projectile.transform.position = gameObject.transform.position;
                projectile.transform.up = targetPotato.transform.position - projectile.transform.position;
            }
            time = 0f;
        }
        // this check needs to be here because in update it will not be checked since moveBroccoli
        // wont be called again until isInRange is flipped.
        if(targetPotato == null)
        {
            isInRange = false;
            moveBroccoli();
        }
    }

    // method is called when the gameobject is destroyed, we can add sounds and animations
    // for their death here
    private void OnDestroy()
    {
        EnemyManager.broccoliCount--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            // Broccoli are a stronger enemy and should take 3-4 default projectiles to kill.
            broccoliHealth -= 25;
            if(broccoliHealth <= 0)
                Destroy(gameObject);
        }
    }

}
