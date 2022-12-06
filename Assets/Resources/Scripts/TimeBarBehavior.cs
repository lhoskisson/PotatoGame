using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBarBehavior : MonoBehaviour
{
    public const float startLevelTime = 180f;
    public Slider timeBar;
    public float time;
    public static float maxTime = startLevelTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float previousTime = time;
        time += Time.deltaTime;

        if (((int)previousTime) - ((int)time) < 0)
        {
            timeBar.value = time;
            timeBar.maxValue = maxTime;
        }
    }
}
