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
	
	//GameObject containing grid component used for planting crops.
	public GameObject grid;
	
	//ammo cost for planting.
	public int plantCost = 5;

    public GameObject farmer;
	
    //Projectile to shoot
    public GameObject proj;

    //Tip of the gun to fire from
    public GameObject gunTip;

    //Firerate
    public float cooldown = 0.2f;
    public float timeCounter = 0f;

    //Round count
    private int ammoCount;
    private bool inRange = false;
    

    // Start is called before the first frame update
    void Start()
    {
       ammoCount = 250;
	   if(potatoManager == null)
		   potatoManager = GameObject.FindWithTag("Potato Manager");
	   if(grid == null)
		   grid = GameObject.Find("Grid");
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        firingProjectiles();

        if(Input.GetKeyDown("space")){
            if(!cropPlant())
				cropHarvest();
        }
    }
    public void movement(){

        //match farmer position
		//transform.position = farmer.transform.position;

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

    public bool cropHarvest(){

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
		return inRange;
    }
	
	public bool cropPlant(){
		
		//check that the player has enough ammo to plant.
		if(ammoCount < plantCost)
			return false;
		
		//access grid and get current grid position.
		Grid g = grid.GetComponent<Grid>();
		Vector3 gridPosition = g.GetCellCenterWorld(g.WorldToCell(transform.position));
		
		//check if there is already a potato at the grid position.
		GameObject closestPotato = potatoManager.GetComponent<PotatoManager>().GetClosestPotato(transform.position);
		if(gridPosition == closestPotato.transform.position)
			return false;
		
		potatoManager.GetComponent<PotatoManager>().SpawnPotato(gridPosition);
		ammoCount -= plantCost;
		return true;
	}
}
