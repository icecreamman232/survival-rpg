using UnityEngine;
using UnityEngine.Tilemaps;

namespace JustGame.Script.World
{
    [CreateAssetMenu(menuName = "JustGame/World/Tile list")]
    public class TileList : ScriptableObject
    {
        [SerializeField] private TileBase[] m_tiles;

        public TileBase GetTileRandom()
        {
            return m_tiles[Random.Range(0, m_tiles.Length)];
        }

        public TileBase GetTile(int index)
        {
            return m_tiles[index];
        }
    }
}
