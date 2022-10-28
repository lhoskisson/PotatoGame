using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoManager : MonoBehaviour
{
	public GameObject potato_crop;
	private List<GameObject> spawned_potatoes;
    // Start is called before the first frame update
    void Start()
    {
		spawned_potatoes = new List<GameObject>();
        SpawnPotatoes(new Vector3(0,0,1), 5, 100);
    }
	
	/*
		Searches the list of spawned potatoes and returns the one closest to the given location
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
	
	public void RemovePotato(){}

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
						spawned_potatoes.Add(Instantiate(potato_crop, spawn_location, Quaternion.identity) as GameObject);
					}
				}
			}
		}
	}
}
