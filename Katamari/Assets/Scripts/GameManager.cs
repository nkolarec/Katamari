using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.Scripts
{
    /// <summary>
    /// Class for handling game flow
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseMenu = null;

        /// <summary>
        /// Function that pauses the game on button click and reveals pause menu.
        /// </summary>
        public void PauseGame()
        {
            Time.timeScale = 0;
            _pauseMenu.SetActive(true);
            Debug.Log("Game paused.");
        }

        /// <summary>
        /// Function that continues the game on button click.
        /// </summary>
        public void ContinueGame()
        {
            Time.timeScale = 1;
            _pauseMenu.gameObject.SetActive(false);
            Debug.Log("Game continued.");
        }

        /// <summary>
        /// Function that reloads the current scene on button click, restarting the game.
        /// </summary>
        public void RestartGame()
        {
            Time.timeScale = 1;
            _pauseMenu.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Game restarted.");
        }

        /// <summary>
        /// Function that ends the game on button click, exiting the application.
        /// </summary>
        public void EndGame()
        {
            Application.Quit();
        }
    }
}

