using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Events;
using System;
using UnityEngine.Rendering.Universal;
using System.Diagnostics;

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
        private PlayGround playGround;

        // The nearest flower to the agent
        private Coin nearestGoodCoin;

        // The nearest flower to the agent
        private Coin nearestBadCoin;

        // The nearest flower to the agent
        private Jumping nearestEnemy;

        // Start is called before the first frame update
        void Start()
        {
            playGround = GetComponentInParent<PlayGround>(); // move to Initialize
            rb = GetComponent<Rigidbody>();
            winTextObject.enabled = false;
            count = 0;
            highscore = PlayerPrefs.GetInt("highscore", 0);
            SetCountText();
            //PlayerPrefs.SetInt("highscore", 0);
            UpdateNearestGoodCoin();
            UpdateNearestBadCoin();
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

            UpdateNearestEnemy();
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
                    OnCollectBadCoin.Invoke();
                    UpdateNearestBadCoin();
                }
                else
                {
                    UpdateNearestGoodCoin();
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

        /// <summary>
        /// Update the nearest flower to the agent
        /// </summary>
        private void UpdateNearestGoodCoin()
        {
            foreach (Coin coin in playGround.GoodCoins)
            {
                if (nearestGoodCoin == null)
                {
                    // No current nearest flower and this flower has nectar, so set to this flower
                    nearestGoodCoin = coin;
                }
                else
                {
                    // Calculate distance to this flower and distance to the current nearest flower
                    float distanceToCoin = Vector3.Distance(coin.transform.position, transform.position);
                    float distanceToCurrentNearestCoin = Vector3.Distance(nearestGoodCoin.transform.position, transform.position);

                    // If current nearest flower is empty OR this flower is closer, update the nearest flower
                    if (distanceToCoin < distanceToCurrentNearestCoin)
                    {
                        nearestGoodCoin = coin;
                    }
                }
            }
        }

        private void UpdateNearestBadCoin()
        {
            foreach (Coin coin in playGround.BadCoins)
            {
                if (nearestBadCoin == null)
                {
                    // No current nearest flower and this flower has nectar, so set to this flower
                    nearestBadCoin = coin;
                }
                else
                {
                    // Calculate distance to this flower and distance to the current nearest flower
                    float distanceToCoin = Vector3.Distance(coin.transform.position, transform.position);
                    float distanceToCurrentNearestCoin = Vector3.Distance(nearestBadCoin.transform.position, transform.position);

                    // If current nearest flower is empty OR this flower is closer, update the nearest flower
                    if (distanceToCoin < distanceToCurrentNearestCoin)
                    {
                        nearestBadCoin = coin;
                    }
                }
            }
        }

        private void UpdateNearestEnemy()
        {
            foreach (Jumping enemy in playGround.Enemies)
            {
                if (nearestEnemy == null)
                {
                    // No current nearest flower and this flower has nectar, so set to this flower
                    nearestEnemy = enemy;
                }
                else
                {
                    // Calculate distance to this flower and distance to the current nearest flower
                    float distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
                    float distanceToCurrentNearestEnemy = Vector3.Distance(nearestEnemy.transform.position, transform.position);

                    // If current nearest flower is empty OR this flower is closer, update the nearest flower
                    if (distanceToEnemy < distanceToCurrentNearestEnemy)
                    {
                        nearestEnemy = enemy;
                    }
                }
            }
        }

        //public override void Initialize()
        //{
        //    rigidbody = GetComponent<Rigidbody>();
        //    playGround = GetComponentInParent<PlayGround>();

        //    // If not training mode, no max step, play forever
        //    if (!trainingMode) MaxStep = 0;
        //}

        private void Update()
        {
            // Draw a line from the beak tip to the nearest flower
            if (nearestEnemy != null)
            {
                UnityEngine.Debug.DrawLine(transform.position, nearestEnemy.transform.position, Color.red);
            }
                
            if (nearestBadCoin != null)
            {
                UnityEngine.Debug.DrawLine(transform.position, nearestBadCoin.transform.position, Color.green);
            }

            if (nearestGoodCoin != null)
            {
                UnityEngine.Debug.DrawLine(transform.position, nearestGoodCoin.transform.position, Color.blue);
            }
        }
    }

}
