using System;
using Common;
using UnityEngine;
using Random = System.Random;

namespace Stage2
{
    public class SpiderEnemy : Enemy
    {
        private Rigidbody _rb;
        private readonly Random _r = new Random();
    
        public float speed;
        public int directionChangeFrameCount;
        public float maxX;
        public float maxZ;

        private int _frameCount = 0;
    
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }
    
        private void FixedUpdate()
        {
            ReverseMovementIfExitingPlane();
        
            _frameCount++;
            if (_frameCount % directionChangeFrameCount != 0)
                return;
        
            var movementX = (float)_r.NextDouble() * 2 - 1;
            var movementZ = (float)Math.Sqrt(1 - movementX * movementX) * (_r.Next(0, 2) * 2 - 1);
        
            var newVelocity = new Vector3(movementX, 0, movementZ) * speed;

            SetVelocityAndRotation(newVelocity);
        }

        private void ReverseMovementIfExitingPlane()
        {
            var currentVelocity = _rb.velocity;

            if (Math.Abs(transform.localPosition.x) > maxX && Math.Sign(currentVelocity.x) == Math.Sign(transform.localPosition.x))
                SetVelocityAndRotation(new Vector3(-currentVelocity.x, currentVelocity.y, currentVelocity.z));
        
            if(Math.Abs(transform.localPosition.z) > maxZ && Math.Sign(currentVelocity.z) == Math.Sign(transform.localPosition.z))
                SetVelocityAndRotation(new Vector3(currentVelocity.x, currentVelocity.y, -currentVelocity.z));
        }

        private void SetVelocityAndRotation(Vector3 newVelocity)
        {
            var newAngle = (float)Math.Atan2(newVelocity.x, newVelocity.z) * (180/(float)Math.PI);
        
            _rb.velocity = newVelocity;
            _rb.rotation = Quaternion.AngleAxis(newAngle, Vector3.up);
        }

        public override void SetLocation(Vector3 location)
        {
            transform.position = location;
        }
    }
}
