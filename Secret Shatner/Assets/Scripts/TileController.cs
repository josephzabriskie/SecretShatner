using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Tile controller manages the games legislation tiles. Their draws, discards and displays
public class TileController : MonoBehaviour {
	private int numLibTiles;
	private int numFacTiles;
	private int numTiles;
	private List<Tile> tiles;

	void Awake(){
		numLibTiles = 10;
		numFacTiles = 5;
		numTiles = numLibTiles + numFacTiles;
		//tiles = new List<bool> ();
		for (int i = 0; i < numLibTiles; i++) {
		}
	}
}
