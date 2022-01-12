using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManagerScript : MonoBehaviour
{
    // Game Objects
    public GameObject posMessage; // Player Position Textbox
    public GameObject mainMessage; // Output Textbox
    public GameObject diamondMessage; // Collected Diamonds Textbox

    // Audio Sources
    public AudioSource step;
    public AudioSource hitWall;
    public AudioSource foundNothing;
    public AudioSource collect;
    public AudioSource radar;

    bool checkOn = false; // Check Toggle
    Vector2 playerPos; // Player Position
    Vector2 exit; // Exit Position

    // Boundary Dimensions
    public int width, height;

    // Score
    static int moves;

    // DiamondFields
    Vector2[] diamonds = new Vector2[5];
    int collectedDiamonds;


    // Start is called before the first frame update
    void Start()
    {
        // Initializing Variables
        moves = 0;
        collectedDiamonds = 0;
        playerPos = new Vector2(0, 0);
        exit = new Vector2(0, 0);

        diamonds[0] = new Vector2(0, 5);
        diamonds[1] = new Vector2(-2, 3);
        diamonds[2] = new Vector2(4, -4);
        diamonds[3] = new Vector2(-7, -8);
        diamonds[4] = new Vector2(2, 1);

        print("Hello World!");
    }

    // Update is called once per frame
    void Update()
    {
        // When the player collects all the diamonds
        if (collectedDiamonds == diamonds.Length)
        {
            diamondMessage.GetComponent<Text>().text = "You collected all the diamonds! Now get to the exit.";

            if (playerPos.Equals(exit))
            {
                radar.Stop();
                SceneManager.LoadScene("AdventureEnd");
            }
        }

        float volume;
        // Adjusting volume of the radar sound based on distance from diamonds
        if (collectedDiamonds < diamonds.Length)
        {
            volume = .25f;
            foreach (Vector2 x in diamonds)
            {
                if (Vector2.Distance(playerPos, x) <= 2)
                {
                    if (Vector2.Distance(playerPos, x) == 1)
                        volume = 1f;
                    else
                        volume = Math.Max(volume, .5f);
                }
            }
        }
        // Adjusting volume of the radar sound based on distance from exit
        else
        {
            if (Vector2.Distance(playerPos, exit) >= 2)
            {
                volume = .25f;
            }
            else
            {
                if (Vector2.Distance(playerPos, exit) > 1)
                    volume = .5f;
                else
                    volume = 1f;
            }
        }
        radar.volume = volume;

        // Main Menu Keybind
        if (Input.GetKeyDown("escape"))
            LoadAScene.ReturnToMain();
    }

    public static int getScore()
    {
        return moves;
    }

    public void setDiamondMessage()
    {
        // Change diamondMessage
        diamondMessage.GetComponent<Text>().text = "Diamonds Collected: " + collectedDiamonds + "/" + diamonds.Length;
    }

    public void setPositionMessage()
    {
        // Change posMessage
        posMessage.GetComponent<Text>().text = "Position: (" + playerPos.x + ", " + playerPos.y + ")";
        print(playerPos.x + " " + playerPos.y);
    }

    public void setMainMessage(String message)
    {
        // Change mainMessage
        mainMessage.GetComponent<Text>().text = message;
    }

    public void checkToggle(bool state)
    {
        checkOn = state;
    }

    public void button(int ordinal)
    {
        moves++;

        if (checkOn == false) // moving the player
            switch (ordinal) // checking which button is pressed
            {
                case 0:
                    if (playerPos.y < height) // preventing the player from leaving boundaries
                    {
                        playerPos.y++;
                        step.Play();
                        setPositionMessage();
                        setMainMessage("You moved up.");
                    }
                    else
                    {
                        setMainMessage("You bumped into a wall.");
                        hitWall.Play();
                    }
                    break;
                case 1:
                    if (playerPos.x < width)
                    {
                        playerPos.x++;
                        step.Play();
                        setPositionMessage();
                        setMainMessage("You moved right.");
                    }
                    else
                    {
                        setMainMessage("You bumped into a wall.");
                        hitWall.Play();
                    }
                    break;
                case 2:
                    if (playerPos.y > -height)
                    {
                        playerPos.y--;
                        step.Play();
                        setPositionMessage();
                        setMainMessage("You moved down.");
                    }
                    else
                    {
                        setMainMessage("You bumped into a wall.");
                        hitWall.Play();
                    }
                    break;
                case 3:
                    if (playerPos.x > -width)
                    {
                        playerPos.x--;
                        step.Play();
                        setPositionMessage();
                        setMainMessage("You moved left.");
                    }
                    else
                    {
                        setMainMessage("You bumped into a wall.");
                        hitWall.Play();
                    }
                    break;
                default:
                    break;
            }
        else // checking a tile next to the player
            switch (ordinal) // checking which button is pressed
            {
                case 0:
                    playerPos.y++;
                    for (int x = 0; x < diamonds.Length; x++)
                    {
                        if (playerPos.Equals(diamonds[x]))
                        {
                            collectedDiamonds++;
                            diamonds[x] = new Vector2(width + 1, height + 1);
                            collect.Play();
                            setDiamondMessage();
                            setMainMessage("You found a diamond!");
                            break;
                        }
                        else if (playerPos.y == height)
                        {
                            foundNothing.Play();
                            setMainMessage("You stare at the wall, and the wall stares back at you.");
                        }
                        else
                        {
                            setMainMessage("You found nothing.");
                            foundNothing.Play();
                        }

                    }
                    playerPos.y--;
                    break;
                case 1:
                    playerPos.x++;
                    for (int x = 0; x < diamonds.Length; x++)
                    {
                        if (playerPos.Equals(diamonds[x]))
                        {
                            collectedDiamonds++;
                            diamonds[x] = new Vector2(width + 1, height + 1);
                            collect.Play();
                            setDiamondMessage();
                            setMainMessage("You found a diamond!");
                            break;
                        }
                        else if (playerPos.x == width)
                        {
                            setMainMessage("You stare at the wall, and the wall stares back at you.");
                            foundNothing.Play();
                        }
                        else
                        {
                            setMainMessage("You found nothing.");
                            foundNothing.Play();
                        }

                    }
                    playerPos.x--;
                    break;
                case 2:
                    playerPos.y--;
                    for (int x = 0; x < diamonds.Length; x++)
                    {
                        if (playerPos.Equals(diamonds[x]))
                        {
                            collectedDiamonds++;
                            diamonds[x] = new Vector2(width + 1, height + 1);
                            collect.Play();
                            setDiamondMessage();
                            setMainMessage("You found a diamond!");
                            break;
                        }
                        else if (playerPos.y == -height)
                        {
                            setMainMessage("You stare at the wall, and the wall stares back at you.");
                            foundNothing.Play();
                        }
                        else
                        {
                            setMainMessage("You found nothing.");
                            foundNothing.Play();
                        }

                    }
                    playerPos.y++;
                    break;
                case 3:
                    playerPos.x--;
                    for (int x = 0; x < diamonds.Length; x++)
                    {
                        if (playerPos.Equals(diamonds[x]))
                        {
                            collectedDiamonds++;
                            diamonds[x] = new Vector2(width + 1, height + 1);
                            collect.Play();
                            setDiamondMessage();
                            setMainMessage("You found a diamond!");
                            break;
                        }
                        else if (playerPos.x == -width)
                        {
                            setMainMessage("You stare at the wall, and the wall stares back at you.");
                            foundNothing.Play();
                        }
                        else
                        {
                            setMainMessage("You found nothing.");
                            foundNothing.Play();
                        }
                    }
                    playerPos.x++;
                    break;
                default:
                    break;
            }
    }


}
