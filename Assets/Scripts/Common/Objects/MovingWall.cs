using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Objects
{
    public class MovingWall: MonoBehaviour
    {
        public bool moveX;

        public bool moveZ;

        // Start is called before the first frame update
        void Start()
        {
            
        }

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

        // Update is called once per frame
        void Update()
        {

        }
    }
}
