using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSelectionScript : MonoBehaviour {
	public List<GameObject> tileButtonList;
	private List<bool> activeList;
	private float spacing;
	private float buttonWidth;


	void Awake(){
		spacing = 30;
		buttonWidth = tileButtonList [0].GetComponent<RectTransform> ().rect.width;
		foreach(GameObject tileButton in tileButtonList){
			tileButton.SetActive (false);
		}
	}

	public void DislayTileSelection(List<bool> tiles){
		if (tiles.Count > 3 || tiles.Count < 2){ // Should only ever need to display 2 or 3 tiles
			Debug.Log (string.Format ("Bad length of input {0}", tiles.Count));
		}
		if (tiles.Count == 2){
			Vector3 temp = tileButtonList [0].transform.position;
			temp = new Vector3 (buttonWidth / 2 + spacing / 2, temp.y, temp.z);
			tileButtonList [0].transform.position = temp;
			temp = tileButtonList [1].transform.position;
			temp = new Vector3 ((buttonWidth / 2 + spacing / 2) * - 1, temp.y, temp.z);
			tileButtonList [1].transform.position = temp;
		}
		if (tiles.Count == 3){
			Vector3 temp = tileButtonList [0].transform.position;
			temp = new Vector3 (0, temp.y, temp.z);
			tileButtonList [0].transform.position = temp;
			temp = tileButtonList [1].transform.position;
			temp = new Vector3 (buttonWidth + spacing, temp.y, temp.z);
			tileButtonList [1].transform.position = temp;
			temp = tileButtonList [2].transform.position;
			temp = new Vector3 ((buttonWidth + spacing) * - 1, temp.y, temp.z);
			tileButtonList [2].transform.position = temp;
		}
			
	}

	public void HideTileSelection(){
		
	}

}
