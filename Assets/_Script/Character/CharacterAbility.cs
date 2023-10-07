using UnityEngine;

namespace JustGame.Script.Character
{
    /// <summary>
    /// Base class for character ability whether it's player or enemy
    /// </summary>
    public class CharacterAbility : MonoBehaviour
    {
        [SerializeField] protected bool m_isPermit;

        public bool IsPermit => m_isPermit;
        
        public void SetPermission(bool value)
        {
            m_isPermit = value;
        }
        
        protected virtual void Start()
        {
            
        }
        
        protected virtual void Update()
        {
            
        }
    }
}

