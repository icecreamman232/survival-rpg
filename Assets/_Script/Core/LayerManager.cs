using UnityEngine;

namespace JustGame.Script.Managers
{
    public static class LayerManager
    {
        #region Layers
        public static int PlayerLayer = 6;
        public static int EnemyLayer = 7;
        public static int WorldObject = 8;
        #endregion

        #region Layer Masks
        public static int PlayerMask = 1 << PlayerLayer;
        public static int EnemyMask = 1 << EnemyLayer;
        public static int WorldObjectMask = 1 << WorldObject;
        
        //public static int PlayerMask = DoorMask | WallMask;
        #endregion
        
        public static bool IsInLayerMask(int layerWantToCheck, LayerMask layerMask)
        {
            if (((1 << layerWantToCheck) & layerMask) != 0)
            {
                return true;
            }
            return false;
        }
    }
}
