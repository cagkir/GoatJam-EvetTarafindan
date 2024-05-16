using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Passcode : MonoBehaviour
{
    string Code = "386";
    string Nr = null;
    int NrIndex = 0;
    string alpha;
    public TextMeshProUGUI UiText = null;
    [SerializeField] GameObject gameManager;
    [SerializeField] private GameObject arrows;
    [SerializeField] private GameObject greenButton;
    [SerializeField] private GameObject redButton;
    [SerializeField] private GameObject backround;
    [SerializeField] AudioSource buttonSound;
    [SerializeField] AudioSource failPassword;
    [SerializeField] AudioSource succesPassword;
    float sayac;
    bool isSayac;
    float sayac2;
    bool isSayac2;

    private void FixedUpdate()
    {
        if (isSayac2)
        {
            sayac2 += Time.deltaTime;
            if (sayac2 > 0.35f)
            {
                Cursor.lockState = CursorLockMode.Locked;
                gameManager.GetComponent<GameManager>().openRoom3D();
                arrows.SetActive(true);
            }
        }
        if (isSayac)
        {
            sayac += Time.deltaTime;
            if (sayac > 0.3f)
            {
                backround.GetComponent<RawImage>().color = Color.black;
                sayac = 0;
                isSayac = false;
            }
        }
    }
    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            CodeFunction("0");
            buttonSound.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            CodeFunction("1");
            buttonSound.Play();

        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            CodeFunction("2");
            buttonSound.Play();

        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            CodeFunction("3");
            buttonSound.Play();

        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            CodeFunction("4");
            buttonSound.Play();

        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            CodeFunction("5");
            buttonSound.Play();

        }
        if (Input.GetKeyDown(KeyCode.Alpha6)    || Input.GetKeyDown(KeyCode.Keypad6))
        {
            CodeFunction("6");
            buttonSound.Play();

        }
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            CodeFunction("7");
            buttonSound.Play();

        }
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            CodeFunction("8");
            buttonSound.Play();

        }
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            CodeFunction("9");
            buttonSound.Play();

        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Delete();

        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Enter();
        }
    }
    public void CodeFunction(string Numbers)
    {
        NrIndex++;
        Nr = Nr + Numbers;
        UiText.text = Nr;

    }
    public void Enter()
    {
        if (Nr == Code)
        {
            succesPassword.Play();
            isSayac2 = true;
        }
        else
        {
            isSayac = true;
            backround.GetComponent<RawImage>().color = Color.red;
            failPassword.Play();
        }
    }
    public void Delete()
    {
        NrIndex++;
        Nr = null;
        UiText.text = Nr;
    }
}