using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComixScene : MonoBehaviour
{
    [SerializeField] 
    private Sprite[] comixImages;

    [SerializeField]
    private Image comixPanel;

    private void Start()
    {
        StartCoroutine(PlayComix());
    }

    private IEnumerator PlayComix()
    {
        // Loop through each image in the comixImages array
        foreach (var sprite in comixImages)
        {
            comixPanel.sprite = sprite;
            // Wait for 2 seconds before showing the next image
            yield return new WaitForSeconds(20f);
        }
        
        // Load the next scene after the comix is done playing
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void OnSkipButtonClicked()
    {
        Debug.Log("Comix skipped!");
        // Skip the comix and load the next scene immediately
        StopAllCoroutines();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
