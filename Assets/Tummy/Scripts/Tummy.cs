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
        private float mouthClosedValue = 0;

        private AudioSource aSource;
        public AudioClip fail;
        public AudioClip gulp;

        // Use this for initialization
        void Start()
        {
            aSource = gameObject.GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

            // Reset game:
            if (Input.GetKeyDown(KeyCode.Space)){
                score = 0;
                life = 3;
                livesText.SetText("Lives: " + life.ToString());
                scoreText.SetText("Score: " + score.ToString());
                Time.timeScale = 1;
            }

            if (life == 0)
            {
                print("Game over!");
                livesText.SetText("Game over!");
                Time.timeScale = 0;
            }

            if (stomachBody.GetComponent<SkinnedMeshRenderer>().GetBlendShapeWeight(2) > 5)
            {
                mouthOpen = true;
            }
            else
            {
                mouthOpen = false;
            }
            print(mouthOpen);
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Good")
            {
                collidedFood = "Good";
            }
            if (col.tag == "Bad")
            {
                collidedFood = "Bad";
            }

            // MOUF LOGIC

            //eat good food
            if (mouthOpen && collidedFood == "Good" || Input.GetKeyDown(KeyCode.UpArrow))
            {
                score++;
                scoreText.SetText("Score: " + score.ToString());
                col.gameObject.SetActive(false);
                //PLAY GOOD SOUND
                aSource.clip = gulp;
                aSource.Play();
            }
            //eat bad food
            if (mouthOpen && collidedFood == "Bad" || Input.GetKeyDown(KeyCode.DownArrow))
            {
                life--;
                livesText.SetText("Lives: " + life.ToString());
                col.gameObject.SetActive(false);
                // PLAY BAD SOUND
                aSource.clip = fail;
                aSource.Play();
            }
            if (mouthOpen == false && collidedFood == "Good")
            {
                life--;
                livesText.SetText("Lives: " + life.ToString());
                // PLAY BAD SOUND
            }
            if (mouthOpen == false && collidedFood == "Bad")
            {
            }

            collidedFood = "";
        }
    }
}
