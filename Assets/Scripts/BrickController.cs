using System;
using MoreMountains.Feedbacks;
using MyAssets.ScriptableObjects.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BML.Scripts
{
    public class BrickController : MonoBehaviour
    {
        [SerializeField] private string ballTag = "Ball";
        [SerializeField] private GameEvent brickBroken;
        [SerializeField] private GameObject leanTweenOwner;
        [SerializeField] private Collider2D brickCollider;
        [SerializeField] private MMFeedbacks onBreakFeedbacks;

        [ReadOnly] public bool IsNotBroken { get; private set; }

        private void Awake()
        {
            IsNotBroken = true;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.tag.Equals(ballTag))
            {
                BreakBrick();
            }
        }

        private void BreakBrick()
        {
            brickCollider.enabled = false;

            IsNotBroken = false;
            brickBroken.Raise();
            
            onBreakFeedbacks.PlayFeedbacks();
        }
    }
}