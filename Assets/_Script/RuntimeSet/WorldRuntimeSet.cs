using JustGame.Script.World;
using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/Runtime/World set")]
    public class WorldRuntimeSet : ScriptableObject
    {
        [SerializeField] private Camera m_mainCamera;
        [SerializeField] private WorldBounds m_worldBounds;
        
        #region Properties
        public Camera MainCamera => m_mainCamera;
        public WorldBounds WorldBounds => m_worldBounds;
        
        #endregion

        #region Setter
        public void SetCamera(Camera cam)
        {
            m_mainCamera = cam;
        }

        public void SetWorldBounds(WorldBounds bounds)
        {
            m_worldBounds = bounds;
        }
        #endregion
        

        private void OnDestroy()
        {
            m_mainCamera = null;
            m_worldBounds = null;
        }
    }
}
