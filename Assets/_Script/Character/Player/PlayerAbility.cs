
namespace JustGame.Script.Character
{
    /// <summary>
    /// Base class for player ability
    /// </summary>
    public class PlayerAbility : CharacterAbility
    {
        public virtual void HandleInput()
        {
            
        }

        public override void Update()
        {
            if (!m_isPermit) return;
            HandleInput();
            
            base.Update();
        }
    }
}
