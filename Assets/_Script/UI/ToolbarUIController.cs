using JustGame.Script.Character;
using JustGame.Script.Event;
using UnityEngine;

namespace JustGame.Script.UI
{
    public class ToolbarUIController : MonoBehaviour
    {
        [SerializeField] private PlayerToolEvent m_playerToolEvent;
        [SerializeField] private GameObject[] m_slotSelectLayers;
        private int m_prevToolSlotIndex;
        private void Start()
        {
            m_playerToolEvent.AddListener(OnChangePlayerTool);
            m_prevToolSlotIndex = 0;
            m_slotSelectLayers[0].SetActive(true);
        }

        private void OnChangePlayerTool(PlayerTool newTool)
        {
            switch (newTool)
            {
                case PlayerTool.SWORD:
                    m_slotSelectLayers[m_prevToolSlotIndex].SetActive(false);
                    m_slotSelectLayers[0].SetActive(true);
                    m_prevToolSlotIndex = 0;
                    break;
                case PlayerTool.AXE:
                    m_slotSelectLayers[m_prevToolSlotIndex].SetActive(false);
                    m_slotSelectLayers[1].SetActive(true);
                    m_prevToolSlotIndex = 1;
                    break;
            }
        }

        private void OnDestroy()
        {
            m_playerToolEvent.RemoveListener(OnChangePlayerTool);
        }
    }
}

