using UnityEngine;

namespace JustGame.Script.Character
{
    public enum PlayerTool
    {
        SWORD = 0,
        
        AXE = 100,
    }
    
    /// <summary>
    /// This class will control which tool is in use.
    /// Most of time will be enable/disable tool component scripts
    /// </summary>
    public class PlayerToolController : MonoBehaviour
    {
        [SerializeField] private PlayerTool m_curTool;
        [SerializeField] private PlayerAxeHandler m_axeHandler;
        [SerializeField] private PlayerWeaponHandler m_weaponHandler;

        private void Start()
        {
            SetTool(PlayerTool.SWORD);
        }

        public void SetTool(PlayerTool tool)
        {
            switch (tool)
            {
                case PlayerTool.SWORD:
                    m_weaponHandler.SetPermission(true);
                    m_axeHandler.SetPermission(false);
                    break;
                case PlayerTool.AXE:
                    m_weaponHandler.SetPermission(false);
                    m_axeHandler.SetPermission(true);
                    break;
            }

            m_curTool = tool;
        }
    }
}

