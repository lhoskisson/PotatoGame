using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManagerSet : MonoBehaviour
{
    public GunManager myModes;
    
    // Start is called before the first frame update
    void Awake()
    {
        myModes.timesInitialized = 0;

        for(int i = 1; i < 4; i++) {
            myModes.modesEnabled[i] = false;
        }
    }
}
