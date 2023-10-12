using JustGame.Script.Event;
using JustGame.Script.Managers;
using UnityEngine;

namespace JustGame.Script.Character
{
    public class PlayerHunger : MonoBehaviour
    {
        [SerializeField] private float m_maxHunger;
        [SerializeField] private float m_curHunger;
        [SerializeField] private float m_damageFrequency;
        [SerializeField] private int m_damageOnHunger;
        [SerializeField] private PlayerHealth m_playerHealth;
        [SerializeField] private FloatEvent m_floatEvent;

        private float m_damageTimer;
        
        private void Start()
        {
            m_curHunger = m_maxHunger;
        }
        
        [ContextMenu("Decrease")]
        private void Test()
        {
            CauseHungry(10);
        }
        
        /// <summary>
        /// Recover hunger and increase value
        /// </summary>
        /// <param name="value"></param>
        public void ReduceHungry(int value)
        {
            m_curHunger += value;
            m_floatEvent.RaiseEvent(MathHelpers.Remap(m_curHunger,0,m_maxHunger,0,1));
        }
        
        /// <summary>
        /// Cause hungry and decrease value
        /// </summary>
        /// <param name="value"></param>
        public void CauseHungry(int value)
        {
            Debug.Log("Cause");
            m_curHunger -= value;
            m_floatEvent.RaiseEvent(MathHelpers.Remap(m_curHunger,0,m_maxHunger,0,1));
        }
        
        private void Update()
        {
            if (m_curHunger > 0) return;
            
            if (m_damageTimer <= 0)
            {
                m_damageTimer = m_damageFrequency;
                m_playerHealth.TakeSelfDamage(m_damageOnHunger);
            }

            m_damageTimer -= Time.deltaTime;
        }
    }
}

