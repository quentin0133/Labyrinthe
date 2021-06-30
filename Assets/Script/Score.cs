using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score; 

    private void Awake()
    {
        int time = (int) Parameter.instance.Time;

        if(time / 60 > 1)
            score.text += time / 60 + " minutes et ";
        else if(time / 60 == 1)
            score.text += time / 60 + " minute et ";

        if (time % 60 > 1f)
            score.text += time % 60 + " secondes";
        else
            score.text += time / 60 + " seconde";
    }
}
