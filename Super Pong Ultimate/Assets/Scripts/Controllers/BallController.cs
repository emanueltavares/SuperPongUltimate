﻿using UnityEngine;

namespace Application.Controllers
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private Vector2 _direction = Vector2.zero;
        [SerializeField] private float _speed = 0;

        private Rigidbody2D _rigidbody2D;
        private bool _flipX = false;
        private bool _flipY = false;

        protected virtual void OnEnable()
        {
            if (_rigidbody2D == null)
            {
                _rigidbody2D = GetComponent<Rigidbody2D>();
            }
        }

        protected virtual void Update()
        {
            if (_flipX)
            {
                _direction.x = -_direction.x; // flip X by negative X
                _flipX = false;
            }
            if (_flipY)
            {
                _direction.y = -_direction.y; // flip Y by negative Y
                _flipY = false;
            }

            Vector2 velocity = _direction * _speed;
            _rigidbody2D.velocity = velocity * Time.deltaTime;
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision2D)
        {
            // Obtain the normal by adding all normal from all contact points 
            Vector2 contactNormal = Vector2.zero;
            for (int i = 0; i < collision2D.contactCount; i++)
            {
                ContactPoint2D contactPoint = collision2D.GetContact(i);
                contactNormal += contactPoint.normal;
            }
            contactNormal /= collision2D.contactCount;

            // If contact normal is either down or up, we will flip Y
            if (contactNormal == Vector2.down || contactNormal == Vector2.up)
            {
                _flipY = true;
            }
            // If contact normal is either left or right, we will flip X
            else if (contactNormal == Vector2.left || contactNormal == Vector2.right)
            {
                _flipX = true;
            }
        }
    }
}