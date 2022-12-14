using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotatoGunScript : MonoBehaviour
{
    //UI script to keep count of ammo
	public Text ammo = null;

    public GameObject potato;
    public GameObject potatoDead;

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
    public GunManager myModes;

    //Round count
    private int ammoCount;
    private bool inRange = false;

    //time variables for planting/harvesting
    public float harvestingCooldown;
    public float plantingCooldown;
    private float lastPlantTime = Timer.levelTime;
    private float lastHarvestTime = Timer.levelTime;

    // Start is called before the first frame update
    void Start()
    {
	   if(potatoManager == null)
		   potatoManager = GameObject.FindWithTag("Potato Manager");
	   if(grid == null)
		   grid = GameObject.Find("Grid");

        //Set initial modes active.
        //May have to move this elsewhere

        if(myModes.timesInitialized <= 4) {
            myModes.timesInitialized++;
        }  
        
        if(myModes.timesInitialized == 3) {
            myModes.timesInitialized = 4;
        }

        for(int j = 0; j < 4; j++) {
            myUI.toggleUI(j, false);
        }

        for(int i = 0; i < myModes.timesInitialized; i++) {
            myUI.toggleUI(i, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        changeMode();
        firingProjectiles();

        if(Input.GetKey("space")){
		    cropHarvest();
        }
        if(Input.GetKey("c"))
        {
            cropPlant();
        }
    }

    public int getAmmoCount()
    {
        return ammoCount;
    }
    // method added to be able to update the ammocount when enemies hit the farmer
    public void setAmmoCount(int newAmmoCount)
    {
        ammoCount = newAmmoCount;
    }

    //Handles Camera rotation
    public void movement(){
        //Rotating Camera
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        transform.up = mouse - transform.position;
    }

    //Toggles modes that are available
    public void toggleModes(int which) {
        if(which >= 0 && which < 4) {
            myModes.modesEnabled[which] = !myModes.modesEnabled[which];
        }
    }

    //Changes the mode for the gun
    //Also changes gunside firing info: times a projectile is fired, 
    //Firerate, and spray.
    public void changeMode() {
        int oldMode = mode;

        //Key 1: Default PotatoGun
        if(Input.GetKey(KeyCode.Alpha1) && mode != 0 && myModes.modesEnabled[0]) {
            mode = 0;
        } else if(Input.GetKey(KeyCode.Alpha2) && mode != 1 && myModes.modesEnabled[1]) {
            //Key 2:Fries MachineGun

            mode = 1;
        } else if(Input.GetKey(KeyCode.Alpha3) && mode != 2 && myModes.modesEnabled[2]) {
            //Key 3: Slow, but powerful

            mode = 2;
        } else if(Input.GetKey(KeyCode.Alpha4) && mode != 3 && myModes.modesEnabled[3]) {
            //Key 4: Shotgun

            mode = 3;
        } else if(Input.GetAxis("Mouse ScrollWheel") > 0f) {
            //Scroll Up
            mode++;

            if(mode > 3) {
                mode = 0;
            }

            while(!myModes.modesEnabled[mode]) {
                mode++;

                if(mode > 3) {
                    mode = 0;
                }
            }
        } else if(Input.GetAxis("Mouse ScrollWheel") < 0f) {
            //Scroll Down
            mode--;

            if(mode < 0) {
                    mode = 3;
            }

            while(!myModes.modesEnabled[mode]) {
                mode--;

                if(mode < 0) {
                    mode = 3;
                }
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

        ammo.text = ": " + ammoCount;
        if(ammoCount == 0){
            potatoDead.SetActive(true);
        }

        //Firing Projectile
        if((Input.GetKey(KeyCode.Mouse0)) && timeCounter > cooldown) {

            changeText();
            
            if (ammoCount > 0){
                ammoCount--;
                potato.SetActive(true);
                

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
                potato.SetActive(false);
                potatoDead.SetActive(false);
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
        
        if(inRange == true && (lastHarvestTime-Timer.levelTime) > harvestingCooldown){
            int harvested = potatoManager.GetComponent<PotatoManager>().HarvestPotato(targetCrop, 0f);
            ammoCount += harvested;
            lastHarvestTime = Timer.levelTime;
            potato.SetActive(true);
            potatoDead.SetActive(false);
        }
        targetCrop = null;
		return inRange;
    }
	
	public bool cropPlant(){
		
        //check that cooldown has passed
        if(lastPlantTime-Timer.levelTime < plantingCooldown)
            return false;

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
        lastPlantTime = Timer.levelTime;

        // checking if the potatoCount has increased to 1, changing enemy pathing static variable when first new crop is planted
        if (potatoManager.GetComponent<PotatoManager>().PotatoCount() == 1)
        {
            BroccoliBehavior.pathingMode = false;
            CarrotBehavior.pathingMode = false;
            TomatoBehavior.pathingMode = false;
        }

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
