using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	private bool confirm;

	void Awake(){
	}

	public void SetText(string mainText = null){
		Debug.Log (string.Format ("SetText():: MainText: '{0}'", mainText));
		Text stateText = gameObject.GetComponentInChildren<Text> ();
		stateText.text = mainText;
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
