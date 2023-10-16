using JustGame.Script.Character;
using JustGame.Script.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JustGame.Script.UI
{
    public class ToolbarSlotUI : Selectable
    {
        [SerializeField] private PlayerTool m_toolID;
        [SerializeField] private Image m_selectLayer;
        [SerializeField] private PlayerToolEvent m_playerToolEvent;
        
        public override void OnSelect(BaseEventData eventData)
        {
            m_selectLayer.gameObject.SetActive(true);
            m_playerToolEvent.RaiseEvent(m_toolID);
            base.OnSelect(eventData);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            m_selectLayer.gameObject.SetActive(false);
            base.OnDeselect(eventData);
        }
    }
}
