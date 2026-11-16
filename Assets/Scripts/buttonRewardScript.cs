using UnityEngine;

public class buttonRewardScript : MonoBehaviour
{
    public PlayerScript PlayerScript;
    public void rewardChosen1()
    {     
        Time.timeScale = 1.0f;
        PlayerScript.rewardTaken(0);
        gameObject.SetActive(false);
    }
    public void rewardChosen2()
    {
        Time.timeScale = 1.0f;
        PlayerScript.rewardTaken(1);
        gameObject.SetActive(false);
    }
    public void rewardChosen3()
    {
        Time.timeScale = 1.0f;
        PlayerScript.rewardTaken(2);
        gameObject.SetActive(false);
    }
}
