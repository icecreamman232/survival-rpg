using System.Collections;
using UnityEngine;

namespace JustGame.Script.Character
{
    /// <summary>
    /// Base class for health
    /// </summary>
    public class Health : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] protected float m_maxHealth;
        [SerializeField] protected float m_curHealth;
        [SerializeField] protected Color m_flickerColor;
        [SerializeField] protected float m_delayBetweenFlicks;
        
        protected SpriteRenderer m_spriteRenderer;
        protected bool m_isInvulnerable;

        public bool IsInvulnerable => m_isInvulnerable;
        
        protected virtual void Start()
        {
            m_curHealth = m_maxHealth;
            m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public virtual void TakeDamage(float damage, float invulnerableDuration, GameObject instigator)
        {
            if (!CheckDamageCondition()) return;
            
            m_curHealth -= damage;
            StartCoroutine(OnInvulnerable(invulnerableDuration));
            
            if (m_curHealth <= 0)
            {
                Kill();
            }
        }

        protected virtual bool CheckDamageCondition()
        {
            if (m_isInvulnerable)
            {
                return false;
            }

            return true;
        }
        

        protected IEnumerator OnInvulnerable(float duration)
        {
            var flickStop = Time.time + duration;
            var initColor = m_spriteRenderer.material.color;
            m_isInvulnerable = true;
            while (Time.time < flickStop)
            {
                m_spriteRenderer.material.color = m_flickerColor;
                yield return new WaitForSeconds(m_delayBetweenFlicks);
                m_spriteRenderer.material.color = initColor;
                yield return new WaitForSeconds(m_delayBetweenFlicks);
            }
            
            m_spriteRenderer.material.color = initColor;
            m_isInvulnerable = false;
        }

        protected virtual void Kill()
        {
            this.gameObject.SetActive(false);
        }
    }
}

