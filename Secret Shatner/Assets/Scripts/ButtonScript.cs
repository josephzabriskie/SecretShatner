using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	private bool confirm;

	void Awake(){
		SetActive (false);
	}

	public void SetText(string mainText = null, bool setActive = true){
		SetActive (true);
		Debug.Log (string.Format ("SetText():: MainText: '{0}', SetActive: {1}", mainText, setActive));
		Text stateText = gameObject.GetComponentInChildren<Text> ();
		stateText.text = mainText;
		SetActive (setActive);
	}

	public void SetActive(bool setActive){
		gameObject.SetActive (setActive);
	}

	public void Press(){
		Debug.Log ("Press() button press.");
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
