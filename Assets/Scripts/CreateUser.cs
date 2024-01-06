using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class CreateUser : MonoBehaviour
{
    public TMP_InputField usernametxt;
    int t;
    private void Awake()
    {
       
    }
    private void Start()
    {
        t = 0;
        PlayerPrefs.SetInt("usernamestate", PlayerPrefs.GetInt("usernamestate", 0));
        if (PlayerPrefs.GetInt("usernamestate") == 2)
        {
            SceneManager.LoadScene(1);
        }
       
    }
    public void create_user()
    {
        PlayerPrefs.SetString("username", usernametxt.text);
        AudioManager.instance.Play("Button");
        if (usernametxt.text != "")
        {
            
            t = 1;
            PlayerPrefs.SetInt("usernamestate", PlayerPrefs.GetInt("usernamestate")+1);
            
           
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("usernamestate") == 1 || t == 1)
        {

            SceneManager.LoadScene(1);
            PlayerPrefs.SetInt(("usernamestate"), 2);
        }
    }
}
