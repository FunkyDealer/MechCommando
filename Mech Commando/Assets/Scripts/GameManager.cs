using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool pause;
    Player p;
    [SerializeField]
    GameObject pauseMenu;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pause = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        p = transform.parent.gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (p.isAlive() && p.inControl)
        {

            if (Input.GetButtonDown("Pause"))
            {
                if (pause) unPauseGame();
                else { pauseGame(); createPauseMenu(); }
            }

        }



    }

    public void pauseGame()
    {
        if (!pause)
        {
            p.inControl = false;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            pause = true;
        }
    }

    public void unPauseGame()
    {
        if (pause)
        {
            p.inControl = true;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            pause = false;
        }
    }

    private void createPauseMenu()
    {
        GameObject o = Instantiate(pauseMenu, Vector3.zero, Quaternion.identity);
        PauseMenu p = o.GetComponent<PauseMenu>();
        p.manager = this;

    }


}
