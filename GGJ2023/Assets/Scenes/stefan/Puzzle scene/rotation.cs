using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{

    private void OnMouseDown(){

        if(GameControl.youWin == false) {

            transform.Rotate(0,0,90);

        }


    }


}
