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
            m_cameraFollowing.SetCameraPosition(worldPos);
            m_cameraFollowing.SetTarget(player.transform);
        }
    }
}

