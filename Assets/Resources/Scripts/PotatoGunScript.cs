using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotatoGunScript : MonoBehaviour
{
    //UI script to keep count of ammo
	public Text ammo = null;

    //GameObjects used to harvest potato
    public GameObject potatoManager;
    private GameObject targetCrop;

    public GameObject farmer;
	
    //Projectile to shoot
    public GameObject proj;

    //Tip of the gun to fire from
    public GameObject gunTip;

    //Firerate
    public float cooldown = 0.2f;
    public float timeCounter = 0f;

    //Round count
    public int ammoCount;
    private bool inRange = false;
    

    // Start is called before the first frame update
    void Start()
    {
       ammoCount = 250;
	   if(potatoManager == null)
		   GameObject.FindWithTag("Potato Manager");
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        firingProjectiles();

        if(Input.GetKeyDown("space")){
            cropHarvest();
        }
        
        
    }
    public void movement(){

        //match farmer position
		transform.position = farmer.transform.position;

        //Rotating Camera
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        transform.up = mouse - transform.position;
    }

    public void firingProjectiles(){

        timeCounter = timeCounter + Time.smoothDeltaTime;

        ammo.text = "Ammo Count: " + ammoCount;
        //Firing Projectile
        if((Input.GetKey(KeyCode.Mouse0)) && timeCounter > cooldown) {
            
            if (ammoCount > 0){

                GameObject projectile = Instantiate(proj);
                ammoCount--;
                

                //Note! Points to the gun's right
                projectile.transform.position = gunTip.transform.position;
                projectile.transform.up = transform.up;
                
                timeCounter = 0;
            } else {
                ammo.text = "Out of Ammo!!";
            }
        }
    }

    public void cropHarvest(){

        if (targetCrop == null){
            targetCrop = potatoManager.GetComponent<PotatoManager>().GetClosestPotato(transform.position);
            inRange = false;
        }

        if (targetCrop != null && (Vector3.Distance(gameObject.transform.position, targetCrop.transform.position) < 2f)){
            inRange = true;
        }
        
        if(inRange == true){
            int harvested = potatoManager.GetComponent<PotatoManager>().HarvestPotato(targetCrop, 0f);
            ammoCount += harvested;
        }
    }
}
