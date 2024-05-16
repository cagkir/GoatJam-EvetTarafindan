using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement3D : MonoBehaviour
{

    [SerializeField] public AudioClip pressButton;
    [SerializeField] public AudioClip openShelveDoor;
    [SerializeField] public AudioClip switche;
    [SerializeField] public AudioClip openEscapeDoor;
    [SerializeField] public AudioClip backround;
    [SerializeField] public GameObject elektricSound;
    AudioSource audioSource;
    public Transform cam;
    public float playerActivateDistance;
    bool active = false;
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed;
    public float lookXLimit = 45f;
    [Header("Interactable Objects")]
    [SerializeField] private GameObject table;
    [SerializeField] private GameObject switchE;
    [SerializeField] private GameObject passBox;
    [SerializeField] private GameObject numberBox;
    [SerializeField] private GameObject shelveDoorL;
    [SerializeField] private GameObject shelveDoorR;
    [SerializeField] List<GameObject> switches = new List<GameObject>();
    [SerializeField] List<GameObject> ligths = new List<GameObject>();
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    [SerializeField] private GameObject uiManager;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject soundsManager;
    [SerializeField] private GameObject arrows;
    [SerializeField] private GameObject greenButton;
    [SerializeField] private GameObject redButton;
    [SerializeField] private GameObject redButton1;
    [SerializeField] private GameObject greenButton1;
    [SerializeField] private GameObject buttonNumberPaper;
    [SerializeField] private TextMeshProUGUI sayacText;
    [SerializeField] private TextMeshPro buttonText;

    CharacterController characterController;
    private int keyCount;
    [SerializeField] private LayerMask interactableLayer;
    private float sayacCharacterController;
    [SerializeField] private bool isSwitch;
    [SerializeField] private bool isNumberBox;
    public float buttonSayac;
    public float lightOpenSayac;
    public bool isSayac;
    float sayac1;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        lookSpeed = PlayerPrefs.GetInt("sens");
    }
    void Update()
    {
        if (isSayac == true)
        {
            lightOpenSayac += Time.deltaTime;
        }
        foreach (var item in ligths)
        {
            if (lightOpenSayac > 0.25f)
            {
                item.GetComponent<Light>().enabled = false;
                item.GetComponent<Light>().intensity = 2f;
            }
            if (lightOpenSayac > 0.5f)
            {
                item.GetComponent<Light>().enabled = true;
                item.GetComponent<Light>().intensity = 2.4f;
            }
            if (lightOpenSayac > 0.75f)
            {
                item.GetComponent<Light>().enabled = false;
                item.GetComponent<Light>().intensity = 2.8f;
            }
            if (lightOpenSayac > 1)
            {
                item.GetComponent<Light>().intensity = 3f;
                item.GetComponent<Light>().enabled = true;

            }
        }
        switchControl();
        InteractWithShelveDoorL();
        InteractWithShelveDoorR();
        switchControl();
        InteractWithTable();
        InteractableText();
        if (arrows.activeSelf)
        {
            foreach (var item in switches)
            {
                item.gameObject.tag = "Ýnteractable";
                item.gameObject.layer = 6;
            }
            InteractWithSwitch();
        }
        if (isSwitch == true)
        {
            sayac1 += Time.deltaTime;
            greenButton1.SetActive(true);
            redButton1.SetActive(false);
            isSayac = true;
            elektricSound.SetActive(true);
            Debug.Log("Switch is true");
            numberBox.gameObject.tag = "Ýnteractable";
            numberBox.gameObject.layer = 6;
            InteractWithNumberBox();
        }
        if (sayac1 > 1f)
        {
            elektricSound.SetActive(false);
        }
            if (isNumberBox && buttonSayac <7)
        {
            greenButton.gameObject.tag = "Ýnteractable";
            greenButton.gameObject.layer = 6;
            InteractableButton();
        }
        if (buttonSayac >= 7)
        {
            //Kapý açýlma ve karakter uçma animasyonu
        }
        InteractWithBox();


    }
    private void FixedUpdate()
    {
        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        //if (!characterController.isGrounded)
        //{
        //    moveDirection.y -= gravity * Time.deltaTime;
        //}

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion
        //sayacCharacterController += Time.deltaTime;
        //if (sayacCharacterController > 0.1f)
        //{
        //    gameObject.GetComponent<PlayerMovement3D>().enabled = true;
        //}
    }
    private void InteractWithTable() // Tablonun etkileþimi
    {
        RaycastHit hit;
        active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, playerActivateDistance, interactableLayer);

        if (Input.GetKeyDown(KeyCode.E) && active == true)
        {
            if (hit.collider.gameObject == table)
            {
                soundsManager.GetComponent<SoundsManager>().tableFall.Play();
                table.GetComponent<Animator>().SetTrigger("Intreract");
                Debug.Log("Table Clicked");
            }
        }
    }
    private void InteractWithShelveDoorL() // shelve L door etkileþimi
    {
        RaycastHit hit;
        active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, playerActivateDistance, interactableLayer);

        if (Input.GetKeyDown(KeyCode.E) && active == true)
        {

            if (hit.collider.gameObject == shelveDoorL)
            {
                if (shelveDoorL.GetComponent<Animator>().GetBool("isOpen") == false)
                {
                    soundsManager.GetComponent<SoundsManager>().openShelveDoor.Play();
                    shelveDoorL.GetComponent<Animator>().SetBool("isOpen", true);
                    shelveDoorL.GetComponent<Animator>().SetTrigger("Open");
                }
                else
                {
                    soundsManager.GetComponent<SoundsManager>().closeShelveDoor.Play();
                    shelveDoorL.GetComponent<Animator>().SetBool("isOpen", false);
                    shelveDoorL.GetComponent<Animator>().SetTrigger("Close");
                }
            }
        }
    }
    private void InteractWithShelveDoorR() // shelve R door etkileþimi
    {
        RaycastHit hit;
        active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, playerActivateDistance, interactableLayer);

        if (Input.GetKeyDown(KeyCode.E) && active == true)
        {

            if (hit.collider.gameObject == shelveDoorR)
            {
                if (shelveDoorR.GetComponent<Animator>().GetBool("isOpen") == false)
                {
                    soundsManager.GetComponent<SoundsManager>().openShelveDoor.Play();
                    shelveDoorR.GetComponent<Animator>().SetBool("isOpen", true);
                    shelveDoorR.GetComponent<Animator>().SetTrigger("Open");
                }
                else
                {
                    soundsManager.GetComponent<SoundsManager>().closeShelveDoor.Play();
                    shelveDoorR.GetComponent<Animator>().SetBool("isOpen", false);
                    shelveDoorR.GetComponent<Animator>().SetTrigger("Close");
                }
            }
        }
    }
    private void InteractWithSwitch() // Þartel etkileþimi
    {
        RaycastHit hit;
        active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, playerActivateDistance, interactableLayer);

        if (Input.GetKeyDown(KeyCode.E) && active == true)
        {
            foreach (var item in switches)
            {
                if (hit.collider.gameObject == item)
                {
                    if (item.GetComponent<Animator>().GetBool("isOpen") == false)
                    {

                        item.GetComponent<Animator>().SetBool("isOpen", true);
                        item.GetComponent<Animator>().SetTrigger("Open");
                        Debug.Log("Switch On");
                    }
                    else
                    {
                        item.GetComponent<Animator>().SetBool("isOpen", false);
                        item.GetComponent<Animator>().SetTrigger("Close");
                        Debug.Log("Switch Of");
                    }
                }
            }
        }
    }
    private void InteractableText() // Etkileþim textleri
    {
        RaycastHit hit;
        active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, playerActivateDistance, interactableLayer);

        if (active == true)
        {
            uiManager.GetComponent<UIManager>().interactableText.gameObject.SetActive(true);
        }
        else
        {
            uiManager.GetComponent<UIManager>().interactableText.gameObject.SetActive(false);
        }
    }
    private void InteractWithBox() // Tablonun etkileþimi
    {
        RaycastHit hit;
        active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, playerActivateDistance, interactableLayer);

        if (Input.GetKeyDown(KeyCode.E) && active == true)
        {
            if (hit.collider.gameObject == passBox)
            {
                soundsManager.GetComponent<SoundsManager>().openShelveDoor.Play();
                gameManager.GetComponent<GameManager>().OpenPasscodeScreen();
            }
        }
    }
    private void InteractWithNumberBox() // Number Box etkileþimi
    {
        RaycastHit hit;
        active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, playerActivateDistance, interactableLayer);

        if (Input.GetKeyDown(KeyCode.E) && active == true)
        {
            if (hit.collider.gameObject == numberBox)
            {
                soundsManager.GetComponent<SoundsManager>().openShelveDoor.Play();
                Debug.Log("Number Box Clicked");
                numberBox.GetComponent<Animator>().SetTrigger("interact");
                redButton.SetActive(false);
                greenButton.SetActive(true);
                isNumberBox = true;
            }
        }
    }
    private void InteractableButton() // Button Sayacý
    {
        RaycastHit hit;
        active = Physics.Raycast(cam.position, cam.TransformDirection(Vector3.forward), out hit, playerActivateDistance, interactableLayer);

        if (active == true && hit.collider.gameObject == greenButton && Input.GetKeyDown(KeyCode.E) && isSwitch)
        {
            soundsManager.GetComponent<SoundsManager>().pressButton.Play();
            greenButton.GetComponent<Animator>().SetTrigger("click");
            buttonSayac++;
            Debug.Log(buttonSayac + " button sayaç");
            buttonText.text = buttonSayac.ToString();
        }
    }
    public void switchControl()
    {
        if (switches[0].GetComponent<Animator>().GetBool("isOpen") == false && switches[1].GetComponent<Animator>().GetBool("isOpen") == true &&
    switches[2].GetComponent<Animator>().GetBool("isOpen") == true)
        {
            isSwitch = true;
        }
    }

}
