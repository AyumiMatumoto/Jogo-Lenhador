using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    public Text best;
    public Text score;
	// Use this for initialization
	void Start () {
        best.text = PlayerPrefs.GetInt("best").ToString();
        score.text = PlayerPrefs.GetInt("score").ToString();
    }
	
    public void JogarNovamente()
    {
        SoundManager.instance.PlayFx(SoundManager.instance.fxPlay);
        SceneManager.LoadScene("Game");
    }
}
