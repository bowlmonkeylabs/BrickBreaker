using MyAssets.ScriptableObjects.Events;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BML.Scripts
{
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text displayMessageText;
        [SerializeField] private GameEvent ballPassedPaddleEvent;
        [SerializeField] private float MessageTime = 3f;
        [SerializeField] private string LoseMessage = "You Lose!";
        [SerializeField] private string WinMessage = "You Don't Lose!";
        //DisplayMessage("You lose!");

        private void Start()
        {
            ballPassedPaddleEvent.Subscribe(OnLose);
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
        }

        private void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
