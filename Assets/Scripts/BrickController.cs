using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BrickController : MonoBehaviour
    {
        [SerializeField] private string ballTag = "Ball";
        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("Received");
            
            if (other.gameObject.tag.Equals(ballTag))
                Destroy(this);
        }
    }
}