using UnityEngine;

namespace JustGame.Script.Data
{
    [CreateAssetMenu(menuName = "JustGame/Runtime/World set")]
    public class WorldRuntimeSet : ScriptableObject
    {
        [SerializeField] private Camera m_mainCamera;
        
        #region Properties
        public Camera MainCamera => m_mainCamera;
        #endregion

        #region Setter
        public void SetCamera(Camera cam)
        {
            m_mainCamera = cam;
        }
        #endregion
        

        private void OnDestroy()
        {
            m_mainCamera = null;
        }
    }
}
