using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateObjectScript : MonoBehaviour {

	public List<GameObject> mainObjects; // Objects to hide when we deactivate
	public GameObject showObject; // Button press shows our State UI
	public GameObject hideObject; // Button press hides our State UI
	private Dictionary<GameObject, bool> stateObjectActiveDict; // Memory of what we nee

	void Awake(){
		stateObjectActiveDict = new Dictionary<GameObject, bool>();
		foreach (GameObject obj in mainObjects){
			stateObjectActiveDict.Add (obj, false);
		}
		hideObject.SetActive (true);
		showObject.SetActive (false);
	}

	public void SetActiveStateObject(GameObject obj, bool active){
		if (obj.GetInstanceID () == showObject.GetInstanceID ()){
			Debug.LogError ("The 'Show game state' button can't be disable through script");
			return;
		}
		foreach (GameObject mainObj in mainObjects){
			if (obj.GetInstanceID () == mainObj.GetInstanceID ()){
				stateObjectActiveDict [obj] = active;
				obj.SetActive (active);
				return;
			}

		}
		Debug.Log ("Unable to find requested object to disable");
		return;
	}

	public void ShowStateObjects(bool show){
		Debug.Log (string.Format ("Show State Objects {0}", show));
		foreach(KeyValuePair<GameObject,bool> pair in stateObjectActiveDict){
			if (pair.Value){
				pair.Key.SetActive (show);
			}
		}
		hideObject.SetActive (show);
		showObject.SetActive (!show);
	}
}

