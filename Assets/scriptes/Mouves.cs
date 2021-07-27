
using UnityEngine;
using UnityEngine.UI;

public class Mouves : MonoBehaviour
{
    public int mouves = 0;

    private Text textFeild = null;

    private void Start()
    {
        textFeild = GetComponent<Text>();
    }

    public void specificScore(int newScore)
    {
        mouves = newScore;
        textFeild.text = "Mouves : " + mouves.ToString();
    }

    public void changeText()
    {
        mouves++;
        textFeild.text = "Mouves : " + mouves.ToString();
    }

}
