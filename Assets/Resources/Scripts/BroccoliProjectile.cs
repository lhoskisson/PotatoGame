using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroccoliProjectile : MonoBehaviour
{
    public float lifeSpan = 3.0f;
    public static int broccoliDamage = 10;
    public float projectileSpeed = 5f;
    public float timeCounter = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Moveing the projectile towards its transform.up, which should be set to target potato in 
        // the BroccoliBehavior
        transform.position += transform.up * (projectileSpeed * Time.smoothDeltaTime);
        // gameObject.transform.rotation = transform.rotation.Set(newX, newY, newZ, newW);

        timeCounter = timeCounter + Time.smoothDeltaTime;

        if (timeCounter > lifeSpan)
        {
            Destroy(gameObject);
        }
    }
}
