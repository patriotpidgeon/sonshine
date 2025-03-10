﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AiBehaviour_ns;
using Agent_ns;


/// <summary>
/// Author: Jacob Connelly
/// Date Created: 13/8/14
/// Last Updated: 16/9/14
/// Description:
/// These classes will serve as the individual actions the ai may use. 
/// inherited from the AiBehaviour class
/// </summary>
namespace AiBehaviourPlus_ns
{
    /// <summary>
    /// if the agent is within range of the target that was set
    /// </summary>
    public class WithinRange : AiBehaviour
    {
        private float m_range2;
        
        public WithinRange(float a_range) { m_range2 = a_range * a_range; m_BehaviourType = BehaviourType.WITHINRANGE; }

        public override bool execute(Agent a_agent)
        {
            
            float dist2 = Vector3.Distance(a_agent.getPosition(), a_agent.getTarget());
           // Debug.Log("within range");
            if (dist2 < m_range2 && m_range2 != 0 && dist2 !=0)
            {
                //Debug.Log("executing within range as true");
                m_BehaviourType = BehaviourType.WITHINRANGE;
                return true;
            }
            else
            {
               // Debug.Log("executing within range as false");
                m_BehaviourType = BehaviourType.WITHINRANGE;
                return false;
            }

        }
    }

    // create target : find a target based on needs etc / boredem aslong as they arent tethered.
    public class CreateTarget : AiBehaviour
    {
        public List<ChildInteractable> PossibleDistractions;
        
        public CreateTarget() { m_BehaviourType = BehaviourType.CREATETARGET; }

        public ChildInteractable CalculateBestOption(Vector3 a_pos)
        {
             // calculate what will be set
            ChildInteractable tempInteracatable = null;
            for (int i = 0; i < PossibleDistractions.Count; i++)
            {
                if (PossibleDistractions[i] == null || PossibleDistractions[i].gameObject.activeInHierarchy == false)
                {
                    PossibleDistractions.Remove(PossibleDistractions[i]);
                }
                else
                {
                    float tempValue = Vector3.Distance(a_pos, PossibleDistractions[i].gameObject.transform.position);
                    //Debug.Log(tempValue);
                    // is the distraction close enough to be considered?
                    if (tempValue < PossibleDistractions[i].DistractionValues.fVisibleDistance)
                    {
                        // calculate weighting
                        PossibleDistractions[i].DistractionValues.fPostCheckValue = 0;

                        tempValue = tempValue * PossibleDistractions[i].DistractionValues.fDistanceWeightingFalloff
                        * PossibleDistractions[i].DistractionValues.fWeighting;

                        PossibleDistractions[i].DistractionValues.fPostCheckValue = tempValue;
                    }
                }
            }
            // check the values of all other distractions
            for (int d = 0; d < PossibleDistractions.Count; d++)
            {
                if (tempInteracatable == null)
                {
                    tempInteracatable = PossibleDistractions[d];
                }
                    // if the value is higher set that as the new distraction
                else if (PossibleDistractions[d].DistractionValues.fPostCheckValue > tempInteracatable.DistractionValues.fPostCheckValue)
                {
                    tempInteracatable = PossibleDistractions[d];
                }

            }

            // now ensure that it is within the the range
            if (tempInteracatable != null)
            {
                float tempDistance = Vector3.Distance(a_pos, tempInteracatable.gameObject.transform.position);
                if (tempDistance < tempInteracatable.DistractionValues.fVisibleDistance)
                    return tempInteracatable;
                else
                    return null;
            }
            else
                return null;
        }

        public override bool execute(Agent a_agent)
        {

           // Debug.Log("create target");
            // calculate the target
            ChildInteractable tempInteractable = CalculateBestOption(a_agent.getPosition());
            if (tempInteractable!=null && tempInteractable.DistractionValues.fPostCheckValue > a_agent.fBoredem)//
            {
                a_agent.setTarget(tempInteractable.gameObject.transform.position);
                m_BehaviourType = BehaviourType.CREATETARGET;
                return true;
            }
            else
            {
                a_agent.setTarget(a_agent.getPosition());
                m_BehaviourType = BehaviourType.CREATETARGET;
                return false;
            }
        }
    }
    
    // seek target : move towards current set target 
    public class SeekTarget : AiBehaviour
    {
       
        public SeekTarget() { m_BehaviourType = BehaviourType.SEEK; }
         // if the distance is greater than the pre defined threshold move towards the parent
        public override bool execute(Agent a_agent)
        {
           // Debug.Log("executing seek target");
           // Debug.Log("seek target");
            m_BehaviourType = BehaviourType.SEEK;
            a_agent.setPosition(Vector3.MoveTowards(a_agent.getPosition(), a_agent.getTarget(), a_agent.fMovementSpeed * Time.deltaTime));
            return true;
        }

    }

    public class IsClose : AiBehaviour
    {
       

        public IsClose() { m_BehaviourType = BehaviourType.ISCLOSE;}

        public override bool execute(Agent a_agent)
        {
            // if the agent is within below amount of distance
            if (Vector3.Distance(a_agent.getTarget(), a_agent.getPosition()) < 1.0f)
            {
                m_BehaviourType = BehaviourType.ISCLOSE;
                return true;
            }
            else return false;
        }

    }

    public class IsBondStrong : AiBehaviour
    {
       
        private float BondNeeded; // the bond needed to determine if its strong enough

        public IsBondStrong(float a_BondNeeded) { BondNeeded = a_BondNeeded; m_BehaviourType = BehaviourType.ISBONDSTRONG; }

        public override bool execute(Agent a_agent)
        {
            //if the bond with the player is strong enough
            if (a_agent.fPlayerBond > BondNeeded)
            {
                m_BehaviourType = BehaviourType.ISBONDSTRONG;
                return true;

            }
            else return false;
        }
    }
    public class Avoid : AiBehaviour
    {
        private Vector3 WhatIsAvoided;
       
        public Avoid() { m_BehaviourType = BehaviourType.AVOID; }

        public void SetAvoid(Vector3 a_WhatIsAvoided) { WhatIsAvoided = a_WhatIsAvoided; }

        public override bool execute(Agent a_agent)
        {
            Vector3 heading = a_agent.getPosition() - WhatIsAvoided;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance; // This is now the normalized direction.
            m_BehaviourType = BehaviourType.AVOID;
            a_agent.setPosition(direction * distance * 1.2f);

            return true;
        }


    }

    

    
}
