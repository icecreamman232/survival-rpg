using System;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Script.Character
{
    public class PlayerWeaponHandler : MonoBehaviour
    {
        private InputManager m_inputManager;

        private void Start()
        {
            m_inputManager = InputManager.Instance;
        }

        private void Update()
        {
            if (!m_inputManager.IsInputActive) return;

            if (m_inputManager.GetLeftClick())
            {
                WeaponStart();
            }
        }

        public void WeaponStart()
        {
            
        }

        public void WeaponStop()
        {
            
        }
    } 
}

