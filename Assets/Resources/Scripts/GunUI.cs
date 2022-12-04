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

    //For now, the alphanumeric 7, 8, 9 are shortcuts to enable the modes
    void Update() {
        if(Input.GetKey(KeyCode.Alpha7)) {
            toggleUI(1, true);
        } else if(Input.GetKey(KeyCode.Alpha8)) {
            toggleUI(2, true);
        } else if(Input.GetKey(KeyCode.Alpha9)) {
            toggleUI(3, true);
        }
    }

    //Tells the gun to swap to a certain mode
    //Used when buttons are pushed 
    public void swap(int which) {
        if(myGun.modesEnabled[which]) {
            myGun.setMode(which);
            current = which;
        }
    }

    //Tells the UI to set to a certain mode
    //Used by the PotatoGun
    public void setVal(int which) {
        if(which != current && which >= 0 && which < 4) {
            myGunUI[which].SetIsOnWithoutNotify(true);
            myGunUI[current].SetIsOnWithoutNotify(false);
            current = which;
        }
    }

    //Sets a button associated with one mode to be enabled or disabled
    //Also updates modesEnabled in PotatoGun
    public void toggleUI(int which, bool tog) {
        myGunUI[which].enabled = tog;
        myGun.modesEnabled[which] = tog;

        if(tog) {
            myGunUI[which].interactable = true;
        } else {
            myGunUI[which].interactable = false;
        }
    }
}
