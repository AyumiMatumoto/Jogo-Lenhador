using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public void IniciaJogo()
    {
        SoundManager.instance.PlayFx(SoundManager.instance.fxPlay);
        SceneManager.LoadScene("Game");
    }
}
