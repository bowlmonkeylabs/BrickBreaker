using MyAssets.ScriptableObjects.Events;
using UnityEngine;

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
        if(this.bricksParent.childCount <= 0)
        {
            allBricksBroken.Raise();
        }
    }

    private void OnDestroy()
    {
        brickBroken.Unsubscribe(CheckBricksRemaining);
    }
}
