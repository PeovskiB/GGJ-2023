using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{


    [SerializeField] Image UIImage;
    [SerializeField] Sprite[] images;

    int pageNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        pageNumber = 0;
        UIImage.sprite = images[pageNumber];
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
            Next();
    }


    public void Next(){
        pageNumber++;
        if(pageNumber < images.Length)
            UIImage.sprite = images[pageNumber];
        else{
            GameState.instance.SwitchState(State.Playing);
            //SceneManager.LoadScene(1);
        }
           

    }

}
