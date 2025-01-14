﻿using UnityEngine;
using System.Collections;

public class Level2 : MonoBehaviour
{
	public SceneFade Fade;
	public Collider player;
	bool endScene = false;

	public void OnTriggerEnter(Collider playerCol)
	{
			Debug.Log("Level 2 Complete");
			endScene = true;
	}

    public void Update()
    {
		if ((Input.GetKey(KeyCode.Escape) == true) || (Input.GetButton("Start_Button")))
		{
			Fade.QuitScene(0);
		}
		else if ((Input.GetKeyUp(KeyCode.Escape)) || (Input.GetButtonUp("Start_Button")))
		{
			Fade.ResetAlpha();
		}
        //debug use only
		if (Input.GetKeyDown(KeyCode.Alpha1) == true)
        {
            Application.LoadLevel(1);
        }
		if (endScene)
		{
			Fade.EndScene(0);
		}
    }
}
