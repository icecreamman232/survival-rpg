using JustGame.Script.Common;
using UnityEngine;

namespace JustGame.Script.Character
{
    public class PlayerLookArrow : MonoBehaviour
    {
        [SerializeField] private AimComponent m_aimComponent;

        private void Update()
        {
            transform.rotation = Quaternion.AngleAxis(m_aimComponent.ClampAngle - 90f,Vector3.forward);
        }
    }
}

