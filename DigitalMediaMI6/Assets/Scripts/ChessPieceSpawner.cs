using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceSpawner
{
	private ChessPieceFactory factory;
	public List<GameObject> activeChessPieces;

	public ChessPiece[,] ChessPieces { set; get; }

	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFSET = 0.5f;
	private Transform transform;

	public ChessPieceSpawner(Transform _transform)
	{
		this.transform = _transform;
		factory = ChessPieceFactory.GetInstance();
	}

	public void SpawnAllPieces()
	{
		if (factory == null)
		{
			Debug.Log("Factory is null");
		}
		activeChessPieces = new List<GameObject>();
		ChessPieces = new ChessPiece[8, 8];

		SpawnChessPiece(factory.BuildWhiteKing(), 4, 0);
		SpawnChessPiece(factory.BuildWhiteQueen(), 3, 0);
		SpawnChessPiece(factory.BuildWhiteRook(), 0, 0);
		SpawnChessPiece(factory.BuildWhiteRook(), 7, 0);
		SpawnChessPiece(factory.BuildWhiteBishop(), 2, 0);
		SpawnChessPiece(factory.BuildWhiteBishop(), 5, 0);
		SpawnChessPiece(factory.BuildWhiteKnight(), 1, 0);
		SpawnChessPiece(factory.BuildWhiteKnight(), 6, 0);
		for (int i = 0; i < 8; i++)
		{
			SpawnChessPiece(factory.BuildWhitePawn(), i, 1);
		}

		SpawnChessPiece(factory.BuildBlackKing(), 4, 7);
		SpawnChessPiece(factory.BuildBlackQueen(), 3, 7);
		SpawnChessPiece(factory.BuildBlackRook(), 0, 7);
		SpawnChessPiece(factory.BuildBlackRook(), 7, 7);
		SpawnChessPiece(factory.BuildBlackBishop(), 2, 7);
		SpawnChessPiece(factory.BuildBlackBishop(), 5, 7);
		SpawnChessPiece(factory.BuildBlackKnight(), 1, 7);
		SpawnChessPiece(factory.BuildBlackKnight(), 6, 7);
		for (int i = 0; i < 8; i++)
		{
			SpawnChessPiece(factory.BuildBlackPawn(), i, 6);
		}

	}

	public void RemoveChessPiece(ChessPiece piece)
	{
		this.activeChessPieces.Remove(piece.gameObject);
	}

	private void SpawnChessPiece(GameObject piece, int x, int y)
	{
		GameObject go = MonoBehaviour.Instantiate(piece, GetTileCenter(x, y), Quaternion.identity) as GameObject;
		go.transform.SetParent(transform);
		ChessPieces[x, y] = go.GetComponent<ChessPiece>();
		ChessPieces[x, y].SetPosition(x, y, false);
		ChessPieces[x, y].SetSpawner(this);
		activeChessPieces.Add(go);
	}

	public Vector3 GetTileCenter(int x, int y)
	{
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE * x) + TILE_OFFSET;
		origin.z += (TILE_SIZE * y) + TILE_OFFSET;
		return origin;
	}

}