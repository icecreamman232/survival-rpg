using JustGame.Script.Data;
using UnityEngine;

namespace JustGame.Script.Common
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Camera m_camera;
        [SerializeField] private WorldRuntimeSet m_worldRuntimeSet;

        private void Start()
        {
            m_worldRuntimeSet.SetCamera(m_camera);
        }
    }
}

