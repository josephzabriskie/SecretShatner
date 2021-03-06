using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePanelScript : MonoBehaviour {
	void Awake(){
	}

	public void SetText(string mainText, string subText, bool setActive = true){
		Debug.Log (string.Format ("SetText():: MainText: '{0}', SubText: '{1}', SetActive: {2}", mainText, subText, setActive));
		Text[] stateTexts = gameObject.GetComponentsInChildren<Text> ();
		stateTexts [0].text = mainText;
		stateTexts [1].text = subText;
		gameObject.SetActive (setActive);
	}

	public void SetTopPosition(){
		Debug.Log ("Set state panel to top position");
		gameObject.transform.localPosition += new Vector3(0,105,0);
	}

	public void SetMidPosition(){
		Debug.Log ("Set state panel to mid position");
		gameObject.transform.localPosition += new Vector3(0,0,0);
	}
}

