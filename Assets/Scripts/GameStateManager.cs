using System;
using MyAssets.ScriptableObjects.Events;
using MyAssets.ScriptableObjects.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace BML.Scripts
{
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text displayMessageText;
        [SerializeField] private GameEvent ballPassedPaddleEvent;
        [SerializeField] private GameEvent allBricksBroken;
        [SerializeField] private GameEvent catchPressed;
        [SerializeField] private GameEvent onGameStarted;
        [SerializeField] private BoolReference isGameStarted;
        [SerializeField] private float MessageTime = 3f;
        [SerializeField] private string LoseMessage = "You Lose!";
        [SerializeField] private string WinMessage = "You Don't Lose!";
        [SerializeField] private string StartMessage = "Press Space to Start";

        private void Awake()
        {
            ballPassedPaddleEvent.Subscribe(OnLose);
            allBricksBroken.Subscribe(OnWin);
            catchPressed.Subscribe(StartGame);
        }
        
        private void OnDisable()
        {
            ballPassedPaddleEvent.Unsubscribe(OnLose);
            allBricksBroken.Unsubscribe(OnWin);
            catchPressed.Unsubscribe(StartGame);
        }

        private void Start()
        {
            isGameStarted.Value = false;
            displayMessageText.text = StartMessage;
        }

        private void StartGame()
        {
            if (!isGameStarted.Value)
            {
                isGameStarted.Value = true;
                onGameStarted.Raise();
                displayMessageText.text = "";
            }
        }

        private void OnLose()
        {
            DisplayMessage(LoseMessage);
            LeanTween.value(this.gameObject, 0, 1, MessageTime).setOnComplete(RestartScene);
        }

        private void OnWin()
        {
            DisplayMessage(WinMessage);
            LeanTween.value(this.gameObject, 0, 1, MessageTime).setOnComplete(RestartScene);
        }

        private void DisplayMessage(string message)
        {
            displayMessageText.text = message;
        }

        private void OnDestroy()
        {
            ballPassedPaddleEvent.Unsubscribe(OnLose);
            allBricksBroken.Unsubscribe(OnWin);
        }

        private void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
