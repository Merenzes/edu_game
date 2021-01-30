using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform movePoint;
    public Vector3 startingPosition;

    public GameObject collisionPanel;
    public GameObject winPanel;

    public LayerMask whatStopsMovement;
    public LayerMask exit; 

    public TMP_InputField interpreterText;
    public TextMeshProUGUI consoleText;

    public List<string> moveList;


    bool completed = false;
    bool colided = false;
    bool unrecognized = false;
    bool canMove = true;
    int lineNumber = 1;
    bool clearConsole = false;

    bool resetPosition = false;
    bool moving = false;


    public void GenerateMovesList()
    {
        StringReader sr = new StringReader(ButtonsActions.inputText);

        string line;

        while ((line = sr.ReadLine()) != null)
        {
            moveList.Add(line);
        }

        // Empty string containing moves
        ButtonsActions.inputText = "";
    }

    public void MovePlayer(string move)
     {
         switch(move)
         {
             case "character.moveNorth()":
                //check if player is in the exit tile
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), .2f, whatStopsMovement))
                {
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, 1f, 0f), .2f, exit) && !completed)
                    {
                        print("congrat");
                        completed = true;
                    }
                    movePoint.position += new Vector3(0f, 1f, 0f);
                }
                else
                {
                    movePoint.position += new Vector3(0f, 1f, 0f);
                    print("collision");
                    colided = true;

                    collisionPanel.SetActive(true);
                }
                break;
            case "character.moveSouth()":
                //check if player is in the exit tile
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .2f, whatStopsMovement))
                {
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, -1f, 0f), .2f, exit) && !completed)
                    {
                        print("congrat");
                        completed = true;
                    }
                    movePoint.position += new Vector3(0f, -1f, 0f);
                }
                else
                {
                    movePoint.position += new Vector3(0f, -1f, 0f);
                    print("collision");
                    colided = true;

                    collisionPanel.SetActive(true);
                }
                break;
            case "character.moveEast()":
                //check if player is in the exit tile
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f, 0f), .2f, whatStopsMovement))
                {
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(1f, 0f, 0f), .2f, exit) && !completed)
                    {
                        print("congrat");
                        completed = true;
                    }
                    movePoint.position += new Vector3(1f, 0f, 0f);
                }
                else
                {
                    movePoint.position += new Vector3(1f, 0f, 0f);
                    print("collision");
                    colided = true;

                    collisionPanel.SetActive(true);
                }
                break;
             case "character.moveWest()":
                //check if player is in the exit tile
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f, 0f), .2f, whatStopsMovement))
                {
                    if (Physics2D.OverlapCircle(movePoint.position + new Vector3(-1f, 0f, 0f), .2f, exit) && !completed)
                    {
                        print("congrat");
                        completed = true;
                    }
                    movePoint.position += new Vector3(-1f, 0f, 0f);
                }
                else
                {
                    movePoint.position += new Vector3(-1f, 0f, 0f);
                    print("collision");
                    colided = true;

                    collisionPanel.SetActive(true);
                }
                break;
            default:
                print("no direction");
                unrecognized = true;
                canMove = false;
                break;
         }
        
    }

    public void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.5f)
        {
            //check if player is in the exit tile
            if (Physics2D.OverlapCircle(transform.position, .2f, exit))
            {
                print("congrat");
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                // Check if player wont go into wall or fall of the ledge
                if (Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, whatStopsMovement))
                {
                    print("obstacleH");
                    collisionPanel.SetActive(true);
                    transform.position = startingPosition;
                }
                else
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }

            }

            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                // Check if player wont go into wall or fall of the ledge
                if (Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, whatStopsMovement))
                {
                    print("obstacleV");
                    collisionPanel.SetActive(true);
                    transform.position = startingPosition;
                }
                else
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
            
            
        }
    }

    IEnumerator movingCorutine()
    {
        if (moving)
            yield break;

        moving = true;
        if (moveList.Count != 0)
        {
            if (transform.position != startingPosition && !completed)
            {
                print("going back");
                transform.position = startingPosition;
                movePoint.position = startingPosition;
                yield return new WaitForSeconds(2f);
            }
            else
            {
                print(moveList.Count);
                for (int i = 0; i<moveList.Count; i++)
                {
                    string move = moveList[i];
                    print("Completed: " + completed);
                    print("Colided: " + colided);
                    print("Move: " + move);
                    if (completed)
                    {
                        print("to much moves");
                        break;
                    }
                    else if (colided)
                    {
                        print("colisions happen");
                        break;
                    }
                    else
                    {
                        yield return new WaitForSeconds(1f);
                        MovePlayer(move);
                        print("moving");
                        
                        yield return new WaitForSeconds(1f);
                    }
                    yield return new WaitForSeconds(1f);
                }
                moveList.Clear();

                completed = false;
                colided = false;
            }
        }
        else
        {
            // In the case when file is empty, moves are not generated
            if (ButtonsActions.inputText != "")
            {
                GenerateMovesList();
                print("generating");
            }
        }

        transform.position = movePoint.position;
        yield return new WaitForSeconds(1f);
        print("done moving");
        moving = false;
    }

    IEnumerator MovingCorutine()
    {
        while(true)
        {
            // Move player only if there are moves in moveList
            if (moveList.Count != 0)
            {
                if (clearConsole)
                {
                    lineNumber = 1;
                    consoleText.text = "Console: ";
                    clearConsole = false;
                    unrecognized = false;
                    canMove = true;
                }
                string move = moveList[0];
                print("list not empty");
                // Reset position of the player if he didn't reached the end of the level
                if (resetPosition)
                {
                    print("reset position");
                    transform.position = startingPosition;
                    movePoint.position = startingPosition;
                    yield return new WaitForSeconds(1f);
                    resetPosition = false;
                }
                else if(colided || completed)
                {
                    MovePlayer(move);
                    moveList.RemoveAt(0);
                    movePoint.position = transform.position;
                }
                else
                {

                    print("move point");
                    MovePlayer(move);
                    if (unrecognized)
                    {
                        consoleText.text += "\nIncorrect command in line " + lineNumber;
                        unrecognized = false;
                    }
                    yield return new WaitForSeconds(0.5f);
                    if (canMove)
                    {
                    print("move player");

                    transform.position = movePoint.position;
                    yield return new WaitForSeconds(0.5f);
                    print("movement done");
                    }
                    moveList.RemoveAt(0);
                    lineNumber++;
                }

            }
            else
            {
                clearConsole = true;
                // reset player position if he didnt won
                if (completed && !colided)
                {
                    print("you won!");
                    winPanel.SetActive(true);
                    completed = false;
                    resetPosition = true;
                    

                }
                else if (completed && colided)
                {
                    print("too many moves");
                    completed = false;
                    colided = false;
                    resetPosition = true;
                }
                else
                {
                    resetPosition = true;
                    completed = false;
                    colided = false;
                }

                print("empty list");
                GenerateMovesList();
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    void Start()
    {
        movePoint.parent = null;
        startingPosition = transform.position - new Vector3(0f, 0.2f, 0f);
        // Clear string before other actions
        ButtonsActions.inputText = "";

        StartCoroutine(MovingCorutine());
    }
}
