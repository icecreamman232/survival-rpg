using UnityEngine;

namespace JustGame.Script.Character
{
    public class PlayerHunger : MonoBehaviour
    {
        [SerializeField] private int m_maxHunger;
        [SerializeField] private int m_curHunger;
        [SerializeField] private float m_damageFrequency;
        [SerializeField] private int m_damageOnHunger;
        [SerializeField] private PlayerHealth m_playerHealth;

        private float m_damageTimer;
        
        private void Start()
        {
            m_curHunger = m_maxHunger;
        }

        /// <summary>
        /// Recover hunger and increase value
        /// </summary>
        /// <param name="value"></param>
        public void ReduceHungry(int value)
        {
            m_curHunger += value;
        }
        
        /// <summary>
        /// Cause hungry and decrease value
        /// </summary>
        /// <param name="value"></param>
        public void CauseHungry(int value)
        {
            m_curHunger -= m_maxHunger;
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

