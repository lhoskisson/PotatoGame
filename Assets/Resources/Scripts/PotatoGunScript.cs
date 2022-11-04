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
    private bool inRange = true;
    

    // Start is called before the first frame update
    void Start()
    {
       ammoCount = 100;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        firingProjectiles();
        cropHarvest();
        
        
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

        
        //Firing Projectile
        if((Input.GetKey(KeyCode.Mouse0)) && timeCounter > cooldown) {
            
            if (ammoCount > 0){

                GameObject projectile = Instantiate(proj);
                ammoCount--;
                ammo.text = "Ammo Count: " + ammoCount;

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
            Debug.Log("You there?" + targetCrop);
            inRange = false;
        } else{
            if (Vector3.Distance(gameObject.transform.position, targetCrop.transform.position) < 1.0f){
                Debug.Log("You Still there?" + targetCrop);
                inRange = true;
            }
        }

        if(targetCrop != null && inRange == true){
            Debug.Log("But are you Really?" + targetCrop);
            if(Input.GetKeyDown("space")){
                Debug.Log("Is it working?");
                int harvested = potatoManager.GetComponent<PotatoManager>().HarvestPotato(targetCrop, 0f);
                ammoCount += harvested;
                Debug.Log(ammoCount);
            }
            
        }
    }
}
