using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoCrop : MonoBehaviour
{
	public int health = 50;
	
	public GameObject potatoManager;

    void Start()
    {
        potatoManager = GameObject.Find("Potato Manager");
    }

    public void applyDamage(int damage)
	{
		health -= damage;
	}
	
	
	public void OnDestroy()
	{
	}
	
	public void Update()
	{
		if(health <= 0)
		{
			potatoManager.GetComponent<PotatoManager>().RemovePotato(gameObject);
		}
	}
}
