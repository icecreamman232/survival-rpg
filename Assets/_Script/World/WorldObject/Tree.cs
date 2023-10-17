using System;
using JustGame.Script.Managers;
using UnityEngine;

namespace JustGame.Script.World
{
    public class Tree : MonoBehaviour
    {
        [SerializeField] private int m_hitToTake;
        [SerializeField] private GameObject m_woodPilePrefab;

        private int m_remainingHitToTake;

        private void Start()
        {
            m_remainingHitToTake = m_hitToTake;
        }


        public void TakeHit()
        {
            m_remainingHitToTake--;
            if (m_remainingHitToTake <= 0)
            {
                CreateWoodPile();
                this.gameObject.SetActive(false);
            }
        }
        
        private void CreateWoodPile()
        {
            var wood = Instantiate(m_woodPilePrefab, transform.position, Quaternion.identity);
        }
    }
}
