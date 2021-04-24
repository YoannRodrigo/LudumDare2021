using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private Collider2D _collider2D;

        [SerializeField] private float _speed = default;
        [SerializeField] private float _jumpForce = default;

        private bool _isOnGround = false;

        private void Awake()
        {
            _rigidbody2D.velocity = transform.forward * _speed;
            DontDestroyOnLoad(gameObject);
        }

        private void OnJump()
        {
            if (_isOnGround)
            {
                _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _isOnGround = true;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            _isOnGround = false;
        }
    }
}
