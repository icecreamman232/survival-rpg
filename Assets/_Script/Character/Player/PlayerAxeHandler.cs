using System.Collections;
using JustGame.Script.Common;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Script.Character
{
    public class PlayerAxeHandler : MonoBehaviour
    {
        [SerializeField] private bool m_isPermit;
        [SerializeField] private float m_axeRange;
        [SerializeField] private float m_energyConsume;
        [SerializeField] private PlayerEnergy m_playerEnergy;
        [SerializeField] private AimComponent m_aimComponent;
        [SerializeField] private BoxCollider2D m_axeDamageArea;
        [SerializeField] private AnimationParameter m_useAxeAnim;
        private InputManager m_inputManager;
        private bool m_AxeInProgress;

        public void SetPermission(bool value)
        {
            m_isPermit = value;
        }
        
        private void Start()
        {
            m_inputManager = InputManager.Instance;
        }
        
        private void Update()
        {
            if (!m_inputManager.IsInputActive) return;

            if (!m_isPermit) return;
            
            if (m_inputManager.GetLeftClick())
            {
                WeaponStart();
            }
        }
        
        public void WeaponStart()
        {
            StartCoroutine(AttackRoutine());
        }
        
        private IEnumerator AttackRoutine()
        {
            if (m_AxeInProgress)
            {
                yield break;
            }
            
            m_playerEnergy.SpentEnergy(m_energyConsume);
                
            m_AxeInProgress = true;
            var atkPos = (Vector2)transform.position + m_aimComponent.ClampAimDirection * m_axeRange;
            m_axeDamageArea.enabled = true;
            m_axeDamageArea.transform.position = atkPos;
            m_useAxeAnim.SetTrigger();
            yield return new WaitForSeconds(m_useAxeAnim.Duration);
            m_axeDamageArea.enabled = false;
            m_axeDamageArea.transform.localPosition = Vector2.zero;
            m_AxeInProgress = false;
        }
        
        public void WeaponStop()
        {
            m_AxeInProgress = false;
            StopAllCoroutines();
        }
    }
}
