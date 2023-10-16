using JustGame.Script.Data;
using JustGame.Script.Managers;
using UnityEngine;

namespace JustGame.Script.Common
{
    public class CameraFollowing : MonoBehaviour
    {
        [SerializeField] private WorldRuntimeSet m_worldRuntimeSet;
        [SerializeField] private Transform m_cameraTransform;
        [SerializeField] private Transform m_targetTransform;
        [SerializeField] private float m_followingSpeed;
        [SerializeField] private Vector2 m_cameraSize;

        private Vector2 m_topLeft;
        private Vector2 m_botRight;
        
        private Vector3 m_targetPos;
        private bool m_canFollow;

        private Bounds m_cameraBounds;

        public Bounds CameraBounds => m_cameraBounds;

        public Vector2 TopLeft => m_topLeft;
        public Vector2 BotRight => m_botRight;

        private void Awake()
        {
            m_canFollow = true;
            m_cameraBounds = new Bounds((Vector2)m_cameraTransform.position, m_cameraSize);
            m_topLeft = new Vector2(transform.position.x - m_cameraSize.x / 2, transform.position.y + m_cameraSize.y / 2);
            m_botRight = new Vector2(transform.position.x + m_cameraSize.x / 2, transform.position.y - m_cameraSize.y / 2);
        }
    
        public void SetPermission(bool value)
        {
            m_canFollow = value;
        }
        public void SetTarget(Transform target)
        {
            m_targetTransform = target;
        }
    
        public void SetCameraPosition(Vector3 newPosition)
        {
            m_cameraTransform.position = newPosition;
        }
        
        private void Update()
        {
            if (!m_canFollow) return;
            if (m_targetTransform == null) return;

            m_targetPos = m_targetTransform.position;
            m_targetPos.z = -10;
            m_cameraTransform.position = Vector3.Lerp(m_cameraTransform.position, m_targetPos, Time.deltaTime * m_followingSpeed / 10);
            m_cameraTransform.position = ClampInWorldBounds();
            m_cameraBounds.center = (Vector2)m_cameraTransform.position;
            
            m_topLeft.x = transform.position.x - m_cameraSize.x / 2;
            m_topLeft.y = transform.position.y + m_cameraSize.y / 2;

            m_botRight.x = transform.position.x + m_cameraSize.x / 2;
            m_botRight.y = transform.position.y - m_cameraSize.y / 2;
        }

        private Vector3 ClampInWorldBounds()
        {
            return new Vector3(
                Mathf.Clamp(m_cameraTransform.position.x, m_worldRuntimeSet.WorldBounds.Bounds.min.x + m_cameraSize.x/2, m_worldRuntimeSet.WorldBounds.Bounds.max.x - m_cameraSize.x/2)
                , Mathf.Clamp(m_cameraTransform.position.y, m_worldRuntimeSet.WorldBounds.Bounds.min.y + m_cameraSize.y/2, m_worldRuntimeSet.WorldBounds.Bounds.max.y - m_cameraSize.y/2)
                ,-10);
        }
        
        public void ResetCamera()
        {
            m_cameraTransform.position = new Vector3(0, 0, -10);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position,m_cameraSize);
            Gizmos.color = Color.red;
            Gizmos.DrawCube(m_topLeft,Vector3.one * 0.5f);
            Gizmos.DrawCube(m_botRight,Vector3.one * 0.5f);
        }
    }
}
