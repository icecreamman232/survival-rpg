using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace JustGame.Script.World
{
    public class WorldGenerator : MonoBehaviour
    {
        [Header("Base")]
        [SerializeField] private int m_mapWidth;
        [SerializeField] private int m_mapHeight;
        [SerializeField] private bool m_isGenerateDone;
        [SerializeField] private Tilemap m_groundTilemap;
        [SerializeField] private Tilemap m_waterTilemap;
        [Header("Sample limits")]
        [SerializeField] private float m_maxWaterValue;
        [SerializeField] private float m_maxGroundValue;
        [SerializeField] private float m_maxGrassValue;
        [SerializeField] private float m_maxTreeValue;
        [SerializeField] private float m_percentToSpawnGrass;
        [Header("Tile list")]
        [SerializeField] private TileList m_groundTiles;
        [SerializeField] private TileList m_grassTiles;
        [SerializeField] private TileBase m_waterTile;
        [SerializeField] private GameObject m_treePrefab;
        [Header("Perlin Noise")] 
        [SerializeField] private float m_scale;
        [SerializeField] private Vector2 m_offsetRange;
        [Header("Area")] 
        [SerializeField] private WorldArea[] m_areas;

        public int MapWidth => m_mapWidth;
        public int MapHeight => m_mapHeight;
        public bool IsGenerateDone => m_isGenerateDone;
        
        private int[] m_worldArr;
        
        private void Start()
        {
            m_worldArr = new int[m_mapWidth * m_mapHeight];
        }

        [ContextMenu("Generate world")]
        public void GenerateWorld()
        {
            StartCoroutine(GenerateWorldRoutine());
        }

        private void GenerateData()
        {
            //Generate ground + water level
            var offsetX = Random.Range(m_offsetRange.x, m_offsetRange.y);
            var offsetY = Random.Range(m_offsetRange.x, m_offsetRange.y);
            for (int y = 0; y < m_mapHeight; y++)
            {
                for (int x = 0; x < m_mapWidth; x++)
                {
                    float noiseValue = GetNoiseValue(x,y, offsetX, offsetY);
                    m_worldArr[x + y * m_mapWidth] = ConvertNoiseToWorld(noiseValue);
                }
            }

            for (int y = 0; y < m_mapHeight; y++)
            {
                for (int x = 0; x < m_mapWidth; x++)
                {
                    if (m_worldArr[x + y * m_mapWidth] == 3)
                    {
                        var worldPos = new Vector2((x - m_mapWidth/2f) * 0.5f, (y - m_mapHeight/2f)* 0.5f);
                        worldPos.x += 0.25f;
                        worldPos.y += 0.25f;
                        //Create tree
                        var tree = Instantiate(m_treePrefab, worldPos, Quaternion.identity);
                        var area = GetAreaContainThis(worldPos.x, worldPos.y);
                        if (area != null)
                        {
                            tree.transform.parent = area.transform;
                        }
                    }
                }
            }
            
            // var startX = 0;
            // var endX = 50;
            // var startY = 0;
            // var endY = 50;
            //
            // for (int i = 0; i < 3; i++)
            // {
            //     for (int j = 0; j < 3; j++)
            //     {
            //         for (int y = startY; y < endY; y++)
            //         {
            //             for (int x = startX; x < endX; x++)
            //             {
            //                 float noiseValue = GetNoiseValue(x,y, offsetX, offsetY);
            //                 m_worldArr[x + y * m_mapWidth] = ConvertNoiseToWorld(noiseValue);
            //                 m_areas[i * 3 + j].SaveAreaData(m_worldArr[x + y * m_mapWidth]);
            //             }
            //         }
            //         startX += 50;
            //         endX += 50;
            //     }
            //
            //     startX = 0;
            //     endX = 50;
            //     startY += 50;
            //     endY += 50;
            // }
            
            
        }

        private WorldArea GetAreaContainThis(float x, float y)
        {
            for (int i = 0; i < m_areas.Length; i++)
            {
                if (m_areas[i].IsInArea(new Vector2(x, y)))
                {
                    return m_areas[i];
                }
            }
            return null;
        }
        
        private int ConvertNoiseToWorld(float noise)
        {
            if (noise <= m_maxWaterValue)
            {
                return 0;
            }
            
            if (noise > m_maxWaterValue && noise <= m_maxGroundValue)
            {
                return 1;
            }

            if (noise > m_maxGroundValue && noise <= m_maxGrassValue)
            {
                var rand = Random.Range(0, 100f);
                //55% is grass
                return rand <= 55 ? 2 : 3;
            }
            
            // if (noise > m_maxGrassValue && noise <= m_maxTreeValue)
            // {
            //     return 3;
            // }

            return 1;
        }

        private IEnumerator GenerateWorldRoutine()
        {
            m_isGenerateDone = false;
            
            GenerateData();
            
            m_groundTilemap.ClearAllTiles();
            m_waterTilemap.ClearAllTiles();

            for (int y = 0; y < m_mapHeight; y++)
            {
                for (int x = 0; x < m_mapWidth; x++)
                {
                    SetProperTile(x - m_mapWidth/2, y - m_mapHeight/2, m_worldArr[x + y * m_mapWidth]);
                }
            
                yield return null;
            }

            m_isGenerateDone = true;
        }

        private void SetProperTile(int x, int y, int sample)
        {
            switch (sample)
            {
                //Water
                case 0:
                    m_waterTilemap.SetTile(new Vector3Int(x,y), m_waterTile) ;
                    break;
                //Ground
                case 1:
                    var randGround = Random.Range(0, 100);
                    if (randGround < 40)
                    {
                        m_groundTilemap.SetTile(new Vector3Int(x,y), m_groundTiles.GetTileRandom());
                    }
                    break;
                //Grass    
                case 2:
                    var rand = Random.Range(0, 100);
                    m_groundTilemap.SetTile(new Vector3Int(x, y),
                        rand <= m_percentToSpawnGrass ? m_grassTiles.GetTileRandom() : m_groundTiles.GetTileRandom());
                    break;
            }
        }

        private float GetNoiseValue(int x, int y, float offsetX, float offsetY )
        {
            float xCoord = x / m_scale + offsetX;
            float yCoord = y / m_scale + offsetY;

            var sample = Mathf.PerlinNoise(xCoord, yCoord);
            //Debug.Log($"Sample {sample}");
            return sample;
        }

        public Vector2 GetSpawnPointInWorldCoord()
        {
            int x = Random.Range(1,150);
            int y = Random.Range(1,150);
            while (m_worldArr[x + y * m_mapWidth] != 1)
            {
                x = Random.Range(1,150);
                y = Random.Range(1,150);
            }
            //Debug.Log($"Value spawnpoint {m_worldArr[x + y * m_mapWidth]}");
            return new Vector2((x - m_mapWidth/2) * 0.5f, (y - m_mapHeight/2)* 0.5f);
        }
    }
}
