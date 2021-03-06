using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailCountScript : MonoBehaviour {

	private int failCount;
	private int failCountMax;
	public SpriteRenderer[] failIconList; //Fail Icon 1 - 4 

	void Awake(){
		DeactivateSprites ();
		failCount = 0;
		failCountMax = 4;
	}

	void SetSpriteActive(int spriteNum){
		failIconList [spriteNum].enabled = true;
	}

	void DeactivateSprites(){
		foreach(SpriteRenderer icon in failIconList){
			icon.enabled = false;
		}
	}

	public void AddCount(){
		if (failCount < failCountMax) {
			failCount += 1;
			SetSpriteActive (failCount - 1);
		}
	}

	public void ResetCount(){
		failCount = 0;
		DeactivateSprites ();
	}
}
