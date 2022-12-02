using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public const float startLevelTime = 180f;

    //time that the level lasts in seconds
    public static float levelTime = startLevelTime;
	
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
    }
}
