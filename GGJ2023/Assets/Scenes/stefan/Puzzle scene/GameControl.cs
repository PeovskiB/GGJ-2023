using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{

    [SerializeField]
    private Transform[] pics;

    

    public static bool youWin;




    // Start is called before the first frame update
    void Start()
    {
       
        youWin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pics[0].rotation.z == 0 &&
            pics[1].rotation.z == 0 &&
            pics[2].rotation.z == 0 &&
            pics[3].rotation.z == 0 &&
            pics[4].rotation.z == 0 &&
            pics[5].rotation.z == 0 &&
            pics[6].rotation.z == 0 &&
            pics[7].rotation.z == 0 &&
            pics[8].rotation.z == 0 &&
            pics[9].rotation.z == 0 &&
            pics[10].rotation.z == 0 &&
            pics[11].rotation.z == 0 &&
            pics[12].rotation.z == 0 &&
            pics[13].rotation.z == 0 &&
            pics[14].rotation.z == 0 &&
            pics[15].rotation.z == 0 &&
            pics[16].rotation.z == 0 &&
            pics[17].rotation.z == 0 &&
            pics[18].rotation.z == 0 &&
            pics[19].rotation.z == 0)

            {

            youWin = true;
           

        }




        
    }
}
