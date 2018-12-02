using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour {

	private int[] counterArray = {0,0,0,0,0,0,0,0,0}; 

	private bool ScarificeArm = false;
	private bool ScarificeEye = false;
	private bool ScarificeLeg = false;

	private bool hasTankArm = false;
	private bool hasTankEye = false;
	private bool hasTankLeg = false;

	private bool hasBearArm = false;
	private bool hasBearEye = false;
	private bool hasBearLeg = false;

	private bool hasJackArm = false;
	private bool hasJackEye = false;
	private bool hasJackLeg = false;

	private PlayerMovement player;

	public void ifScarificeArm(bool answer)
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

	public void ifScarificeEye(bool answer)
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


	public void ifScarificeLeg(bool answer)
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












	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
