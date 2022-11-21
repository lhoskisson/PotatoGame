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

    //GunUI
    public GunUI myUI;

    //Gun Defaults
    public float cooldown = 0.2f;
    public float timeCounter = 0f;
    public float spray = 0f;
    public int timesFired = 1;
    public int mode = 0;

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
        changeMode();
        firingProjectiles();

        if(Input.GetKeyDown("space")){
		    cropHarvest();
        }
        if(Input.GetKeyDown("c"))
        {
            cropPlant();
        }
    }

    public int getAmmoCount()
    {
        return ammoCount;
    }

    //Handles Camera rotation
    public void movement(){
        //Rotating Camera
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        transform.up = mouse - transform.position;
    }

    //Changes the mode for the gun
    //Also changes gunside firing info: times a projectile is fired, 
    //Firerate, and spray.
    public void changeMode() {
        int oldMode = mode;

        //Key 1: Default PotatoGun
        if(Input.GetKey(KeyCode.Alpha1) && mode != 0) {
            mode = 0;
        } else if(Input.GetKey(KeyCode.Alpha2) && mode != 1) {
            //Key 2:Fries MachineGun

            mode = 1;
        } else if(Input.GetKey(KeyCode.Alpha3) && mode != 2) {
            //Key 3: Slow, but powerful

            mode = 2;
        } else if(Input.GetKey(KeyCode.Alpha4) && mode != 3) {
            //Key 4: Shotgun

            mode = 3;
        } else if(Input.GetAxis("Mouse ScrollWheel") > 0f) {
            //Scroll Up
            mode++;

            if(mode > 3) {
                mode = 0;
            }
        } else if(Input.GetAxis("Mouse ScrollWheel") < 0f) {
            //Scroll Down
            mode--;

            if(mode < 0) {
                mode = 3;
            }
        }

        float modeChanged = (oldMode - mode);

        //If the gun mode changed
        if(modeChanged == 0) {
            myUI.setVal(mode);
            setMode(mode);
        }
    }

    public void setMode(int input) {
        if(input == 0) {
            //Default

            cooldown = 0.2f;
            spray = 0f;
            timesFired = 1;

            gunTip.GetComponent<FiringPointScript>().spriteChange(0);
        } else if(input == 1) {
            //MachineGun

            cooldown = 0.05f;
            spray = 20f;
            timesFired = 1;

            gunTip.GetComponent<FiringPointScript>().spriteChange(1);
        } else if(input == 2) {
            //Slow, Chunky

            cooldown = 1f;
            spray = 0f;
            timesFired = 1;

            gunTip.GetComponent<FiringPointScript>().spriteChange(2);

        } else if(input == 3) {
            //Shotgun

            cooldown = 0.5f;
            spray = 45f;
            timesFired = 20;

            gunTip.GetComponent<FiringPointScript>().spriteChange(3);
        }

        if(input != mode) {
            mode = input;
        } 
    }

    public void firingProjectiles(){

        timeCounter = timeCounter + Time.smoothDeltaTime;

        ammo.text = "Ammo Count: " + ammoCount;

        //Firing Projectile
        if((Input.GetKey(KeyCode.Mouse0)) && timeCounter > cooldown) {

            changeText();
            
            if (ammoCount > 0){
                ammoCount--;

                for(int i = 0; i < timesFired; i++) {
                    GameObject projectile = Instantiate(proj);

                    projectile.GetComponent<ProjectileScript>().mode = mode;

                    projectile.transform.position = gunTip.transform.position;
                    projectile.transform.up = transform.up;

                    //Bullet spray, default 0
                    projectile.transform.Rotate(Random.Range(-spray, spray), Random.Range(-spray, spray), 0);
                }
                
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
        targetCrop = null;
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
        if (closestPotato != null)
		    if(gridPosition == closestPotato.transform.position)
			    return false;
		
		potatoManager.GetComponent<PotatoManager>().SpawnPotato(gridPosition);
		ammoCount -= plantCost;
		return true;
	}
    public void changeText(){
        if (ammoCount >= 101){
            ammo.color = Color.black;
            ammo.fontSize = 22;
        }else if (ammoCount >= 51){
            ammo.color = Color.yellow;
            ammo.fontSize = 24;
        } else if (ammoCount >= 21){
            ammo.color = new Color (.9622f, .4127f, 0f);
            ammo.fontSize = 26;
            ammo.fontStyle = FontStyle.Bold;
        } else{
            ammo.color = Color.red;
            ammo.fontSize = 28;
            ammo.fontStyle = FontStyle.Bold;
        }
    }
}
