using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComixScene : MonoBehaviour
{
    [SerializeField] 
    private Sprite[] comixImages;

    [SerializeField]
    private Image comixPanel;

    private int currentPage = 0;
    private float timeToShow = 20f; 

    private void Start()
    {
        StartCoroutine(PlayComix());
    }

    private IEnumerator PlayComix()
    {
        while (currentPage < comixImages.Length)
        {
            NextPage();
            // Wait for the specified time before showing the next image
            yield return new WaitForSeconds(timeToShow);
        }
        
        // Load the next scene after the comix is done playing
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void OnSkipButtonClicked()
    {
        Debug.Log("Comix skipped!");
        if (currentPage < comixImages.Length) {
            NextPage();
        }
        else
        {
            Debug.Log("Comix finished, loading MainScene.");
            StopAllCoroutines();
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        }
    }

    private void NextPage() 
    {
        Debug.Log($"Current page: {currentPage}");
        comixPanel.sprite = comixImages[currentPage];
        currentPage++;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}
