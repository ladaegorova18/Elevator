using UnityEngine;
using UnityEngine.UI;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField]
    private Image[] starsImages;

    [SerializeField]
    private Sprite activeStarImage;

    [SerializeField]
    private Sprite inActiveStarImage;
    
    public void SetResult(float successRate) {
        Debug.Log($"Success rate: {successRate}");
        starsImages[0].sprite = (successRate >= 0.5f) ? activeStarImage : inActiveStarImage;
        starsImages[1].sprite = (successRate >= 0.7f) ? activeStarImage : inActiveStarImage;
        starsImages[2].sprite = (successRate >= 0.9f) ? activeStarImage : inActiveStarImage;
    }
}
