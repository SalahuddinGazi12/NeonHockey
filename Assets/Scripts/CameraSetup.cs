using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CameraSetup : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
   public GameObject player_scoretext1;
   public GameObject player_scoretext2;
    [SerializeField] GameObject gameoverpanel;
    [SerializeField] GameObject gameoverpanel2;


    public void SetupCamera()
    {
        
            FlipCamera();
        
    }
    public void setuppanel1()
    {
        flippnael1();
    }
    public void setuppanel2()
    {
        flippnael2();
    }
    private void FlipCamera()
    {
        //mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -mainCamera.transform.position.z);
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 180f);

        player_scoretext1.transform.rotation = Quaternion.Euler(0, 0, 180f);
          player_scoretext2.transform.rotation = Quaternion.Euler(0, 0, 0f);
        // gameoverpanel.transform.rotation = Quaternion.Euler(0, 0, 180f);
    }

    void flippnael1()
    {
        gameoverpanel.transform.rotation = Quaternion.Euler(0, 0, 180f);
        gameoverpanel.transform.GetChild(4).transform.rotation = Quaternion.Euler(0, 0, 180f);
    }
    void flippnael2()
    {
        gameoverpanel2.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameoverpanel2.transform.GetChild(4).transform.rotation = Quaternion.Euler(0, 0, 0);
    }

   

}