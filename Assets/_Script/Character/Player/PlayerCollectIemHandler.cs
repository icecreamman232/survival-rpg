using JustGame.Script.Common;
using JustGame.Script.World;
using JustGame.Scripts.Managers;
using UnityEngine;

namespace JustGame.Script.Character
{
    public class PlayerCollectIemHandler : PlayerAbility
    {
        [SerializeField] private LayerMask m_pickableLayerMask;
        [SerializeField] private AimComponent m_aimComponent;
        private InputManager m_inputManager;

        protected override void Start()
        {
            m_inputManager = InputManager.Instance;
            base.Start();
        }

        protected override void HandleInput()
        {
            if (!m_inputManager.IsInputActive) return;

            if (m_inputManager.GetKeyClicked(BindingAction.COLLECT_ITEM))
            {
                TryToCollectItem();
            }
            base.HandleInput();
        }

        private void TryToCollectItem()
        {
            var lookDirection = m_aimComponent.ClampAimDirection;
            var rayCastResult = Physics2D.Raycast(transform.position, lookDirection, 0.25f,m_pickableLayerMask);
            if (rayCastResult)
            {
                HandlePickup(rayCastResult.transform);
            }
        }

        private void HandlePickup(Transform itemTransform)
        {
            var item = itemTransform.GetComponentInParent<PickableItem>();
            if (item != null)
            {
                item.Pick();
            }
        }
    }
}

