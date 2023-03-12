using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Objects
{
    public class MovingWall: MonoBehaviour
    {
        public bool moveX;
        public bool moveZ;
        
        public void SetLocation(Vector3 newLocation)
        {
            var posX = transform.position.x;
            var posZ = transform.position.z;
            if (moveX)
                posX = newLocation.x;

            if (moveZ)
                posZ = newLocation.z;

            transform.position = new Vector3(posX, transform.position.y, posZ);
        }
    }
}
