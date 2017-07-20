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

	public GameObject draw;
	public GameObject discard;
	public GameObject statePanel;
	private StatePanelScript statePanelScript;
	public GameObject confirmButton;
	private ButtonScript confirmButtonScript;
	private bool newState; // Used to skip setup after first time set in states. ONLY USED IN ExecuteGameState!


	void Awake(){
		Debug.Log ("GameController Awake()");
		Debug.logger.logEnabled = true;
		mS = MainStates.Election;
		eS = ElectionStates.PassPresident;
		lS = LegislationStates.None;
		pS = PowerStates.None;
		statePanelScript = statePanel.GetComponent<StatePanelScript> ();
		statePanel.SetActive (false);
		confirmButtonScript = confirmButton.GetComponent<ButtonScript> ();
		confirmButton.SetActive (false);

		newState = true;

	}

	void Update(){
		ExecuteGameState ();
	}

	void restart(){} // Restart game

	private void ExecuteGameState(){
		if (newState) {
			//State Error check We should never have more than one of the substates as non zero
			Debug.Log (string.Format ("||Main: {0}|| Election: {1}|| Legislate: {2}|| Power: {3}||", mS, eS, lS, pS));
		}
		Debug.Assert(!((int)eS != 0 && (int)lS != 0 || (int)eS != 0 && (int)pS != 0 || (int)lS != 0 && (int)pS != 0),
			string.Format ("State Error: multiple states not 'None': {0},{1},{2}", (int)eS, (int)lS, (int)pS));
		bool done = false;
		while(!done){
			if (mS == MainStates.Election){ // In election state
				Debug.Assert (eS != ElectionStates.None, "State Error: Entered into Election state with no substate");
				if (eS == ElectionStates.PassPresident) {
					if (newState) {
						Debug.Log ("State: Election::Pass Presidency");
						statePanelScript.SetText ("Election", "Pass Presidency");
						confirmButtonScript.SetText ("Confirm", setActive: true);
						newState = false;
					}
					if (!confirmButtonScript.EvalConfirm ()) { // If no button press
						done = true; // done with state execute, keep waiting
					}
					else { // We got a button press, go to next state
						eS = ElectionStates.ChooseChancellor;	
						newState = true;
					}
				}
				if (eS == ElectionStates.ChooseChancellor){
					if (newState) {
						Debug.Log ("State: Election::Choose Chancellor");
						statePanelScript.SetText ("Election", "Choose Chancellor");
						confirmButtonScript.SetText ("Confirm", setActive: true);
						newState = false;
					}
					if (!confirmButtonScript.EvalConfirm ()) { // If no button press
						done = true; // done with state execute, keep waiting
					}
					else { // We got a button press, go to next state
						eS = ElectionStates.Vote;	
						newState = true;
					}
				}
				if (eS == ElectionStates.Vote){
					done = true;
				}
			}
			else if(mS == MainStates.Legislation){ // In Legislation state
			}
			else if(mS == MainStates.ExecutivePowers){ // In ExecutivePowers state
			}
			else
				Debug.LogError (string.Format ("Main State is not an expected value? MS: {0}", mS));
		}
	}
};
