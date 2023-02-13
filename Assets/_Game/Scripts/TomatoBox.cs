using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoBox : MonoBehaviour
{
    public GameObject tomatoPrefab=null;

    private PlayerController playerController=null;
    

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals("Player")){
            playerController=other.gameObject.GetComponent<PlayerController>();
            playerController.TriggerHoldingAnim();//trigger the holding animation in player
            GameObject newTomato=Instantiate(tomatoPrefab,playerController.placeForIngredient.transform.position,Quaternion.identity);//place the tomato in his hands in a predetermined place
            newTomato.transform.SetParent(playerController.placeForIngredient.transform);
        }
    }
}
