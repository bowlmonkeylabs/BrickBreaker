using System;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BML.Scripts
{
    [Serializable]
    public struct CollisionType
    {
        public LayerMask layerMask;
        public string tag;
        [LabelText("Include/Exclude Tag")] public bool includeExcludeTag;
        public MMFeedbacks feedbacks;
    }
    
    public class CollisionFeedbacks : MonoBehaviour
    {
        [SerializeField] private List<CollisionType> collisionTypes;

        private void OnCollisionEnter2D(Collision2D col)
        {
            var colLayer = col.gameObject.layer;
            var colTag = col.gameObject.tag;
            collisionTypes.ForEach(collisionType =>
            {
                var matchesLayer = collisionType.layerMask == (collisionType.layerMask | (1 << colLayer));
                var matchesTag = string.IsNullOrEmpty(collisionType.tag) 
                                 || (collisionType.includeExcludeTag == (collisionType.tag == colTag));
                if (matchesLayer && matchesTag)
                {
                    collisionType.feedbacks.PlayFeedbacks();
                }
            });
        }
    }
}