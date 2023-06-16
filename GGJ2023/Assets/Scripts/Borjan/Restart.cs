using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartGame(){
        GameState.instance.state = State.Menu;
        SceneManager.LoadScene(sceneBuildIndex:0);
        MusicPlayer.PlayMenuMusic();
    }
}
