using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringPointScript : MonoBehaviour
{

    public Sprite[] spriteArray = new Sprite[4];

    public void spriteChange(int mode) {
        SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer>();
        
        //Default
        Sprite curr = spriteArray[0];

        //Changes sprite to match given int
        if(mode == 1) {
            //Mandaline
            curr = spriteArray[1];
        } else if(mode == 2) {
            //Heater
            curr = spriteArray[2];
        } else if(mode == 3) {
            //Fries
            curr = spriteArray[3];
        }   

        rend.sprite = curr;
    }
}
