using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Tile controller manages the games legislation tiles. Their draws, discards and displays

public static class ListExtension{
	public static bool Pop(this List<bool> list){
		bool item = list [0];
		list.RemoveAt (0);
		return item;
	}

	public static void LogList(this List<bool> list, string preamble = ""){
		string outstr = preamble;
		for (int i = 0; i < list.Count; i ++){
			outstr += " " + list [i].ToString () + ",";
		}
		Debug.Log (outstr);
	}
	public static void ShuffleList(this List<bool> List){
		System.Random rnd = new System.Random();
		for(int i = List.Count; i > 1; i--) {
			int pos = rnd.Next (i);
			bool x = List[i - 1];
			List[i - 1] = List[pos];
			List[pos] = x;
		}
	}
}

public class TileController {
	private int numLibTiles;
	private int numFacTiles;
	private List<bool> drawTiles;
	private List<bool> discardTiles;

	public TileController(int numL, int numF){
		numLibTiles = numL;
		numFacTiles = numF;
		drawTiles = new List<bool> ();
		discardTiles = new List<bool> ();
		for (int i = 0; i < numLibTiles; i++) {
			drawTiles.Add (true);
		}
		for (int i = 0; i < numFacTiles; i++){
			drawTiles.Add (false);
		}
		drawTiles.ShuffleList ();
	}

	public List<bool> Draw(int numTiles){
		//TODO Check if we have enough tiles to remove, if not pull from discard pile
		Debug.Log (string.Format ("Drawing {0} tiles, make sure to discard!", numTiles));
		if (numTiles > drawTiles.Count) { // If not enough tiles to draw, we need to shuffle and add discard
			if (numTiles < drawTiles.Count + discardTiles.Count) { // Make sure enough tiles total
				Debug.Log ("Shuffling and adding discard to draw");
				discardTiles.ShuffleList ();
				drawTiles.AddRange (discardTiles);
				discardTiles.Clear ();
			}
			else{
				Debug.LogError (string.Format ("Not enough Tiles in play to draw! Len draw{0}, Len discard {1}", drawTiles.Count, discardTiles.Count));
			}
		}
		List<bool> outlist = new List<bool> ();
		for(int i = 0; i < numTiles; i++){
			outlist.Add (drawTiles.Pop ());
		}
		return outlist;
	}

	public void Discard(List<bool> discardList){
		Debug.Log (string.Format ("Discarding {0} tiles", discardList.Count));
		discardTiles.AddRange (discardList);
	}

	public int GetDrawCount(){
		return drawTiles.Count;
	}

	public int GetDiscardCount(){
		return discardTiles.Count;
	}

	public void LogInfo(){
		drawTiles.LogList ("Draw Tiles:");
		discardTiles.LogList ("Discard Tiles:");
	}
}