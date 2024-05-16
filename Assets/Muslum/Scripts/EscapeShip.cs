using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeShip : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainCamera;
    Animator anim;
    [SerializeField] private float timer;
    bool isTimer;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isTimer)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                SceneManager.LoadScene("Cagdas");
            }
        }
        if (player.GetComponent<PlayerMovement3D>().buttonSayac >= 7)
        {
            anim.SetTrigger("open");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && player.GetComponent<PlayerMovement3D>().buttonSayac >= 7)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<PlayerMovement3D>().enabled = false;
            mainCamera.GetComponent<Animator>().enabled = true;
            mainCamera.GetComponent<Animator>().SetTrigger("escape");
            isTimer = true;
        }
    }

}
