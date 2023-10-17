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
        [SerializeField] private float m_unitPerStep;
        [SerializeField] private Vector2 m_movingDirection;
        [SerializeField] private LayerMask m_obstacleMask;
        
        private bool m_isMoving;
        private Vector2 m_nextPos;
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

            if (m_isMoving) return;

            if(m_inputManager.GetKeyClicked(BindingAction.MOVE_LEFT))
            {
                m_movingDirection.x = -1;
            }
            if(m_inputManager.GetKeyClicked(BindingAction.MOVE_RIGHT))
            {
                m_movingDirection.x = 1;
            }
            if(m_inputManager.GetKeyClicked(BindingAction.MOVE_UP))
            {
                m_movingDirection.y = 1;
            }
            if(m_inputManager.GetKeyClicked(BindingAction.MOVE_DOWN))
            {
                m_movingDirection.y = -1;
            }

            if (m_movingDirection != Vector2.zero)
            {
                m_isMoving = true;
                m_nextPos = (Vector2)transform.position + m_movingDirection * m_unitPerStep;
            }
            
            base.HandleInput();
        }

        protected override void Update()
        {
            base.Update();
            if (CheckObstacle())
            {
                m_isMoving = false;
                m_movingDirection = Vector2.zero;
                return;
            }
            
            Movement();
        }

        private bool CheckObstacle()
        {
            var result = Physics2D.Raycast(transform.position, m_movingDirection, 0.25f, m_obstacleMask);
            if (result)
            {
                return true;
            }

            return false;
        }
        
        private void Movement()
        {
            if (!m_isMoving) return;
            transform.position = Vector2.MoveTowards(transform.position, m_nextPos, Time.deltaTime * m_moveSpeed);
            if ((Vector2)transform.position == m_nextPos)
            {
                transform.position = m_nextPos;
                m_isMoving = false;
                m_movingDirection = Vector2.zero;
            }
        }
    }
}

