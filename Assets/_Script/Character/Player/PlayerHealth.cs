using JustGame.Script.Event;
using JustGame.Script.Managers;
using UnityEngine;

namespace JustGame.Script.Character
{
    public class PlayerHealth : Health
    {
        [SerializeField] private FloatEvent m_healthEvent;

        public override void TakeDamage(float damage, float invulnerableDuration, GameObject instigator)
        {
            if (!CheckDamageCondition()) return;
            
            
            m_curHealth -= damage;
            
            m_healthEvent.RaiseEvent(MathHelpers.Remap(m_curHealth,0,m_maxHealth,0,1));

            StartCoroutine(OnInvulnerable(invulnerableDuration));
            
            if (m_curHealth <= 0)
            {
                Kill();
            }
        }

        public void TakeSelfDamage(float damage)
        {
            m_curHealth -= damage;
            m_healthEvent.RaiseEvent(MathHelpers.Remap(m_curHealth,0,m_maxHealth,0,1));
            if (m_curHealth <= 0)
            {
                Kill();
            }
        }
    }
}
