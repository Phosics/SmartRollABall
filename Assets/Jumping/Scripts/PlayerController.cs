using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Events;
using System;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float jumpSpeed;
        public TextMeshProUGUI countText;
        public TextMeshProUGUI highscoreText;
        public TextMeshProUGUI winTextObject;
        public TextMeshProUGUI lossTextObject;
        public UnityEngine.Events.UnityEvent OnLossTrigger;
        public UnityEngine.Events.UnityEvent OnCollectBadCoin;

        private bool isGrounded;
        private int count;
        private int highscore;
        private Vector3 movement;
        private Rigidbody rb;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            winTextObject.enabled = false;
            count = 0;
            highscore = PlayerPrefs.GetInt("highscore", 0);
            SetCountText();
            //PlayerPrefs.SetInt("highscore", 0);
        }

        public void OnMove(InputAction.CallbackContext movementValue)
        {            
            Vector2 movementVector = movementValue.ReadValue<Vector2>();
            movement = new Vector3(movementVector.x, 0.0f, movementVector.y);
        }

        public void OnJump(InputAction.CallbackContext jumpValue)
        {
            if (isGrounded)
            {
                rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
            }
        }

        void SetCountText()
        {
            countText.text = "Count: " + count.ToString();
            highscoreText.text = "highscore: " + highscore.ToString();
        }

        void FixedUpdate()
        {
            rb.AddForce(movement * speed);            
            if (transform.position.y < -2.0f) 
            {
                AudioManager.Play("Win");
                PlayerPrefs.SetInt("highscore", highscore);
                lossTextObject.text = "You loss\nyour score is: " + count.ToString() + "\nhighscore: " + highscore.ToString();
                OnLossTrigger.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin") || other.gameObject.CompareTag("BadCoin"))
            {
                count += 1;
                if (count > highscore)
                {
                    highscore = count;
                }

                SetCountText();
                AudioManager.Play("PickUp");

                if (other.gameObject.CompareTag("BadCoin"))
                {
                    Debug.Log("aaaaaaaa");
                    OnCollectBadCoin.Invoke();
                }
            }           
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
            else if (collision.gameObject.CompareTag("Jumping"))
            {
                rb.AddForce(new Vector3(0, 3 * jumpSpeed, 0), ForceMode.Impulse);
                AudioManager.Play("Jump");
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }
    }

}
