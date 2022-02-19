using MyAssets.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

namespace BML.Scripts
{
    public class PaddleController : SerializedMonoBehaviour
    {
        [SerializeField] private Rigidbody2D paddleRb;
        [SerializeField] private Collider2D paddleCollider;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private AnimationCurve angularInfluenceWeight;
        [SerializeField] private float maxAngularInfluence = 60f;
        [SerializeField] private string ballTag = "Ball";
        [SerializeField] private bool locationInfluencesAngle = true;
        [SerializeField] private FloatReference ballSpeed;

        public void Move(InputAction.CallbackContext cntx)
        {
            Vector2 moveInput = cntx.ReadValue<Vector2>();
            paddleRb.velocity = moveInput * moveSpeed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.collider.CompareTag(ballTag))
            {
                if(locationInfluencesAngle)
                {
                    var contactPoint = other.contacts[0].point;
                    var differenceFromCenter = contactPoint.x - paddleRb.position.x;
                    var sign = Mathf.Sign(differenceFromCenter);
                    var distanceFromCenter = Mathf.Abs(differenceFromCenter);
                    var paddleWidth = paddleCollider.bounds.extents.x;

                    var distFac = (2 * distanceFromCenter) / (paddleWidth);
                    var weight = angularInfluenceWeight.Evaluate(distFac);

                    var maxRotationInfluence = Quaternion.AngleAxis(maxAngularInfluence * sign, Vector3.back);

                    var reflectVelocity = other.rigidbody.velocity.normalized * ballSpeed.Value;
                    var maxReflectVelocity = maxRotationInfluence * Vector3.up * ballSpeed.Value;

                    other.rigidbody.velocity = Vector2.Lerp(reflectVelocity, maxReflectVelocity, weight);
                }
            }
        }
    }
}