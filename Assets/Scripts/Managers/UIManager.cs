using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMenuType
{
    MainMenu = 0,
    GameMenu
}


public class UIManager : MonoBehaviour
{

    #region singleton
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public GameObject gameMenu;
    public GameObject mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchMenu(EMenuType menu)
    {
        switch(menu)
        {
            case EMenuType.MainMenu:
                {
                    gameMenu.SetActive(false);
                    mainMenu.SetActive(true);
                    break;
                }
            case EMenuType.GameMenu:
                {
                    gameMenu.SetActive(true);
                    mainMenu.SetActive(false);
                    break;
                }
            default:
                break;

        }
    }

    public void ShowMainMenu()
    {
        SwitchMenu(EMenuType.MainMenu);
    }

    public void ShowGameMenu()
    {
        SwitchMenu(EMenuType.GameMenu);
    }
}
