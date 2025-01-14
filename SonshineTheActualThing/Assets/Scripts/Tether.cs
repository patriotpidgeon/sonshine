﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Jacob Connelly
/// Date Created: 14/8/14
/// Last Updated: 17/9/14
/// Description: 
/// This script will be used to tether the child to the parent
/// </summary>

public class Tether : MonoBehaviour
{

	// Use this for initialization
	public Watson m_TheChild;
	float fMinDistanceToTether;
	void Start()
	{
		fMinDistanceToTether = 5.0f;
	}

	// Update is called once per frame
	void Update()
	{

		// if the space key is pressed tether or un tether the child
		if (Input.GetKeyDown(KeyCode.Space) || ((Input.GetAxis("Left_Trigger") > 0.00f) && (Input.GetAxis("Right_Trigger") > 0.00f)))
		{
			// if the child is NOT tethered 
			if (m_TheChild.bTethered == false)
			{
				if (Vector3.Distance(m_TheChild.transform.position, transform.position) < fMinDistanceToTether)
				{
					m_TheChild.bTethered = true;
					m_TheChild.m_light.setIntensity(0.4f);
					m_TheChild.m_light.SetRange(75.0f);
				}
			}


		}
		else if ((Input.GetKeyDown(KeyCode.Space) || (Input.GetButtonUp("B_Button"))) && (m_TheChild.bTethered == true))//if tethered untether here
		{
			if (m_TheChild != null)
			{
				m_TheChild.bTethered = false;
				m_TheChild.m_light.setIntensity(0.1f);
				m_TheChild.m_light.SetRange(35.0f);
			}
		}
	}
}