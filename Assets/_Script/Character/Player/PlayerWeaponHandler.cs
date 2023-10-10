using System.Collections;
using JustGame.Script.Common;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Script.Character
{
    public class PlayerWeaponHandler : MonoBehaviour
    {
        [SerializeField] private AimComponent m_aimComponent;
        [SerializeField] private float m_weaponRange;
        [SerializeField] private BoxCollider2D m_weaponDamageArea;
        [SerializeField] private AnimationParameter m_atkAnim;
        private InputManager m_inputManager;
        private bool m_atkInProgress;
        private void Start()
        {
            m_inputManager = InputManager.Instance;
        }

        private void Update()
        {
            if (!m_inputManager.IsInputActive) return;

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
            if (m_atkInProgress)
            {
                yield break;
            }
            m_atkInProgress = true;
            var atkPos = (Vector2)transform.position + m_aimComponent.ClampAimDirection * m_weaponRange;
            m_weaponDamageArea.enabled = true;
            m_weaponDamageArea.transform.position = atkPos;
            m_atkAnim.SetTrigger();
            yield return new WaitForSeconds(m_atkAnim.Duration);
            m_weaponDamageArea.enabled = false;
            m_weaponDamageArea.transform.localPosition = Vector2.zero;
            m_atkInProgress = false;
        }
        
        public void WeaponStop()
        {
            m_atkInProgress = false;
            StopAllCoroutines();
        }
    } 
}

