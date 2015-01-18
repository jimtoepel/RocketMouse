using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	public Animator startButton;
	public Animator settingsButton;


	public void StartGame ()
	{
		Application.LoadLevel ("RocketMouse");
		
	}


	public void OpenSettings()
	{

		startButton.SetBool ("isHidden", true);
		settingsButton.SetBool ("isHidden", true);
	
	}

}

