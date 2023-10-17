using JustGame.Script.Managers;
using UnityEngine;
using Tree = JustGame.Script.World.Tree;

namespace JustGame.Script.Character
{
    public class ToolInteractHandler : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerManager.WorldObject
                && other.CompareTag("Tree"))
            {
                HandleTree(other);
            }
        }

        private void HandleTree(Collider2D other)
        {
            var tree = other.gameObject.GetComponentInParent<Tree>();
            if (tree != null)
            {
                tree.TakeHit();
            }
        }
    }
}
