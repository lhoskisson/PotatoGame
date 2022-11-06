using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	//time that the level lasts in seconds
	public float levelTime = 180.0f; 
	
	public GameObject timeText;

    void Start()
    {
        timeText = GameObject.Find("TimeText");
    }


    void Update()
    {
		float previousTime = levelTime;
        levelTime -= Time.deltaTime;
		
		//update text if there is a change in the seconds value.
		if(((int) previousTime) - ((int) levelTime) > 0)
		{
			timeText.GetComponent<Text>().text = ((int) levelTime).ToString();
		}
		
		//if time up, end level
		if(levelTime <= 0)
		{
			//stop enemies
			
			
			//harvest all potato crops
			GameObject[] potatoManagers = GameObject.FindGameObjectsWithTag("Potato Manager");
			foreach(GameObject pm in potatoManagers)
				pm.GetComponent<PotatoManager>().HarvestAllPotatoes();
			
			//go to next level
		}
    }
}
