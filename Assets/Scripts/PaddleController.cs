using System;
using MyAssets.ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

namespace BML.Scripts
{
    public class PaddleController : SerializedMonoBehaviour
    {
        [SerializeField] private Rigidbody2D paddleRb;
        [SerializeField] private Rigidbody2D ballRb;
        [SerializeField] private Transform aimTargeter;
        [SerializeField] private Collider2D paddleCollider;
        [SerializeField] private Transform ballCatchPivot;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float aimTurnSpeed = 5f;
        [SerializeField] private AnimationCurve angularInfluenceWeight;
        [SerializeField] private float maxAngularInfluence = 60f;
        [SerializeField] private string ballTag = "Ball";
        [SerializeField] private bool locationInfluencesAngle = true;
        [SerializeField] private FloatReference ballSpeed;
        [SerializeField] private BoolReference isGameStarted;

        private Vector2 moveInput;
        private bool isBallInCatchTrigger;
        private bool isBallCaught;

        private void Update()
        {
            if (!isGameStarted.Value) return;
            
            if (isBallCaught)
                aimTargeter.Rotate(Vector3.back, moveInput.x * aimTurnSpeed * Time.deltaTime);
            else
                paddleRb.velocity = moveInput * moveSpeed;
            
        }

        private void FixedUpdate()
        {
            if (isBallCaught)
                ballRb.MovePosition(ballCatchPivot.position);
        }

        public void ReceiveMove(InputAction.CallbackContext cntx)
        {
            moveInput = cntx.ReadValue<Vector2>();
        }

        public void ReceiveCatch(InputAction.CallbackContext cntx)
        {
            if (!isBallInCatchTrigger || !cntx.performed) return;
            
            if (isBallCaught)
            {
                //Release
                ballRb.velocity = aimTargeter.up * ballSpeed.Value;
                isBallCaught = false;
                aimTargeter.gameObject.SetActive(false);
                return;
            }

            isBallCaught = true;
            paddleRb.velocity = Vector2.zero;
            aimTargeter.gameObject.SetActive(true);
            aimTargeter.rotation = Quaternion.identity;
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(ballTag))
                isBallInCatchTrigger = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(ballTag))
                isBallInCatchTrigger = false;
        }
    }
}