using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Script.Common
{
    /// <summary>
    /// Compute direction which looking at cursor
    /// </summary>
    public class AimComponent : MonoBehaviour
    {
        [SerializeField] private Vector2 m_aimDirection;
        [SerializeField] private float m_angle;
        [SerializeField] private float m_offsetAngle;
        
        public Vector2 AimDirection => m_aimDirection;
        public float Angle => m_angle;
        
        private InputManager m_inputManager;

        private void Start()
        {
            m_aimDirection = Vector2.zero;
            m_inputManager = InputManager.Instance;
        }

        private void Update()
        {
            m_aimDirection = (m_inputManager.GetWorldMousePos() - transform.position).normalized;
            m_angle = Mathf.Atan2(m_aimDirection.y, m_aimDirection.x) * Mathf.Rad2Deg + m_offsetAngle;
        }
    }
}

