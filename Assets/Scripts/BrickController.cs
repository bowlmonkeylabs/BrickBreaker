using MyAssets.ScriptableObjects.Events;
using UnityEngine;

namespace BML.Scripts
{
    public class BrickController : MonoBehaviour
    {
        [SerializeField] private string ballTag = "Ball";
        [SerializeField] private GameEvent brickBroken;
        [SerializeField] private GameObject leanTweenOwner;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.tag.Equals(ballTag))
            {
                Destroy(gameObject);
                LeanTween.value(leanTweenOwner, 0, 1, 0.01f).setOnComplete(() => {
                    brickBroken.Raise();
                });
            }
        }
    }
}