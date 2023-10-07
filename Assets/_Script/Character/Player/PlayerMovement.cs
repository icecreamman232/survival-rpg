using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Script.Character
{
    /// <summary>
    /// Movement ability of player
    /// </summary>
    public class PlayerMovement : PlayerAbility
    {
        [SerializeField] private float m_moveSpeed;
        [SerializeField] private Vector2 m_movingDirection;
        
        private InputManager m_inputManager;
        protected override void Start()
        {
            m_inputManager = InputManager.Instance;
            base.Start();
        }

        protected override void HandleInput()
        {
            if (!m_inputManager.IsInputActive)
            {
                m_movingDirection = Vector2.zero;
                return;
            }

            if (m_inputManager.GetKeyDown(BindingAction.MOVE_LEFT))
            {
                m_movingDirection.x = -1;
            }
            else if (m_inputManager.GetKeyDown(BindingAction.MOVE_RIGHT))
            {
                m_movingDirection.x = 1;
            }
            else
            {
                m_movingDirection.x = 0;
            }
            
            if (!m_inputManager.IsInputActive) return;

            if (m_inputManager.GetKeyDown(BindingAction.MOVE_UP))
            {
                m_movingDirection.y = 1;
            }
            else if (m_inputManager.GetKeyDown(BindingAction.MOVE_DOWN))
            {
                m_movingDirection.y = -1;
            }
            else
            {
                m_movingDirection.y = 0;
            }
            base.HandleInput();
        }

        protected override void Update()
        {
            base.Update();
            Movement();
        }

        private void Movement()
        {
            transform.Translate(m_movingDirection * ((m_moveSpeed/10) * Time.deltaTime));
        }
    }
}

