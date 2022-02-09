using System;
using UnityEngine;

namespace BML.Scripts
{
    public class BrickController : MonoBehaviour
    {
        [SerializeField] private string ballTag = "Ball";
        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.tag.Equals(ballTag))
                Destroy(gameObject);
        }
    }
}