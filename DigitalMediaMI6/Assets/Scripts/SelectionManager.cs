using ControllerSelection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public enum ClickAction
{
	Move,
	Select,
	DeSelect,
	None
}

public class SelectionManager
{
	private Vector2 clickedField;
	private Vector2 selectedField;
	private ChessPieceSpawner spawner;

	public Vector2 ClickedField
	{
		get { return clickedField; }
	}
	public Vector2 SelectedField
	{
		get { return selectedField; }
	}

	public bool IsFieldSelected
	{
		get 
		{ 
			return selectedField.x >= 0 && selectedField.y >= 0;
		}
	}
	public bool IsPieceSelected 
	{ 
		get
		{
			return IsFieldSelected && SelectedPiece != null;
		}
	}
	public bool IsPieceClicked
	{
		get
		{
			return ClickedPiece != null;
		}
	}

	public ChessPiece SelectedPiece
	{
		get
		{
			return spawner.ChessPieces[(int)selectedField.x, (int)selectedField.y];
		}
	}
	public ChessPiece ClickedPiece
	{
		get
		{
			return spawner.ChessPieces[(int)clickedField.x, (int)clickedField.y];
		}
	}

	public SelectionManager( ChessPieceSpawner spawner )
	{
		this.spawner = spawner;
		this.selectedField = new Vector2();
		this.clickedField = new Vector2();
	}

    public ClickAction ProcessClick()
    {
		RaycastHit hittedField;

        try
        {
            if( !Camera.main )
                throw new Exception("Camera not main");

			InitField( ref clickedField );
			
			if( GamePropertiesManager.Instance.CheckIfFieldIsHitted( out hittedField ) )
			{
				Click( (int)hittedField.point.x, (int)hittedField.point.z );

				if( IsPieceSelected )
				{
					if( SelectedPiece.CheckIfMoveIsValid( clickedField ) )
						return ClickAction.Move;
					else
						return ClickAction.DeSelect;
				}
				else
				{
					if( IsPieceClicked )
						return ClickAction.Select;
					else
						return ClickAction.None;
				}

			}
        }
        catch (Exception xept)
        {
            Debug.Log(xept.Message);
        }

		return ClickAction.None;
	}

	public void MoveChessPiece()
	{
		SelectedPiece.SetPosition( (int)clickedField.x, (int)clickedField.y );	
	}
	public void DeSelectChessPiece()
	{
		InitField( ref selectedField );
	}

	public void Select( int X, int Y )
	{
		selectedField.x = X;
		selectedField.y = Y;
	}
	public void Select( Vector2 coords )
	{
		Select( (int)coords.x, (int)coords.y );
	}
	
	public void Click( int X, int Y )
	{
		clickedField.x = X;
		clickedField.y = Y;
	}
	public void Click( Vector2 coords )
	{
		Click( (int)coords.x, (int)coords.y );
	}

	private void InitField( ref Vector2 field )
	{
		field.x = -1;
		field.y = -1;
	}
}
