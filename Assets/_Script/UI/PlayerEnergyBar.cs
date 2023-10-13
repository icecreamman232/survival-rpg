using JustGame.Script.Event;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Script.UI
{
    public class PlayerEnergyBar : MonoBehaviour
    {
        [SerializeField] private Image m_hungerBar;
        [SerializeField] private FloatEvent m_energyEvent;

        private void Start()
        {
            m_energyEvent.AddListener(UpdateHealthBar);
            m_hungerBar.fillAmount = 1;
        }


        private void UpdateHealthBar(float value)
        {
            m_hungerBar.fillAmount = value;
        }

        private void OnDestroy()
        {
            m_energyEvent.RemoveListener(UpdateHealthBar);
        }
    }
}
