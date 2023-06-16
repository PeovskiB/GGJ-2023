using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum State
{
    Menu,
    Playing,
    Death
}

public class GameState : MonoBehaviour
{
    [SerializeField]
    public State state;
    public static GameState instance;
    private Animator anim;
    private Animator rootAnimator = null;
    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            state = State.Menu;
        }
    }

    void Update()
    {
        //if (anim != null || state == State.Menu || win || SceneManager.GetActiveScene().buildIndex == 3) return;

        //anim = GameObject.FindWithTag("Fadeout").GetComponent<Animator>();
        //Debug.Log(anim);

        if(rootAnimator == null){
            if(GameObject.FindGameObjectWithTag("Roots") != null)
                rootAnimator = GameObject.FindGameObjectWithTag("Roots").GetComponent<Animator>();
        }
        
    }

    public IEnumerator DeathRoutine()
    {
        anim.SetBool("fadeout", true);
        // for (float i = 0; i < 1f; i += Time.deltaTime)
        // {
        //     yield return null;
        // }
        yield return new WaitForSecondsRealtime(1f);

        if (win)
            SceneManager.LoadScene(2);
        else{
            SceneManager.LoadScene(1);
        }

        Movement.instance.died = false;
        state = State.Playing;    

        yield break;
    }

    public static void MenuToGame()
    {
        //instance.SwitchState(State.Playing);
        SceneManager.LoadScene(3);
        MusicPlayer.PlayGameMusic();
    }

    private bool win = false;

    public void WinScreen()
    {
        // instance.StartCoroutine(instance.DeathRoutine());
        // instance.win = true;
        // Movement.instance.died = true;
        // WormController.Stop();
        

        rootAnimator.SetBool("Win", true);
        Destroy(GameObject.FindGameObjectWithTag("Boss"));
        Camera.main.gameObject.transform.GetComponentInParent<CameraController>().offset.y = 40;
        Invoke("LoadWin", 5.5f);

        // Utils.Freeze(3f);
    }

    private void LoadWin(){
        SceneManager.LoadScene(sceneBuildIndex:2);
    }

    public static void Die()
    {
        instance.SwitchState(State.Death);
        //Movement.instance.died = true;
        // Utils.Freeze(3f);
    }

    public void SwitchState(State newState)
    {
        switch (newState)
        {
            case State.Playing:
                if (instance.state == State.Menu)
                {
                    SceneManager.LoadScene(1);
                }
                else if (instance.state == State.Death)
                {
                    instance.StartCoroutine("DeathRoutine");
                }
                break;
        }
        instance.state = newState;
    }
}
