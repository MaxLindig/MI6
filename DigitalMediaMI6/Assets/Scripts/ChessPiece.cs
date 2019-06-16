using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessPiece : MonoBehaviour
{
	public int X { set; get; }
	public int Y { set; get; }

	public bool isWhite;
	protected ChessPieceSpawner spawner;

	public abstract bool[,] PossibleMove();

	public void SetPosition(int x, int y, bool updateField = true)
	{
		if (updateField)
		{
			spawner.ChessPieces[this.X, this.Y] = null;
			transform.position = spawner.GetTileCenter(x, y);
		}

		this.X = x;
		this.Y = y;

		if (updateField)
		{
			// X and Y are updated now
			spawner.ChessPieces[this.X, this.Y] = this;
		}
	}

	public void SetSpawner(ChessPieceSpawner _spawner)
	{
		spawner = _spawner;
	}

	public bool CheckIfMoveIsValid(int X, int Y)
	{
		return PossibleMove()[X, Y];
	}

	public bool CheckIfMoveIsValid(Vector2 coords)
	{
		return CheckIfMoveIsValid( (int)coords.x, (int)coords.y );
	}
}