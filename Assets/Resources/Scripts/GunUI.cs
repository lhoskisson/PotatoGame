using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUI : MonoBehaviour
{
    public Toggle[] myGunUI = new Toggle[4];
    public PotatoGunScript myGun;


    public int current;

    // Start is called before the first frame update
    void Start()
    {
        myGunUI[0].onValueChanged.AddListener(delegate {swap(0);});
        myGunUI[1].onValueChanged.AddListener(delegate {swap(1);});
        myGunUI[2].onValueChanged.AddListener(delegate {swap(2);});
        myGunUI[3].onValueChanged.AddListener(delegate {swap(3);});

        myGunUI[0].isOn = true;

        current = 0;
        swap(0);
    }

    public void swap(int which) {
        myGun.setMode(which);
        current = which;
    }

    public void setVal(int which) {
        if(which != current && which >= 0 && which < 4) {
            myGunUI[which].SetIsOnWithoutNotify(true);
            myGunUI[current].SetIsOnWithoutNotify(false);
            current = which;
        }
    }
}
