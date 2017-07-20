using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	public bool confirm;

	void Awake(){
		
	}

	public void SetText(string mainText, bool setActive = true){
		Debug.Log (string.Format ("SetText():: MainText: '{0}', SetActive: {1}", mainText, setActive));
		Text stateText = gameObject.GetComponentInChildren<Text> ();
		stateText.text = mainText;
		gameObject.SetActive (setActive);
	}

	public void Press(){
		Debug.Log ("Confirm() Button press.");
		confirm = true;
	}

	public bool EvalConfirm(){
		if(confirm){
			confirm = false;
			return true;
		}
		else{
			return false;
		}
	}

}
