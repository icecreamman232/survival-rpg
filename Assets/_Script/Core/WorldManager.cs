using System.Collections;
using JustGame.Script.World;
using UnityEngine;

namespace JustGame.Script.Managers
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_playerPrefab;
        [SerializeField] private WorldGenerator m_worldGenerator;

        private void CreateWorld()
        {
            StartCoroutine(CreateWorldRoutine());
        }

        private IEnumerator CreateWorldRoutine()
        {
            m_worldGenerator.GenerateWorld();
            yield return new WaitUntil(() => m_worldGenerator.IsGenerateDone);
            
        }
    }
}

