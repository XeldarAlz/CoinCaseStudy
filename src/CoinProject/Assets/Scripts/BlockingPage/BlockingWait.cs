using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BlockingPage
{
    /// <summary>
    /// Represents a blocking wait screen with a progress bar and text.
    /// This class provides functionality to display a progress bar animation and notify when the progress is complete.
    /// </summary>
    public class BlockingWait : MonoBehaviour
    {
        [Header("UI Elements")]

        [Tooltip("The slider component representing the progress bar.")]
        [SerializeField] private Slider progressBar;

        [Tooltip("The text component displaying the progress percentage.")]
        [SerializeField] private TextMeshProUGUI progressText;

        [Tooltip("The popup that appears when the progress is finished.")]
        [SerializeField] private GameObject finishedPopup;

        [Tooltip("The duration it takes for the progress bar to complete.")]
        [SerializeField][Range(1f, 5f)] private float progressBarCompleteDuration = 2f;

        /// <summary>
        /// Initiates the progress bar animation from 0% to 100%.
        /// </summary>
        /// <param name="onComplete">An optional callback that is invoked when the progress bar reaches 100%.</param>
        public void StartProgressBar(System.Action onComplete = null)
        {
            progressBar.value = 0;
            progressText.text = "0%";
            StartCoroutine(ProgressCoroutine(onComplete));
        }

        /// <summary>
        /// Coroutine to animate the progress bar and update the progress text.
        /// </summary>
        /// <param name="onComplete">Callback to be invoked when the progress bar reaches 100%.</param>
        /// <returns>An IEnumerator used for coroutine execution.</returns>
        private IEnumerator ProgressCoroutine(System.Action onComplete)
        {
            float elapsed = 0.0f;

            while (elapsed < progressBarCompleteDuration)
            {
                elapsed += Time.deltaTime;
                float normalizedValue = Mathf.Lerp(0, 1, elapsed / progressBarCompleteDuration);
                progressBar.value = normalizedValue;
                progressText.text = $"{Mathf.RoundToInt(normalizedValue * 100)}%";

                yield return null;
            }

            progressBar.value = 1;
            progressText.text = "100%";
            finishedPopup.SetActive(true);
        }

        /// <summary>
        /// Closes the blocking screen and deactivates the finished popup.
        /// </summary>
        public void CloseBlockingScreen()
        {
            finishedPopup.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}