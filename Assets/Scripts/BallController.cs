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

        private void Start()
        {
            ballRb.velocity = initialDirection.normalized * ballSpeed.Value;
        }
    }
}