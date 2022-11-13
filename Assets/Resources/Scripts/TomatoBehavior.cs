using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoBehavior : MonoBehaviour
{
    public float tomatoHealth = 200f;
    public int tomatoDamage = 2;

    public float tomatoSpeed = .5f;
    private float time = 0.0f;
    public float timeDelay = 1.0f;

    public GameObject targetPotato;
    public GameObject potatoManager;

    public bool hasTouchedPotato;

    // Start is called before the first frame update
    void Start()
    {
        potatoManager = GameObject.Find("Potato Manager");
    }

    // Update is called once per frame
    void Update()
    {
        moveToPotato();
    }

    // FixedUpdate will update damage done to the potato in a more stable way
    private void FixedUpdate()
    {
        if (hasTouchedPotato == true)
            doDamage(tomatoDamage);
    }

    private void moveToPotato()
    {
        if (targetPotato == null)
        {
            // setting target potato to the next closest potato if target potato hasn't been set or the first has been destroyed
            targetPotato = potatoManager.GetComponent<PotatoManager>().GetClosestPotato(transform.position);
            // setting hasTouchedPotato to false while carrots search for another potato
            hasTouchedPotato = false;
        }
        if (hasTouchedPotato != true && targetPotato != null)
        {
            // moving this carrot towards the targetPotato
            transform.position = Vector3.MoveTowards(transform.position, targetPotato.transform.position,
                (tomatoSpeed * Time.smoothDeltaTime));
        }
    }

    private void doDamage(int damage)
    {
        time += Time.deltaTime;
        if (time >= timeDelay && targetPotato != null)
        {
            time = 0f;
            targetPotato.GetComponent<PotatoCrop>().ApplyDamage(damage);
        }
    }

    // this method will cause the tomato to explode when called. It will be called upon death
    private void Explode()
    {
        Debug.Log("BOOM");
        GameObject explosion = Resources.Load<GameObject>("Prefabs/TomatoExplosion");
        if (explosion != null)
        {
            // setting the explosion to the tomato's position
            explosion.transform.position = gameObject.transform.position;
            Instantiate(explosion);
        }
    }

    private void OnDestroy()
    {
        EnemyManager.tomatoCount--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            // tomatos are a higher level enemy and take a good bit of damage to destroy
            tomatoHealth -= collision.gameObject.GetComponent<ProjectileScript>().damage;
            if(tomatoHealth <= 0)
            {
                // originally Explode() call was in OnDestroy, this caused explosions to be spawned after the scene
                // was ended and the remaining tomatoes were destroyed. 
                Explode();
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Crop")
        {
            // they hath touched the tater
            hasTouchedPotato = true;
        }
    }
}
