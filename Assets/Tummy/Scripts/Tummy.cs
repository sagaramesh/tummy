using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro.Examples
{

    public class Tummy : MonoBehaviour
    {

        // USE: Attach to stomach model and ensure that GameObject has a collider and rigidbody attached. 
        // Make sure good food prefabs are tagged "Good" and bad food prefabs are tagged "Bad" (case-sensitive).
        // Make sure good and bad food models have colliders attached to them, too. 

        private int score;
        [SerializeField]
        private int life = 3;
        private bool mouthOpen;
        private string collidedFood;

        [SerializeField]
        private GameObject stomachBody;
        [SerializeField]
        private TextMeshPro livesText;
        [SerializeField]
        private TextMeshPro scoreText;
        [SerializeField]
        private float mouthClosedValue = 80.0f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (life == 0)
            {
                print("Game over!");
                livesText.SetText("Game over!");
                Time.timeScale = 0;
            }

            if (stomachBody.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(5) >= mouthClosedValue)
            {
                mouthOpen = false;
            }
            else
            {
                mouthOpen = true;
            }

            CheckCollision();
        }

        void CheckCollision()
        {

            if (mouthOpen && collidedFood == "Good" || Input.GetKeyDown(KeyCode.UpArrow))
            {
                score++;
                scoreText.SetText("Score: " + score.ToString());
                print("Score: " + score);
            }
            if (mouthOpen && collidedFood == "Bad" || Input.GetKeyDown(KeyCode.DownArrow))
            {
                life--;
                livesText.SetText("Lives: " + life.ToString());
                print("Life: " + life);
            }
            if (mouthOpen == false && collidedFood == "Good")
            {
                life--;
                livesText.SetText("Lives: " + life.ToString());
                print("Life: " + life);
            }
            if (mouthOpen == false && collidedFood == "Bad")
            {
                print("Continue game.");
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Good")
            {
                collidedFood = "Good";
                CheckCollision();
            }
            if (col.tag == "Bad")
            {
                collidedFood = "Bad";
                CheckCollision();
            }
        }
    }
}
