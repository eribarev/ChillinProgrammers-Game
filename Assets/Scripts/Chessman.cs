using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Permissions;
using UnityEngine;

public class Chessman : MonoBehaviour
{

    public GameObject controller; //reference
    public GameObject movePlate; //reference

    private int xBoard = -1; //positons
    private int yBoard = -1; //positions

    private string player;

    //references for the left sprites
    public Sprite white_queen, white_king, white_rook, white_knight, white_pawn, white_bishop;
    public Sprite black_queen, black_king, black_rook, black_knight, black_pawn, black_bishop;

    //this function is called by the game script (Game.cs)
    public void Activate() {
        //accessing the class GameObject so that we can perform actions and movements
        controller = GameObject.FindGameObjectWithTag("GameController");

        //take the instantiated location and adjust the transform, so we can change the xBoard and yBoard positions to the actual ones
        SetCoords();

		switch (this.name)
		{
            //accessing the game component and change the sprite to be equal to the given reference
            //depending on the name we switch to the corresponding sprite
            case "white_queen": 
                this.GetComponent<SpriteRenderer>().sprite = white_queen;
                player = "white";
                break;
            case "white_king":
                this.GetComponent<SpriteRenderer>().sprite = white_king;
                player = "white";
                break;
            case "white_rook":
                this.GetComponent<SpriteRenderer>().sprite = white_rook;
                player = "white";
                break;
            case "white_knight":
                this.GetComponent<SpriteRenderer>().sprite = white_knight;
                player = "white";
                break;
            case "white_pawn":
                this.GetComponent<SpriteRenderer>().sprite = white_pawn;
                player = "white";
                break;
            case "white_bishop":
                this.GetComponent<SpriteRenderer>().sprite = white_bishop;
                player = "white";
                break;

            case "black_queen":
                this.GetComponent<SpriteRenderer>().sprite = black_queen;
                player = "black";
                break;
            case "black_king":
                this.GetComponent<SpriteRenderer>().sprite = black_king;
                player = "black";
                break;
            case "black_rook":
                this.GetComponent<SpriteRenderer>().sprite = black_rook;
                player = "black";
                break;
            case "black_knight":
                this.GetComponent<SpriteRenderer>().sprite = black_knight;
                player = "black";
                break;
            case "black_pawn":
                this.GetComponent<SpriteRenderer>().sprite = black_pawn;
                player = "black";
                break;
            case "black_bishop":
                this.GetComponent<SpriteRenderer>().sprite = black_bishop;
                player = "black";
                break;
        }
	}

    public void SetCoords() {
        float x = xBoard;
        float y = yBoard;
        // we have to adjust these coordinates based on the "board"

        x *= 0.66f;
        y *= 0.66f;

        x += -2.3f;
        y += -2.3f;

        //setting position to a vector3, where 3 means 3 dimentional
        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    private void OnMouseUp() 
    {
        DestroyMovePlates();

        InitiateMovePlates();
    }

    public void DestroyMovePlates()
    {
        //Destroy old MovePlates
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]); //Be careful with this function "Destroy" it is asynchronous
        }
    }

    public void InitiateMovePlates() 
    {
        switch (this.name) 
        {
            case "white_queen":
            case "black_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "white_knight":
            case "black_knight":
                LMovePlate();
                break;
            case "white_bishop":
            case "black_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "white_king":
            case "black_king":
                SurroundMovePlate();
                break;
            case "white_rook":
            case "black_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "white_pawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
        }
    }

    public void LineMovePlate(int xincrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xincrement;
        int y = yBoard + yIncrement;

        while(sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xincrement;
            y += yIncrement;
        }

        if(sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<Chessman>().player != player)
        {
            MovePlateAttackSpawn(x, y);
        }

    }

    // Knight uses this move
    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    // King uses this move
    public void SurroundMovePlate()
    {
        // goes above
        PointMovePlate(xBoard, yBoard + 1);
        // goes below
        PointMovePlate(xBoard, yBoard - 1);

        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard - 0);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 0);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y)) 
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    // Pawn uses this move
    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<Chessman>().player != player)
            {
                MovePlateAttackSpawn(x - 1, y);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
