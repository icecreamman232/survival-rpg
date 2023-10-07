
namespace JustGame.Script.Character
{
    /// <summary>
    /// Base class for player ability
    /// </summary>
    public class PlayerAbility : CharacterAbility
    {
        protected virtual void HandleInput()
        {
            
        }

        protected override void Update()
        {
            if (!m_isPermit) return;
            HandleInput();
            
            base.Update();
        }
    }
}
