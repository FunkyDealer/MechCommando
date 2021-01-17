using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector]
    public GameManager manager;
    [SerializeField]
    string MenuToQuitTo;

    [SerializeField]
    GameObject OptionsMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Pause"))
        {
            ResumeGame();
        }

    }
    

    public void ResumeGame()
    {
        manager.unPauseGame();
        Destroy(gameObject);
        
    }

    public void Quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(MenuToQuitTo);
    }

    public void GoToOptions()
    {
       GameObject o = Instantiate(OptionsMenu);
        HudOptionsManager h = o.GetComponent<HudOptionsManager>();
        h.pauseMenu = this;

        gameObject.SetActive(false);
    }

    public void BackToPauseMenu()
    {
        gameObject.SetActive(true);
    }
}
