using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManagerSet : MonoBehaviour
{
    public GunManager myModes;
    
    // Start is called before the first frame update
    void Start()
    {
        myModes.timesInitialized = 0;
    }
}