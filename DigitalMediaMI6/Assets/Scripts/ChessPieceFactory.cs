using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ChessPieceFactory : MonoBehaviour
{
    private static ChessPieceFactory instance;
    [SerializeField]
    private GameObject pawnWhitePrefab;
    [SerializeField]
    private GameObject knightWhitePrefab;
    [SerializeField]
    private GameObject rookWhitePrefab;
    [SerializeField]
    private GameObject bishopWhitePrefab;
    [SerializeField]
    private GameObject queenWhitePrefab;
    [SerializeField]
    private GameObject kingWhitePrefab;
    [SerializeField]
    private GameObject pawnBlackPrefab;
    [SerializeField]
    private GameObject knightBlackPrefab;
    [SerializeField]
    private GameObject rookBlackPrefab;
    [SerializeField]
    private GameObject bishopBlackPrefab;
    [SerializeField]
    private GameObject queenBlackPrefab;
    [SerializeField]
    private GameObject kingBlackPrefab;
    [SerializeField]

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            this.enabled = false;
    }

    public static ChessPieceFactory GetInstance()
    {
        if (instance == null)
        {
            GameObject gameobject = new GameObject("ChessPieceFactory");
            instance = gameobject.AddComponent<ChessPieceFactory>();
        }
        return instance;
    }

    public GameObject BuildWhitePawn()
    {
        return pawnWhitePrefab;
    }

    public GameObject BuildWhiteKnight()
    {
        return knightWhitePrefab;
    }

    public GameObject BuildWhiteRook()
    {
        return rookWhitePrefab;
    }

    public GameObject BuildWhiteBishop()
    {
        return bishopWhitePrefab;
    }

    public GameObject BuildWhiteQueen()
    {
        return queenWhitePrefab;
    }

    public GameObject BuildWhiteKing()
    {
        return kingWhitePrefab;
    }

    public GameObject BuildBlackPawn()
    {
        return pawnBlackPrefab;
    }

    public GameObject BuildBlackKnight()
    {
        return knightBlackPrefab;
    }

    public GameObject BuildBlackRook()
    {
        return rookBlackPrefab;
    }

    public GameObject BuildBlackBishop()
    {
        return bishopBlackPrefab;
    }

    public GameObject BuildBlackQueen()
    {
        return queenBlackPrefab;
    }

    public GameObject BuildBlackKing()
    {
        return kingBlackPrefab;
    }
}