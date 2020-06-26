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
                break;
            case "white_king":
                this.GetComponent<SpriteRenderer>().sprite = white_king;
                break;
            case "white_rook":
                this.GetComponent<SpriteRenderer>().sprite = white_rook;
                break;
            case "white_knight":
                this.GetComponent<SpriteRenderer>().sprite = white_knight;
                break;
            case "white_pawn":
                this.GetComponent<SpriteRenderer>().sprite = white_pawn;
                break;
            case "white_bishop":
                this.GetComponent<SpriteRenderer>().sprite = white_bishop;
                break;

            case "black_queen":
                this.GetComponent<SpriteRenderer>().sprite = black_queen;
                break;
            case "black_king":
                this.GetComponent<SpriteRenderer>().sprite = black_king;
                break;
            case "black_rook":
                this.GetComponent<SpriteRenderer>().sprite = black_rook;
                break;
            case "black_knight":
                this.GetComponent<SpriteRenderer>().sprite = black_knight;
                break;
            case "black_pawn":
                this.GetComponent<SpriteRenderer>().sprite = black_pawn;
                break;
            case "black_bishop":
                this.GetComponent<SpriteRenderer>().sprite = black_bishop;
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
        GameObject[] movePlates = GameObject.FindGameObjectWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++) 
        {
            Destroy(movePlates[i]);
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
}
