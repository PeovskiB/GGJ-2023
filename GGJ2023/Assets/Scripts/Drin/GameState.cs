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
        if (anim != null || state == State.Menu || win) return;

        anim = GameObject.FindWithTag("Fadeout").GetComponent<Animator>();
    }

    public IEnumerator DeathRoutine()
    {
        anim.SetBool("fadeout", true);
        for (float i = 0; i < 1f; i += Time.deltaTime)
        {
            yield return null;
        }

        if (win)
            SceneManager.LoadScene(2);
        else
            SceneManager.LoadScene(1);
        yield break;
    }

    public static void MenuToGame()
    {
        instance.SwitchState(State.Playing);
    }

    private bool win = false;

    public static void WinScreen()
    {
        // instance.StartCoroutine(instance.DeathRoutine());
        // instance.win = true;
        // Movement.instance.died = true;
        // WormController.Stop();
        SceneManager.LoadScene(sceneBuildIndex:2);


        // Utils.Freeze(3f);
    }

    public static void Die()
    {
        instance.SwitchState(State.Death);
        Movement.instance.died = true;
        // Utils.Freeze(3f);
    }

    private void SwitchState(State newState)
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
                    instance.StartCoroutine(instance.DeathRoutine());
                }
                break;
        }
        instance.state = newState;
    }
}
