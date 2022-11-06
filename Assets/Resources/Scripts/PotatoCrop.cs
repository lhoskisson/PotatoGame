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
	private int baseYield = 0;
	
	//the time in seconds that this potato crop has existed
	private float lifetime = 0;

	//times when the potato crop automatically changes growth state.
	private float sproutTime = 30;

	private float halfTime = 60;

	private float fullTime = 90;
	
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
	
	public void setTransitionTimes(float s, float h, float f)
	{
		sproutTime = s;
		halfTime = h;
		fullTime = f;
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
				baseYield = 0;
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Farmland_Seed");
				break;
			case GrowthState.Sprout:
				baseYield = 2;
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Farmland_Sprout");
				break;
			case GrowthState.Half:
				baseYield = 5;
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Farmland_Half");
				break;
			case GrowthState.Full:
				baseYield = 10;
				GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Farmland_Full");
				break;
		}
	}
	
	/*
		Returns the baseYield of the crop.
		Used by the PotatoManager to determine total yeild when harvested by the player.
	*/
	public int GetBaseYield()
	{
		return baseYield;
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
		
		lifetime += Time.deltaTime;
		if(lifetime > sproutTime && growthState == GrowthState.Seed)
			ChangeGrowthState(GrowthState.Sprout);
		if(lifetime > halfTime && growthState == GrowthState.Sprout)
			ChangeGrowthState(GrowthState.Half);
		if(lifetime > fullTime && growthState == GrowthState.Half)
			ChangeGrowthState(GrowthState.Full);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BroccoliProjectile(Clone)")
        {
			// hardcoding broccoli projectile damage
			ApplyDamage(BroccoliProjectile.broccoliDamage);
			Destroy(collision.gameObject);
        }
    }
}
