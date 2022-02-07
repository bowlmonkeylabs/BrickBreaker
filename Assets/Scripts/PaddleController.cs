using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class PaddleController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D paddleRb;
        [SerializeField] private float moveSpeed = 5f;
        
        public void Move(InputAction.CallbackContext cntx)
        {
            Vector2 moveInput = cntx.ReadValue<Vector2>();
            paddleRb.velocity = moveInput * moveSpeed;
        }
    }
}