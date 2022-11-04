using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
	//time that the level lasts in seconds
	public float levelTime = 180.0f; 
	
	//potato manager to 
	public GameObject potatoManager;
	
	

    void Start()
    {
        
    }


    void Update()
    {
        levelTime -= Time.deltaTime;
    }
}
