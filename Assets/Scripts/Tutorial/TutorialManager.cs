using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex = 0;
    public ItemJumpCard itemJumpCard;
    bool collide = false;

    // Start is called before the first frame update
    void Start()
    {
        itemJumpCard.collideEvent += checkCollide;
    } 

    // Update is called once per frame
    void Update()
    {
        for (int i = 2; i < popUps.Length; i++)
        {
            if (i == popUpIndex){
                popUps[i].SetActive(true);
            } else {
                popUps[i].SetActive(false);
            }
        }
        if(popUpIndex == 0){
            if (PlayerController.instance.isMovingRight == true){
                popUps[0].SetActive(false);
                popUps[1].SetActive(false);
                popUpIndex++;
                popUpIndex++;
            }
        } //else if (popUpIndex == 1){
        //     if(Input.GetKeyDown(KeyCode.Tab)){
        //         popUps[1].SetActive(false);
        //         popUpIndex++;
        //     }
        // } 
        else if (popUpIndex == 2){
            if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.D)){
                popUpIndex++;
                popUpIndex++;
            }
        } /*else if (popUpIndex == 3){
            if(Input.GetKeyDown(KeyCode.Space)){
                popUpIndex++;
            }
        }*/ else if (popUpIndex == 4){
            if(collide){
                popUpIndex++;
            }
        } else if (popUpIndex == 5){
            if(hintpoint.instance.check == true){
                popUpIndex++;
            }
        } else if (popUpIndex == 6){
            if(hintpoint2.instance.check2 == true){
                popUpIndex++;
            }
        } 
    }

    void checkCollide()
    {
        collide = true;
    }
}
