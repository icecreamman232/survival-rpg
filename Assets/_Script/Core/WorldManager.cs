using System;
using System.Collections;
using JustGame.Script.Common;
using JustGame.Script.World;
using UnityEngine;

namespace JustGame.Script.Managers
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private CameraFollowing m_cameraFollowing;
        [SerializeField] private GameObject m_playerPrefab;
        [SerializeField] private WorldGenerator m_worldGenerator;
        [SerializeField] private WorldArea[] m_areas;
        
        private Transform m_playerRef;
        private bool m_checked;
        private int m_lastAreaIndex;

        private void Start()
        {
            CreateWorld();
        }


        [ContextMenu("Create world")]
        private void CreateWorld()
        {
            StartCoroutine(CreateWorldRoutine());
        }

        private IEnumerator CreateWorldRoutine()
        {
            m_worldGenerator.GenerateWorld();
            yield return new WaitUntil(() => m_worldGenerator.IsGenerateDone);
            var worldPos = m_worldGenerator.GetSpawnPointInWorldCoord();
            worldPos.x -= 0.25f;
            worldPos.y -= 0.25f;
            var player = Instantiate(m_playerPrefab,worldPos , Quaternion.identity);
            m_playerRef = player.transform;
            m_cameraFollowing.SetCameraPosition(worldPos);
            m_cameraFollowing.SetTarget(player.transform);

            for (int i = 0; i < m_areas.Length; i++)
            {
                m_areas[i].gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (m_playerRef == null) return;
            CheckCurrentArea();
        }

        private void CheckCurrentArea()
        {
            m_checked = false;
            for (int i = 0; i < m_areas.Length; i++)
            {
                if (m_areas[i].IsInArea(m_playerRef.position) && !m_checked)
                {
                    if (m_lastAreaIndex != i)
                    {
                        m_areas[m_lastAreaIndex].gameObject.SetActive(false);
                    }
                    m_areas[i].gameObject.SetActive(true);
                    m_lastAreaIndex = i;
                    m_checked = true;
                }
            }
        }
    }
}

