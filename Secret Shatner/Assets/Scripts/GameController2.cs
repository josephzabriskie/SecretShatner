using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController2 : MonoBehaviour {
	public enum MainStates {Election, Legislation, ExecutivePowers}; // Main Gameplay sections
	public enum ElectionStates {None, PassPresident, ChooseChancellor, Vote}; // Actionable Election states
	public enum LegislationStates {None, PresidentLegislate, ChancellorLegislate}; //Actionable Legislation states
	public enum PowerStates {None, Investigate, SpecialElection, PolicyPeek, Execution, VetoEnable}; //Executive powers
	public MainStates mS; // Main game stafte tracking
	public ElectionStates eS; // Election state tracking
	public LegislationStates lS;
	public PowerStates pS;

	//UI Objects
	public Text drawCount;
	public Text discardCount;
	public GameObject statePanel;
	private StatePanelScript statePanelScript;
	public GameObject confirmButton;
	private ButtonScript confirmButtonScript;
	public GameObject jaButton;
	private ButtonScript jaButtonScript;
	public GameObject neinButton;
	private ButtonScript neinButtonScript;
	public GameObject FailCounter;
	private FailCountScript failCountScript;
	private TileController tileController;
	public GameObject gameStateObject;
	private GameStateObjectScript gameStateObjectScript;
	private bool newState; // Used to skip setup after first time set in states. ONLY USED IN ExecuteGameState!


	void Awake(){
		Debug.Log ("GameController Awake()");
		Debug.logger.logEnabled = true;
		mS = MainStates.Election;
		eS = ElectionStates.PassPresident;
		lS = LegislationStates.None;
		pS = PowerStates.None;
		statePanelScript = statePanel.GetComponent<StatePanelScript> ();
		confirmButtonScript = confirmButton.GetComponent<ButtonScript> ();
		jaButtonScript = jaButton.GetComponent <ButtonScript> ();
		neinButtonScript = neinButton.GetComponent <ButtonScript> ();
		failCountScript = FailCounter.GetComponent <FailCountScript> ();
		tileController = new TileController (6, 11);
		drawCount.text = tileController.GetDrawCount ().ToString ();
		discardCount.text = tileController.GetDiscardCount ().ToString ();
		gameStateObjectScript = gameStateObject.GetComponent <GameStateObjectScript> ();

		newState = true;

	}

	void Update(){
		ExecuteGameState ();
	}

	void PrezDraw3(){
		
	}

	void restart(){} // Restart game

	private void ExecuteGameState(){
		//if (newState) {
			//State Error check We should never have more than one of the substates as non zero
		//	Debug.Log (string.Format ("||Main: {0}|| Election: {1}|| Legislate: {2}|| Power: {3}||", mS, eS, lS, pS));
		//}
		Debug.Assert(!((int)eS != 0 && (int)lS != 0 || (int)eS != 0 && (int)pS != 0 || (int)lS != 0 && (int)pS != 0),
			string.Format ("State Error: multiple states not 'None': {0},{1},{2}", (int)eS, (int)lS, (int)pS));
		if (mS == MainStates.Election){ // In election state
			Debug.Assert (eS != ElectionStates.None, "State Error: Entered into Election state with no substate");
			if (eS == ElectionStates.PassPresident) {
				if (newState) {
					Debug.Log ("State: Election::Pass Presidency");
					statePanelScript.SetText ("Election", "Pass Presidency");
					gameStateObjectScript.SetActiveStateObject (statePanel, true);
					confirmButtonScript.SetText ("Confirm");
					gameStateObjectScript.SetActiveStateObject (confirmButton, true);
					gameStateObjectScript.SetActiveStateObject (jaButton, false);
					gameStateObjectScript.SetActiveStateObject (neinButton, false);
					newState = false;
				}
				if (confirmButtonScript.EvalConfirm ()) { // If no button press
					eS = ElectionStates.ChooseChancellor;	
					newState = true;
				}
			}
			if (eS == ElectionStates.ChooseChancellor){
				if (newState) {
					Debug.Log ("State: Election::Choose Chancellor");
					statePanelScript.SetText ("Election", "Choose Chancellor");
					//confirmButtonScript.SetText ("Confirm"); // Maybe not needed?
					newState = false;
				}
				if (confirmButtonScript.EvalConfirm ()) { // If no button press
					eS = ElectionStates.Vote;	
					newState = true;
				}
			}
			if (eS == ElectionStates.Vote){
				if (newState){
					Debug.Log ("State: Election::Vote");
					statePanelScript.SetText ("Election", "Vote");
					//confirmButtonScript.SetActive (false);
					gameStateObjectScript.SetActiveStateObject(confirmButton, false);
					//jaButtonScript.SetActive (true);
					gameStateObjectScript.SetActiveStateObject(jaButton, true);
					//neinButtonScript.SetActive (true);
					gameStateObjectScript.SetActiveStateObject(neinButton, true);
					newState = false;
				}
				if(jaButtonScript.EvalConfirm ()){
					Debug.Log ("Ja Press");
					mS = MainStates.Legislation;
					lS = LegislationStates.PresidentLegislate;
					eS = ElectionStates.None;
					//jaButtonScript.SetActive (false);
					gameStateObjectScript.SetActiveStateObject (jaButton, false);
					//neinButtonScript.SetActive (false);
					gameStateObjectScript.SetActiveStateObject (neinButton, false);
					failCountScript.ResetCount ();
					newState = true;
				}
				else if(neinButtonScript.EvalConfirm ()){
					Debug.Log ("Nien Press");
					eS = ElectionStates.PassPresident;
					//jaButtonScript.SetActive (false);
					gameStateObjectScript.SetActiveStateObject (jaButton, false);
					//neinButtonScript.SetActive (false);
					gameStateObjectScript.SetActiveStateObject (neinButton, false);
					failCountScript.AddCount ();
					newState = true;
				}
				else{}
			}
		}
		else if(mS == MainStates.Legislation){ // In Legislation state
			if (lS == LegislationStates.PresidentLegislate) {
				if (newState) {
					Debug.Log ("State: Legislation::PresidentLegislate");
					statePanelScript.SetText ("Legislation", "President Legislate");
					statePanelScript.SetTopPosition ();
					//confirmButtonScript.SetActive (true);
					newState = false;
				}
			}
		}
		else if(mS == MainStates.ExecutivePowers){ // In ExecutivePowers state
		}
		else
			Debug.LogError (string.Format ("Main State is not an expected value? MS: {0}", mS));

	}
};
