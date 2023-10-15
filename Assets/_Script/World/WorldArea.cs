using System;
using UnityEngine;

namespace JustGame.Script.World
{
    public class WorldArea : MonoBehaviour
    {
        [SerializeField] private Vector2 m_center;
        [SerializeField] private Vector2 m_topLeft;
        [SerializeField] private Vector2 m_botRight;
        [SerializeField] private bool m_showCorner;

        private void Awake()
        {
            m_topLeft = new Vector2(m_center.x - 25f/2, m_center.y + 25f/2);
            m_botRight = new Vector2(m_center.x + 25f/2, m_center.y - 25f/2);
        }

        public bool IsInArea(Vector2 position)
        {
            if ((position.x <= m_botRight.x && position.x >= m_topLeft.x) 
                && (position.y <= m_topLeft.y && position.y >= m_botRight.y))
            {
                return true;
            }
            return false;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(m_center,Vector3.one * 25f);
            if (m_showCorner)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(m_topLeft, Vector3.one * 0.5f);
                Gizmos.DrawCube(m_botRight, Vector3.one * 0.5f);
            }
        }
    }
}

