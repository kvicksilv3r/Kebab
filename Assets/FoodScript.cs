using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
		Setup();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Setup()
	{
		string food_s = "";

		switch (Random.Range(0, 5))
		{
			case 0:
				food_s = "Hambuger";
				break;

			case 1:
				food_s = "Cake";
				break;

			case 2:
				food_s = "Donuts";
				break;

			case 3:
				food_s = "IceCream";
				break;

			case 4:
				food_s = "Waffle";
				break;
		}

		print(food_s);
		Instantiate(Resources.Load("FoodPrefab/" + food_s) as GameObject, transform.GetChild(0), false);
	}
}
