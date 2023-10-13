using JustGame.Script.Event;
using JustGame.Script.Managers;
using UnityEngine;

namespace JustGame.Script.Character
{
    public class PlayerEnergy : MonoBehaviour
    {
        [SerializeField] private float m_maxEnergy;
        [SerializeField] private float m_curEnergy;
        [SerializeField] private FloatEvent m_energyEvent;

        private void Start()
        {
            m_curEnergy = m_maxEnergy;
        }

        public void SpentEnergy(float spending, GameObject instigator = null)
        {
            m_curEnergy -= spending;

            m_energyEvent.RaiseEvent(MathHelpers.Remap(m_curEnergy,0, m_maxEnergy,0,1));
            
            if (m_curEnergy <= 0)
            {
                //TODO:Feeling sleepy and slow down
            }
        }
    }
}
