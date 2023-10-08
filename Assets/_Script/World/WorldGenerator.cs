using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace JustGame.Script.World
{
    public class WorldGenerator : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] private int m_mapWidth;
        [SerializeField] private int m_mapHeight;
        [SerializeField] private Tilemap m_groundTilemap;
        [SerializeField] private Tilemap m_waterTilemap;
        [Header("Sample limits")]
        [SerializeField] private float m_groundSample;
        [SerializeField] private float m_waterSample;
        [Header("Tile list")]
        [SerializeField] private TileList m_groundTiles;
        [SerializeField] private TileBase m_waterTile;

        [Header("Perlin Noise")] 
        [SerializeField] private float m_scale;
        [SerializeField] private Vector2 m_offset;
        
        private float[] m_worldArr;

        private void Start()
        {
            m_worldArr = new float[m_mapWidth * m_mapHeight];
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
            m_groundTilemap.ClearAllTiles();
            
            for (int y = 0; y < m_mapHeight; y++)
            {
                for (int x = 0; x < m_mapWidth; x++)
                {
                    SetProperTile(x, y, m_worldArr[x + y * m_mapWidth]);
                    
                    // m_groundTilemap.SetTile(
                    //     new Vector3Int(x, y), 
                    //     GetProperTile(m_worldArr[x + y * m_mapWidth]));
                }
            }
        }

        private void SetProperTile(int x, int y, float sample)
        {
            if (sample <= m_waterSample)
            {
                m_waterTilemap.SetTile(new Vector3Int(x,y), m_waterTile) ;
            }
            
            if (sample >= m_groundSample)
            {
                m_groundTilemap.SetTile(new Vector3Int(x,y), m_groundTiles.GetTileRandom());
            }
        }
        
        private TileBase GetProperTile(float sample)
        {
            if (sample <= m_waterSample)
            {
                return m_waterTile;
            }

            if (sample >= m_groundSample)
            {
                return m_groundTiles.GetTileRandom();
            }
            return null;
        }
        
        private float GetNoiseValue(int x, int y)
        {
            float xCoord = x * m_scale + m_offset.x;
            float yCoord = y * m_scale + m_offset.y;

            var sample = Mathf.PerlinNoise(xCoord, yCoord);
            //Debug.Log($"Sample {sample}");
            return sample;
        }
    }
}
