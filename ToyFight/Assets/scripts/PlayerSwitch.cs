using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour {


	static private int[] counterArray = {0,0,0,0,0,0,0,0,0}; 

	static private string nextBoss;

	static private bool ScarificeArm = false;
	static private bool ScarificeEye = false;
	static private bool ScarificeLeg = false;

	static private bool hasTankArm = false;
	static private bool hasTankEye = false;
	static private bool hasTankLeg = false;

	static private bool hasBearArm = false;
	static private bool hasBearEye = false;
	static private bool hasBearLeg = false;

	static private bool hasJackArm = false;
	static private bool hasJackEye = false;
	static private bool hasJackLeg = false;

	static private PlayerMovement player;

	public void setNextBoss(string Boss)
	{
		nextBoss = Boss;
	}

	static public void ifScarificeArm(bool answer)
	{
		if (answer) {
			if (hasTankArm == true) {
				counterArray [0] = 1;
			}
			if (hasBearArm == true) {
				counterArray [3] = 1;
			}
			if (hasJackArm == true) {
				counterArray [6] = 1;
			}

			hasTankArm = false;
			hasBearArm = false;
			hasJackArm = false;

			player.setFireRate (0.25f);

		} else {
			if (counterArray [0] == 1) {
				hasTankArm = true;
			}
			if (counterArray [3] == 1) {
				hasBearArm = true;
			}
			if (counterArray [6] == 1) {
				//hasJackArm == true;
			}

			player.setFireRate (0.5f);
		}
			
	}

	static public void ifScarificeEye(bool answer)
	{
		if (answer) {
			if (hasTankEye == true) {
				counterArray [1] = 1;
			}
			if (hasBearEye == true) {
				counterArray [4] = 1;
			}
			if (hasJackEye == true) {
				counterArray [7] = 1;
			}

			hasTankEye = false;
			hasBearEye = false;
			hasJackEye = false;

			//add blurr function

		} else {
			if (counterArray [1] == 1) {
				hasTankEye = true;
			}
			if (counterArray [4] == 1) {
				hasBearEye = true;
			}
			if (counterArray [7] == 1) {
				hasJackEye = true;
			}

			//add bluur function

		}
	}


	static public void ifScarificeLeg(bool answer)
	{
		if (answer) {
			if (hasTankLeg == true) {
				counterArray [2] = 1;
			}
			if (hasBearLeg == true) {
				counterArray [5] = 1;
			}
			if (hasJackLeg == true) {
				counterArray [8] = 1;
			}

			hasTankLeg = false;
			hasBearLeg = false;
			hasJackLeg = false;

			//change speed
			player.setSpeedScale(25);

		} else {
			if (counterArray [2] == 1) {
				hasTankLeg = true;
			}
			if (counterArray [5] == 1) {
				hasBearLeg = true;
			}
			if (counterArray [8] == 1) {
				hasJackLeg = true;
			}

			//change speed
			player.setSpeedScale(50);

		}
	}



	//FIX IF THERE IS TIME 
	static public void switchTankArm()
	{
		if (hasTankArm == true) {
			hasTankArm = false;
		} else {
			hasTankArm = true;
		}
	}









	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
