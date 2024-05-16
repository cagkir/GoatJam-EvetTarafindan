using Pathfinding;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Manager : MonoBehaviour
{

    [SerializeField] GameObject[] bubbles;
    [SerializeField] GameObject player, spaceship, player2d, squarestart, canvas, won1,won2;
    [SerializeField] float oyunsüresi, kalansüre;
    [SerializeField] Boolean uc = false, konusmakontrol1  = true, konusmakontrol2 = false, konusmakontrol3;
    [SerializeField] TextMeshPro textMeshProUGUI, konusmalar;
    float balonpatlatsürefarkı;
    float sayac = 0, konusmasayac, wonsayac = 0;
    Boolean wonkontrol = false;
    public Boolean süresay;

    [SerializeField] GameObject[] aktifedilecekler;

    private void Awake()
    {
        squarestart.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        kalansüre = oyunsüresi;
        
        balonpatlatsürefarkı = oyunsüresi / bubbles.Length;

        if(PlayerPrefs.GetInt("music") == 0)
        {
            GetComponent<AudioSource>().volume = 0f;
        }
        else
        {
            GetComponent<AudioSource>().volume = 0.05f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(konusmasayac >= 3)
        {
            konusmalar.text = "";
        }
        if(konusmasayac >= 3.5f)
        {
            konusmalar.text = "Roketi çalıştırıp kaçmak için bir an önce tüm yakıtları toplamalıyım!";
        }
        if(konusmasayac >= 8)
        {
            konusmalar.text = "";
        }
        if(konusmasayac >= 8.5f)
        {
            for (int i = 0; i < aktifedilecekler.Length; i++)
            {
                aktifedilecekler[i].SetActive(true);
            }
            player2d.GetComponent<Move2D>().enabled = true;
            süresay = true;
            konusmalar.text = "Hızlı ol! Oksijenimin yeterli olacağından emin değilim.";
            
        }
        if(konusmasayac >= 15)
        {
            konusmalar.text = "";
            
        }
        if(konusmasayac >= 15.5f)
        {
            konusmalar.text = "Buzul dikit ve sarkıtlara dikkat et! Aynı zaman da meteorlara da!";
        }
        if(konusmasayac >= 20)
        {
            konusmalar.text = "";
            
        }
        if(konusmasayac >= 20.5f)
        {
            konusmakontrol1 = false;
            konusmasayac = 0;
        }

        if(player2d.GetComponent<Move2D>().toplanancoin == 6)
        {
            konusmalar.text = "Tüm yakıtları topladın. Derhal uzay gemisine git!";
        }

        if(kalansüre < 0)
        {
            süresay = false;
            textMeshProUGUI.text = "";
            player2d.GetComponent<Move2D>().enabled = false;
            player.GetComponent<Animator>().SetBool("die", true);
            aktifedilecekler[0].GetComponent<AIPath>().enabled = false;
            aktifedilecekler[1].GetComponent<AIPath>().enabled = false;
            canvas.SetActive (true);
            Cursor.lockState = CursorLockMode.None;
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            for (int i = 0; i < cam.transform.childCount; i++)
            {
                cam.transform.GetChild(i).gameObject.SetActive(false);
            }
            Cursor.visible = false;
        }
        if(sayac > 1)
        {

            

            player2d.transform.SetParent(spaceship.transform);
            player2d.transform.GetComponent<Rigidbody2D>().gravityScale = 0;
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            cam.GetComponent<Camfollow2D>().flyaway = true;
            spaceship.GetComponent<SpaceShip>().flyaway = true;

            for (int i = 0; i < cam.transform.childCount; i++)
            {
                cam.transform.GetChild(i).gameObject.SetActive(false);
            }

            if (cam.GetComponent<Camera>().orthographicSize < 6f)
            {
                cam.GetComponent<Camera>().orthographicSize += 0.05f;
            }
            if (cam.GetComponent<Camera>().orthographicSize >= 6f)
            {
                sayac = 0;
            }
            
            spaceship.GetComponent<Animator>().enabled = true;
            uc = false;
            
        }

        for(int i = 1; i < bubbles.Length + 1; i++)
        {
            if(kalansüre < oyunsüresi - i * balonpatlatsürefarkı)
            {
                bubbles[bubbles.Length - i].SetActive(false);
            }
        }

        if(wonsayac > 7)
        {
            getwonscreen();
            Cursor.visible = true;
        }

    }

    private void FixedUpdate()
    {
        if (süresay)
        {
            kalansüre -= Time.deltaTime;
            textMeshProUGUI.text = " : " + ((int)kalansüre);
        }
        if (uc)
        {
            sayac += Time.deltaTime;
        }
        if (konusmakontrol1)
        {
            konusmasayac += Time.deltaTime;
        }
        if (wonkontrol)
        {
            wonsayac += Time.deltaTime;
        }

    }

    public void GetInSpaceShip()
    {
        player.GetComponent<Animator>().SetBool("space", true);
        uc = true;
        wonkontrol = true;
    }

    void getwonscreen()
    {
        won1.SetActive(true);
        won2.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void gomainmenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
