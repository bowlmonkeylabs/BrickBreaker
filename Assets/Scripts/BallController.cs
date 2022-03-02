using MyAssets.ScriptableObjects.Variables;
using MyAssets.ScriptableObjects.Events;
using UnityEngine;
using Sirenix.OdinInspector;

namespace BML.Scripts
{
    public class BallController : SerializedMonoBehaviour
    {
        [SerializeField] private Rigidbody2D ballRb;
        [SerializeField] private Vector2 initialDirection;

        [SerializeField] private FloatReference ballSpeed;

        [SerializeField] private GameEvent brickBroken;
        [SerializeField] private Transform bricksParent;
        [SerializeField] private float maxBallSpeed = 20;
        [SerializeField] private AnimationCurve speedCurve;

        private int startingBrickCount;

        private void Start()
        {
            this.ballSpeed.Reset();

            this.ballRb.velocity = initialDirection.normalized * ballSpeed.Value;

            this.brickBroken.Subscribe(this.OnBrickBroken);

            this.startingBrickCount = bricksParent.childCount;
        }

        private void OnBrickBroken()
        {
            float speedFactor = speedCurve.Evaluate(1 - ((float)bricksParent.childCount / (float)this.startingBrickCount));
            this.ballSpeed.Value = Mathf.Lerp(this.ballSpeed.DefaultValue, this.maxBallSpeed, speedFactor);
        }

        private void OnDestroy()
        {
            this.brickBroken.Unsubscribe(this.OnBrickBroken);
        }
    }
}