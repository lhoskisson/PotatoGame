using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoCrop : MonoBehaviour
{
	public enum GrowthState
	{
		Seed,
		Sprout,
		Half,
		Full
	}
	
	//the current growth state of the potato crop
	private GrowthState growthState = GrowthState.Seed;
	
	//the base amount of yeild (potatoes) that the crop will give the player when harvested
	private int baseYeild = 0;
	
	//the default health for the potato crop
	public const int DEFAULT_HEALTH = 50;
	
	//the amount of health the crop has
	public int health = DEFAULT_HEALTH;
	
	//the PotatoManger which should hold a reference to this PotatoCrop
	public GameObject potatoManager;

    public void Start()
    {
		if(potatoManager == null)
			potatoManager = GameObject.FindWithTag("Potato Manager");
		health = DEFAULT_HEALTH;
		ChangeGrowthState(GrowthState.Seed);
    }
	
	/*
		Changes the state of the potatoCrop to the given state, and performs any
		associated routines with that change in state.
	*/
	public void ChangeGrowthState(GrowthState g)
	{
		growthState = g;
		switch(growthState)
		{
			case GrowthState.Seed:
				baseYeild = 0;
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Farmland_Seed");
				break;
			case GrowthState.Sprout:
				baseYeild = 2;
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Farmland_Sprout");
				break;
			case GrowthState.Half:
				baseYeild = 5;
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Farmland_Half");
				break;
			case GrowthState.Full:
				baseYeild = 10;
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Farmland_Full");
				break;
		}
	}
	
	/*
		Returns the baseYeild of the crop.
		Used by the PotatoManager to determine total yeild when harvested by the player.
	*/
	public int GetBaseYeild()
	{
		return baseYeild;
	}

    public void ApplyDamage(int damage)
	{
		health -= damage;
	}
	
	
	public void OnDestroy()
	{
		//play sound, animation, and/or other events
	}
	
	public void Update()
	{
		if(health <= 0)
		{
			potatoManager.GetComponent<PotatoManager>().RemovePotato(gameObject);
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BroccoliProjectile(Clone)")
        {
			// hardcoding broccoli projectile damage
			applyDamage(BroccoliProjectile.broccoliDamage);
			Destroy(collision.gameObject);
        }
    }
}
