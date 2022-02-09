using MyAssets.ScriptableObjects.Events;
using UnityEngine;

namespace BML.Scripts
{
    public class FailZoneController : MonoBehaviour
    {
        [SerializeField] private string ballTag = "Ball";
        [SerializeField] private GameEvent ballPassedPaddleEvent;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag.Equals(ballTag))
            {
                ballPassedPaddleEvent.Raise();
            }
        }
    }
}

