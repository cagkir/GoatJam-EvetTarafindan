using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject passcodeScreen;
    [SerializeField] private GameObject room3D;

    private void Update()
    {
        if (passcodeScreen.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                openRoom3D();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            SceneManager.LoadScene(1);
        }
    }
    public void OpenPasscodeScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        passcodeScreen.SetActive(true);
        room3D.SetActive(false);
    }
    public void openRoom3D()
    {
        Cursor.lockState = CursorLockMode.Locked;
        passcodeScreen.SetActive(false);
        room3D.SetActive(true);
    }
}
