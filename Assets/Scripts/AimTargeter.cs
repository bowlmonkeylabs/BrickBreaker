using System;
using Shapes;
using UnityEngine;

namespace BML.Scripts
{
    public class AimTargeter : MonoBehaviour
    {
        [SerializeField] private Line aimTargeterLine;
        [SerializeField] private TimerVariable paddleReleaseTimer;
        [SerializeField] private float minLineLength = .1f;
        [SerializeField] private float maxLineLength = 1f;
        [SerializeField] private AnimationCurve lengthCurve;

        private void Update()
        {
            if (paddleReleaseTimer.IsStopped) return;
            
            var percentToRelease = paddleReleaseTimer.ElapsedTime / paddleReleaseTimer.Duration;
            var releaseFactor = lengthCurve.Evaluate(percentToRelease);
            aimTargeterLine.End = Mathf.Lerp(maxLineLength, minLineLength, releaseFactor) * Vector2.up;
        }
    }
}