using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using GG.Infrastructure.Utils.Swipe;
using UnityEngine.SceneManagement;


public class playerController : MonoBehaviour
{
    //CameraShake cameraShake;

    //swipe
    [SerializeField] private SwipeListener swipeListener;
    [SerializeField] private Transform playerTransform;

    //playerspeed/direc
    [SerializeField] private float playerSpeed;
    private Vector3 playerDirection = Vector3.zero;

    // CanMove/NbMovement
    [SerializeField] private bool canMove = true;
    [SerializeField] private int nbMovement = 5;
    
    //particles system
    [SerializeField] ParticleSystem collectParticle = null;




    // -- Direction du joueur SWIPE (up;down;left;right)
    private void OnEnable()
    {
        swipeListener.OnSwipe.AddListener(OnSwipe);
    }

    // up;down;left;right movement
    private void OnSwipe(string swipe)
    {

        Debug.Log(swipe);
        if (canMove == true && nbMovement > -1)
        {
            switch (swipe)
            {
                case "Right":
                    playerDirection = Vector3.right;
                    nbMovement -= 1;
                    break;

                case "Left":
                    playerDirection = Vector3.left;
                    nbMovement -= 1;
                    break;

                case "Up":
                    playerDirection = Vector3.up;
                    nbMovement -= 1;
                    break;

                case "Down":
                    playerDirection = Vector3.down;
                    nbMovement -= 1;
                    break;
            }
        }

        if (nbMovement == -1)
        {
            SceneManager.LoadScene("GameOver");

        }

    }

    private void OnDisable()
    {
        swipeListener.OnSwipe.RemoveListener(OnSwipe);
    }

    void Update()
    {

            playerTransform.position += (Vector3)playerDirection * playerSpeed * Time.deltaTime;
    

    }



    // --- Detection collision (PLAYER/WALL)
    private void OnCollisionEnter(Collision collision)
    {
        // si collision avec tag "Wall" le player ne peut plus bouger
        if (collision.gameObject.CompareTag("Wall"))
        {
            //Instantiate(gameWallCollision, collision.GetContact(0).point);)
            Collect();
            canMove = true;
            playerDirection=Vector3.zero;
            //StartCoroutine(Shaking());
            Debug.Log("player/wall ON");

        }
    }


    // En sortant de la collision, le player peut bouger
    private void OnCollisionExit(Collision collision)
    {
        canMove = false;
        Debug.Log("player/wall OFF");

    }

    public void Collect()
    {
        // VFX
        collectParticle.Play();
       // SFX

    }

}
