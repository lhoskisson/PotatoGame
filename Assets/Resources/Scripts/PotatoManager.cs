using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoManager : MonoBehaviour
{
	public GameObject potato_crop;
	private List<GameObject> spawned_potatoes;
	
	//the smallest modifier that could be applied to the crop yeild from luck
	public float harvestLuckMin = -0.1f;
	
	//the largest modifier that could be applied to the crop yeild from luck
	public float harvestLuckMax = 0.2f;

    void Start()
    {
		spawned_potatoes = new List<GameObject>();
        SpawnPotatoes(new Vector3(0,0,1), 5, 100);
    }
	
	/*
		Searches the list of spawned potato crops and returns the one closest to the given location
	*/
	public GameObject GetClosestPotato(Vector3 location)
	{
		if(spawned_potatoes.Count == 0) return null;

		GameObject closest_potato = null;
		float closest_distance = float.MaxValue;
		foreach(GameObject potato in spawned_potatoes)
		{
			if(closest_potato == null)
			{
				closest_potato = potato;
				closest_distance = Vector3.Distance(location, potato.transform.position);
			}
			if(Vector3.Distance(location, potato.transform.position) < closest_distance)
			{
				closest_potato = potato;
				closest_distance = Vector3.Distance(location, potato.transform.position);
			}
		}
		return closest_potato;
	}
	
	/*
		Sorts the list of spawned potato crops and returns an array of the closest to the given location.
		
		location: A Vector3 containing the reference location (location of enemy for example).
		amount: Integer value of the amount of potatoes to be returned.
	*/	
	public GameObject[] GetXClosestPotatoes(Vector3 location, int amount)
	{
		if(amount <= 0) return null;
		if(amount == 1)
		{
			GameObject[] p = new GameObject[1];
			p[0] = GetClosestPotato(location);
			return p;
		}
		
		if(amount > spawned_potatoes.Count)
			amount = spawned_potatoes.Count;
		
		//sort each potato crop based on its closeness to the given location
		List<GameObject> sortedPotatoes = new List<GameObject>(spawned_potatoes);
		sortedPotatoes.Sort(new PotatoDistanceComparer(location));
		
		//create and return array of <amount> closest potato crops.
		GameObject[] potatoes = new GameObject[amount];
		for(int i=0; i<amount; i++)
		{
			potatoes[i] = sortedPotatoes[i];
		}
		return potatoes;
	}
	
	/*
		Compare class used to compare potato crops based on given reference location.
		Used in GetXClosestPotatoes to sort potato crops.
	*/
	private class PotatoDistanceComparer : IComparer<GameObject>
	{
		private Vector3 referenceLocation;
		
		public PotatoDistanceComparer(Vector3 location)
		{
			referenceLocation = location;
		}
		
		public int Compare(GameObject x, GameObject y)
		{
			float d1 = Vector3.Distance(x.transform.position, referenceLocation);
			float d2 = Vector3.Distance(x.transform.position, referenceLocation);
			if(d1 > d2) return 1;
			if(d1 < d2) return -1;
			return 0;
		}
	}
	
	/*
		Harvests the given Potato Crop game object. Calculates and returns the amount of potatoes yeilded,
		and destroys the potato. The yeild is determined by a base yeild (distinguished by the crop's
		growth stage), scaled by the crops health percentage, luck, and a given bonus.
		
		p: A GameObject referencing the potato crop to be harvested.
		bonus: A percent increase of the base yeild. Added with the other scale values to modify the base yeild.
		Since this is used to scale, it should generally be between 0 and 1.
		
		Returns an integer value of potatoes harvested.
	*/
	public int HarvestPotato(GameObject p, float bonus=0f)
	{
		PotatoCrop cropScript = p.GetComponent<PotatoCrop>();
		float percentHealth = ((float) cropScript.health) / ((float) PotatoCrop.DEFAULT_HEALTH);
		float luck = Random.Range(harvestLuckMin, harvestLuckMax);
		int yield = (int) (cropScript.GetBaseYield()*(percentHealth+bonus+luck));
		if(RemovePotato(p))
			return yield;
		return 0;
	}
	
	/*
		Harvests all potato crops that this Potato Manager handles. Returns the yield of all these crops.
	*/
	public int HarvestAllPotatoes(float bonus=0f)
	{
		int total = 0;
		for(int i=0; i<spawned_potatoes.Count; i++)
		{
			total += HarvestPotato(spawned_potatoes[i], bonus);
			i=0; //reset to zero since the size of the list is changing each iteration.
		}
		return total;
	}
	
	/*
		Searches the list of spawned potatoes, if the given potato crop GameObject is found,
		it is removed from the list and the GameObject is Destroyed.
		
		p: The GameObject referencing the potato crop to be removed.
		
		Returns true if the given GameObject was in the list, removed, and destroyed.
		Otherwise returns false. If false, check if there are other PotatoMangers in the scene.
	*/
	public bool RemovePotato(GameObject p)
	{
		for(int i=0; i<spawned_potatoes.Count; i++)
		{
			if(spawned_potatoes[i] == p)
			{
				spawned_potatoes.RemoveAt(i);
				Destroy(p);
				return true;
			}
		}
		return false;
	}
	
	/*
		Spawns a Potato_Crop prefab at the given location.
	*/
	public void SpawnPotato(Vector3 location)
	{
		spawned_potatoes.Add(Instantiate(potato_crop, location, Quaternion.identity) as GameObject);
	}
	
	/*
		Spawns the Potato_Crop prefabs based on the given parameters.The parameters describe an area 
		in which the potatoes are placed, as well as the density and distribution of potatoes in the area.
		
		location: The world coordinates where the spawn area will be centered.
		radius: The distance from the center of the spawn area to the outer edges. If shape is a square
		then the radius is the distance from the center to the closest points of the square.
		density: The amount of potatoes per squared world unit.
		shape: 0 is a circle, 1 is a square.
		even: Set to true for an even distribution, otherwise uses a random distribution.
		
		CURRENTLY ONLY IMPLEMENTED FOR EVEN SQUARES
	*/
	public void SpawnPotatoes(Vector3 location, float radius, float density, int shape=1, bool even=true)
	{
		float area = 0;
		if(shape == 0) //circle
			area = Mathf.PI*radius*radius;
		if(shape == 1) //square
			area = 4*radius*radius;
		int potato_count = (int) (area*density);
		SpawnPotatoes(location, radius, potato_count, shape, even);
	}
	
	/*
		Spawns the Potato_Crop prefabs based on the given parameters.The parameters describe an area 
		in which the potatoes are placed, as well as the density and distribution of potatoes in the area.
		
		location: The world coordinates where the spawn area will be centered.
		radius: The distance from the center of the spawn area to the outer edges. If shape is a square
		then the radius is the distance from the center to the closest points of the square.
		potato_count: The amount of potatoes to be spawned.
		shape: 0 is a circle, 1 is a square.
		even: Set to true for an even distribution, otherwise uses a random distribution.
		
		CURRENTLY ONLY IMPLEMENTED FOR EVEN SQUARES
	*/
	public void SpawnPotatoes(Vector3 location, float radius, int potato_count, int shape=1, bool even=true)
	{
		if(shape == 0) //circle
		{
			
		}
		
		if(shape == 1) //square
		{
			//compute various attributes of the square
			Vector3 top_left = new Vector3(location.x-radius, location.y+radius, location.z);
			float side_length = 2*radius;
			if(even)
			{
				int iterations = (int) Mathf.Sqrt(potato_count);
				float increment = side_length/iterations;
				float center_adjust = increment/2;
				for(int i=0; i<iterations; i++)
				{
					for(int j=0; j<iterations; j++)
					{
						Vector3 spawn_location = 
						new Vector3(top_left.x+(i*increment)+center_adjust, top_left.y-(j*increment)-center_adjust, top_left.z);
						SpawnPotato(spawn_location);
					}
				}
			}
		}
	}
}
