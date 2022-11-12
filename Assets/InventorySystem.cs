using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private int numWeapons;

    public  GameObject[] weapons;
    public GameObject weaponInventory;
    public GameObject currentWeapon;
    public int weaponIndex;

    void Start(){
        addWeapons();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f){
            
            if (weaponIndex >= numWeapons - 1){
                changeWeaponBackward();
            } else {
                changeWeaponForward();
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f){
            
            if (weaponIndex <= 0){
                changeWeaponForward();
            } else {
                changeWeaponBackward();
            }
        }
    }
    public void addWeapons(){
        numWeapons = weaponInventory.transform.childCount;
        weapons = new GameObject[numWeapons];

        for (int i = 0; i < numWeapons; i++){
            weapons[i] = weaponInventory.transform.GetChild(i).gameObject;
            weapons[i].SetActive(false);
        }

        weapons[0].SetActive(true);
        currentWeapon = weapons[0];
        weaponIndex = 0;
    }
    public void changeWeaponForward(){

        if ( weaponIndex <= numWeapons - 1){
            weapons[0].SetActive(false);
            weaponIndex++;
            weapons[weaponIndex].SetActive(true);
            currentWeapon = weapons[weaponIndex];
        }
    }
    public void changeWeaponBackward(){

        if ( weaponIndex >= numWeapons - 1){
            weapons[weaponIndex].SetActive(false);
            weaponIndex--;
            weapons[weaponIndex].SetActive(true);
            currentWeapon = weapons[weaponIndex];
        }
    }
}
