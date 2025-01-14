﻿using UnityEngine;
using System.Collections;
using AiBehaviour_ns;

/// <summary>
/// Author: Jacob Connelly
/// Date Created: 13/8/14
/// Last Updated: 13/8/14
/// Description: This class intends to be 
/// a script that can be attached or used by the child
/// in conjuction with the AiBehaviou class as a 
/// behaviour tree.
/// </summary>
namespace Agent_ns
{
    public class Agent 
    {

        // just the stored behavior tree that this agent will use
        private AiBehaviour m_behaviour;
        private Vector3 m_position;
        private Vector3 m_target;

        public float fMovementSpeed;
        
        public float fRotationSpeed;

        public float fBoredem; // the decreasing value associated to the child boredem
        public float fBoredomRate;
        public float fBoredomMax;
        public float fVisionRange;

        public float fPlayerBond;
        public float fPlayerBondMax;
        public float fPlayerBondMinLine;        // this is not a minimum but once the bond is below this line it lowers a lot slower
        public float fPlayerBondMinLineFalloff; // this is the multiplier by how much it lowers below the line    
        public float fPlayerBondIncreaseRate;
        public float fPlayerBondDecreaseRate;

       public Vector3   getPosition()  { return m_position;}
       public Vector3   getTarget() { return m_target; }

       
       public void      setPosition(Vector3 a_position) { m_position = a_position; }
       public void      setTarget(Vector3 a_target) { m_target = a_target; }
       

        public void     setBehaviour(AiBehaviour a_behaviour) { m_behaviour = a_behaviour; }


        public void update()
        {
            if (m_behaviour != null)
            {
               
                m_behaviour.execute(this);
                //Debug.Log(m_behaviour.m_BehaviourType.ToString());
                
            }
        }
        public AiBehaviour GetBehaviour()
        {
            return m_behaviour;
        }

    }
}