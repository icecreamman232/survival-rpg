using System;
using JustGame.Script.Data;
using UnityEngine;

namespace JustGame.Script.World
{
    public class WorldBounds : MonoBehaviour
    {
        [SerializeField] private WorldRuntimeSet m_worldRuntimeSet;
        [SerializeField] private WorldGenerator m_worldGenerator;
        
        private Vector2 m_worldSize;
        private Bounds m_worldBounds;

        public Bounds Bounds => m_worldBounds;
        
        private void Start()
        {
            m_worldRuntimeSet.SetWorldBounds(this);
            m_worldSize = new Vector2(m_worldGenerator.MapWidth/2, m_worldGenerator.MapHeight/2);
            m_worldBounds = new Bounds(transform.position, m_worldSize);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, m_worldSize);
            Gizmos.color = Color.red;
            var currentPos = transform.position;
            //Bot left
            Gizmos.DrawCube(new Vector2(
                    currentPos.x-m_worldBounds.extents.x, currentPos.y - m_worldBounds.extents.y), 
                Vector2.one);
            //Bot right
            Gizmos.DrawCube(new Vector2(
                    currentPos.x + m_worldBounds.extents.x, currentPos.y - m_worldBounds.extents.y), 
                Vector2.one);
            
            //Top left
            Gizmos.DrawCube(new Vector2(
                    currentPos.x-m_worldBounds.extents.x, currentPos.y + m_worldBounds.extents.y), 
                Vector2.one);
            //Top right
            Gizmos.DrawCube(new Vector2(
                    currentPos.x + m_worldBounds.extents.x, currentPos.y + m_worldBounds.extents.y), 
                Vector2.one);
        }
    }
}
