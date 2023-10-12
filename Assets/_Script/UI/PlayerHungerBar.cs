using JustGame.Script.Event;
using UnityEngine;
using UnityEngine.UI;

namespace JustGame.Script.UI
{
    public class PlayerHungerBar : MonoBehaviour
    {
        [SerializeField] private Image m_hungerBar;
        [SerializeField] private FloatEvent m_hungerEvent;

        private void Start()
        {
            m_hungerEvent.AddListener(UpdateHealthBar);
            m_hungerBar.fillAmount = 1;
        }


        private void UpdateHealthBar(float value)
        {
            m_hungerBar.fillAmount = value;
        }

        private void OnDestroy()
        {
            m_hungerEvent.RemoveListener(UpdateHealthBar);
        }
    }
}

