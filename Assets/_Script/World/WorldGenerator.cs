using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random =  UnityEngine.Random;

namespace JustGame.Script.World
{
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField] private int m_mapWidth;
        [SerializeField] private int m_mapHeight;
        [SerializeField] private Tilemap m_tilemap;
        [SerializeField] private TileBase m_groundTile;

        [Header("Perlin Noise")] 
        [SerializeField] private float m_scale;
        [SerializeField] private Vector2 m_offset;
        
        private int[] m_worldArr;

        private void Start()
        {
            m_worldArr = new int[m_mapWidth * m_mapHeight];
        }

        [ContextMenu("Generate world")]
        private void GenerateWorld()
        {
            GenerateData();
            RenderWorld();
        }

        private void GenerateData()
        {
            for (int y = 0; y < m_mapHeight; y++)
            {
                for (int x = 0; x < m_mapWidth; x++)
                {
                    m_worldArr[x + y * m_mapWidth] = GetNoiseValue(x,y); //[0,1]
                }
            }
        }

        private void RenderWorld()
        {
            m_tilemap.ClearAllTiles();
            
            for (int y = 0; y < m_mapHeight; y++)
            {
                for (int x = 0; x < m_mapWidth; x++)
                {
                    if (m_worldArr[x + y * m_mapWidth] == 1)
                    {
                        m_tilemap.SetTile(new Vector3Int(x,y),m_groundTile);
                    }
                }
            }
        }

        private int GetNoiseValue(int x, int y)
        {
            float xCoord = x * m_scale + m_offset.x;
            float yCoord = y * m_scale + m_offset.y;

            var sample = Mathf.PerlinNoise(xCoord, yCoord);
            Debug.Log($"Sample {sample}");
            return sample >= 0.5f ? 1 : 0;
        }
    }
}
