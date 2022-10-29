using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoGunScript : MonoBehaviour
{
	public GameObject farmer;
	
    //Projectile to shoot
    public GameObject proj;
    //Tip of the gun to fire from
    public GameObject gunTip;

    //Firerate
    public float cooldown = 0.2f;
    public float timeCounter = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//match farmer position
		transform.position = farmer.transform.position;
		
        timeCounter = timeCounter + Time.smoothDeltaTime;

        //Rotating Camera
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        transform.up = mouse - transform.position;

        //Firing Projectile
        if((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && timeCounter > cooldown) {
            GameObject projectile = Instantiate(proj);

            //Note! Points to the gun's right
            projectile.transform.position = gunTip.transform.position;
            projectile.transform.up = transform.up;

            timeCounter = 0;
        }
    }
}
