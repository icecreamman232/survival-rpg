using JustGame.Script.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace JustGame.Script.Character
{
    /// <summary>
    /// Base class for damage dealing component.
    /// This component will deal damage to target which has health component attached
    /// </summary>
    public class DamageHandler : MonoBehaviour
    {
        [SerializeField] protected float m_minDamage;
        [SerializeField] protected float m_maxDamage;
        [SerializeField] protected float m_invulnerableDurationCause;
        [SerializeField] protected LayerMask m_targetMask;

        protected virtual float GetDamage()
        {
            return Random.Range(m_minDamage, m_maxDamage);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!LayerManager.IsInLayerMask(other.gameObject.layer, m_targetMask)) return;

            CauseDamage(other);
        }

        protected virtual void CauseDamage(Collider2D other)
        {
            var damage = GetDamage();
            var health = other.gameObject.GetComponentInParent<Health>();
            
            health.TakeDamage(Mathf.Round(damage),m_invulnerableDurationCause,this.gameObject);
        }
    }
}
