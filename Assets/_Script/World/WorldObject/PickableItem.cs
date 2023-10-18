using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustGame.Script.World
{
    public class PickableItem : MonoBehaviour
    {
        public virtual void Pick()
        {
            Destroy(this.gameObject);
        }
    }
}

