using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoExplosion : MonoBehaviour
{
    // too high? 50 damage one shots any potato crops in the explosion.
    public static int exlosionDamage = 25;

    public float life = .5f;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.smoothDeltaTime;

        if (time > life)
        {
            Destroy(gameObject);
        }
    }
}
