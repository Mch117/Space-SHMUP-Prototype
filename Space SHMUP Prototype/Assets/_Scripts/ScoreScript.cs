using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScript: MonoBehaviour
{
    public Text Highscore;
    
    public static int score;        // The player's score.
    public static int highscore;

   public Text text;                      // Reference to the Text component.

   
   public static ScoreScript S;

   void Awake ()
   {
       // Set up the reference.
       //text = GetComponent <Text> ();

       // Reset the score.
       score = 0;
       S = this;
       highscore = PlayerPrefs.GetInt("highscore", highscore);
       Highscore.text = "High Score: " + score;

   }

   public void UpdateScore()
   {
       score++;
       text.text = "Score: " + score;
   }

   void Update ()
   {
       if (score > highscore)
       {
           highscore = score;
           Highscore.text ="High Score: " + highscore;
           PlayerPrefs.SetInt("highscore", highscore);
       }
       // Set the displayed text to be the word "Score" followed by the score value.
       //text.text = "Score: " + score;
   }


}
