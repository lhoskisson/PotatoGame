using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliBehavior : MonoBehaviour
{
    // health bar variables
    public HealthBarBehavior healthBar;
    public float minHealth = 0f;
    public float maxHealth = 100f;

    public float broccoliHealth = 100;
    public static int broccoliCost = 3;

    public float broccoliSpeed = 1f;
    private float time = 0.0f;
    public float timeDelay = 1.0f;

    public GameObject targetPotato;
    public GameObject Farmer; // used to path to the farmer in the moveBroccoli method
    public GameObject potatoManager;
    public GameObject broccoliProjectile;

    public static bool pathingMode; // false = path to crops, true = path to farmer
    public bool isInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        // initializing healthBar on enemy
        minHealth = maxHealth;
        healthBar.SetHealth(minHealth, maxHealth);
        healthBar.offset = new Vector3(0f, 1f, 0f);

        broccoliProjectile = Resources.Load<GameObject>("Prefabs/BroccoliProjectile");
        potatoManager = GameObject.Find("Potato Manager");
        Farmer = GameObject.Find("Farmer");
    }

    // Update is called once per frame
    void Update()
    {
        // if statement moves broccoli towards a potato until one is in range, then attacks if one is.
        if (isInRange == true && (targetPotato != null || pathingMode == true)) // maybe we can remove the targetPotato check to have them throw at the farmer.
            throwBroccoli();
        else
            moveBroccoli();

    }

    private void moveBroccoli()
    {
        if (pathingMode == true)
        {
            // moving towards the farmer
            transform.position = Vector3.MoveTowards(transform.position, Farmer.transform.position, (broccoliSpeed * Time.smoothDeltaTime));
            // setting is in range to true so the broccoli will attack the farmer when in range
            if (Vector3.Distance(gameObject.transform.position, Farmer.transform.position) < 3.0f)
                isInRange = true;
            else
                isInRange = false;

            
        }
        else if(targetPotato == null)
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
            if(Vector3.Distance(gameObject.transform.position, targetPotato.transform.position) < 3.0f)
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
            if(projectile != null && pathingMode == false)
            {
                projectile.transform.position = gameObject.transform.position;
                projectile.transform.up = targetPotato.transform.position - projectile.transform.position;
            }
            else if (projectile != null && pathingMode == true)
            {
                projectile.transform.position = gameObject.transform.position;
                projectile.transform.up = Farmer.transform.position - projectile.transform.position;
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
            // broccoliHealth -= collision.gameObject.GetComponent<ProjectileScript>().damage;
            // adjusting healthbar
            minHealth -= collision.gameObject.GetComponent<ProjectileScript>().damage;
            healthBar.SetHealth(minHealth, maxHealth);
            if (minHealth <= 0)
                Destroy(gameObject);
        }
    }

}
