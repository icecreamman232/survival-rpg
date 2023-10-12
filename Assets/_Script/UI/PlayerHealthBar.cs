using JustGame.Script.Event;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Script.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private Image m_healthBar;
        [SerializeField] private FloatEvent m_healthEvent;

        private void Start()
        {
            m_healthEvent.AddListener(UpdateHealthBar);
            m_healthBar.fillAmount = 1;
        }


        private void UpdateHealthBar(float value)
        {
            m_healthBar.fillAmount = value;
        }

        private void OnDestroy()
        {
            m_healthEvent.RemoveListener(UpdateHealthBar);
        }
    }
}
