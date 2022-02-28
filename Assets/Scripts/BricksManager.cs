using System.Linq;
using MyAssets.ScriptableObjects.Events;
using UnityEngine;

namespace BML.Scripts
{
    public class BricksManager : MonoBehaviour
    {
        [SerializeField] private GameEvent brickBroken;
        [SerializeField] private GameEvent allBricksBroken;
        [SerializeField] private Transform bricksParent;

        private void Awake()
        {
            brickBroken.Subscribe(CheckBricksRemaining);
        }

        private void CheckBricksRemaining()
        {
            var bricks = bricksParent.GetComponentsInChildren<BrickController>();
            var anyBricksRemain = bricks.Any(brick => brick.IsNotBroken);
            if (!anyBricksRemain)
            {
                allBricksBroken.Raise();
            }
        }

        private void OnDestroy()
        {
            brickBroken.Unsubscribe(CheckBricksRemaining);
        }
    }
}
