using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BallController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D ballRb;
        [SerializeField] private Vector2 initialDirection;
        [SerializeField] private float initialSpeed = 5f;

        private void Start()
        {
            ballRb.velocity = initialDirection * initialSpeed;
        }
    }
}