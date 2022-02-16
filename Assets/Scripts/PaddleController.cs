using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BML.Scripts
{
    public class PaddleController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D paddleRb;
        [SerializeField] private Collider2D paddleCollider;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private AnimationCurve angularInfluence;

        public void Move(InputAction.CallbackContext cntx)
        {
            Vector2 moveInput = cntx.ReadValue<Vector2>();
            paddleRb.velocity = moveInput * moveSpeed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // var velocity = other.rigidbody.velocity;
            // other.rigidbody.velocity = new Vector2(5, 0);

            var contactPoint = other.contacts[0].point;
            var differenceFromCenter = contactPoint.x - paddleRb.position.x;
            var sign = Mathf.Sign(differenceFromCenter);
            var distanceFromCenter = Mathf.Abs(differenceFromCenter);
            
            var paddleWidth = paddleCollider.bounds.extents.x;
            var distFac = (2 * distanceFromCenter) / (paddleWidth);
            var angle = angularInfluence.Evaluate(distFac);
            var rotationInfluence = Quaternion.AngleAxis(angle * sign, Vector3.back);
            
            var speed = other.rigidbody.velocity.magnitude;
            other.rigidbody.velocity = rotationInfluence * paddleRb.transform.up * speed;
        }
    }
}