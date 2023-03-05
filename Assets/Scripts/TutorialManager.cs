using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    } 

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex){
                popUps[i].SetActive(true);
            } else {
                popUps[i].SetActive(false);
            }
        }
        if(popUpIndex == 0){
            //if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.D)){
            if(Input.GetKeyDown(KeyCode.Tab)){
                popUpIndex++;
            }
        } else if (popUpIndex == 1){
            if(Input.GetKeyDown(KeyCode.Tab)){
                popUpIndex++;
            }
        } else if (popUpIndex == 2){
            if(Input.GetKeyDown(KeyCode.Tab)){
                popUpIndex++;
            }
        } else if (popUpIndex == 3){
            if(Input.GetKeyDown(KeyCode.Tab)){
                popUpIndex++;
            }
        } else if (popUpIndex == 4){
            if(Input.GetKeyDown(KeyCode.Tab)){
                popUpIndex++;
            }
        } else if (popUpIndex == 5){
            if(Input.GetKeyDown(KeyCode.Tab)){
                popUpIndex++;
            }
        } else if (popUpIndex == 6){
            if(Input.GetKeyDown(KeyCode.Tab)){
                popUpIndex++;
            }
        } else if (popUpIndex == 7){
            if(Input.GetKeyDown(KeyCode.Tab)){
                popUpIndex++;
            }
        } else if (popUpIndex == 8){
            if(Input.GetKeyDown(KeyCode.Tab)){
                popUpIndex++;
            }
        } else if (popUpIndex == 9){
            if(Input.GetKeyDown(KeyCode.Tab)){
                popUpIndex++;
            }
        } 
    }


}
