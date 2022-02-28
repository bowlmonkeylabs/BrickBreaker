using System;
using MyAssets.ScriptableObjects.Events;
using MyAssets.ScriptableObjects.Variables;
using UnityEngine;
using Sirenix.OdinInspector;

namespace BML.Scripts
{
    public class BallController : SerializedMonoBehaviour
    {
        [SerializeField] private Rigidbody2D ballRb;
        [SerializeField] private Vector2 initialDirection;
        [SerializeField] private FloatReference ballSpeed;
        [SerializeField] private GameEvent onGameStarted;

        private void Awake()
        {
            onGameStarted.Subscribe(StartMoving);
        }

        private void OnDisable()
        {
            onGameStarted.Unsubscribe(StartMoving);
        }

        private void StartMoving()
        {
            ballRb.velocity = initialDirection.normalized * ballSpeed.Value;
        }
    }
}