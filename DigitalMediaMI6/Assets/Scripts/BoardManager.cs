using ControllerSelection;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent( typeof( ChessPieceFactory ) )]

public class BoardManager : MonoBehaviour
{
	[HideInInspector]
	public OVRInput.Controller activeController = OVRInput.Controller.RTouch;

	public static BoardManager Instance { set; get; }

	public bool isWhiteTurn = true;
	private Client client;
	private ChessPieceSpawner spawner;
	private SelectionManager selection;
	public GameObject camera1;
	public GameObject camera2;
	public GameObject pointer1;
	public GameObject pointer2;
	public GameObject event1;
	public GameObject event2;
	public GameObject reticule1;
	public GameObject reticule2;

	float timePercentage = 0.0f;

	Pawn pawn;
	
	private float timeToArrive;

	private void IsHostCamera()
	{
		if( client.isHost == true )
		{
			camera1.SetActive( true );
			pointer1.SetActive( true );
			event1.SetActive( true );
			reticule1.SetActive( true );
		}
		else if( client.isHost == false )
		{
			camera2.SetActive( true );
			pointer2.SetActive( true );
			event2.SetActive( true );
			reticule2.SetActive( true );
		}
	}

	private bool IsMyTurn
	{
		get
		{
			return client.isHost == isWhiteTurn;
		}
	}

	private void Start()
	{
		AddDebugFunctions();
		Instance = this;
		
		spawner = new ChessPieceSpawner( this.transform );
		selection = new SelectionManager( this.spawner );
		client = FindObjectOfType<Client>();

		spawner.SpawnAllPieces();

		IsHostCamera();

		isWhiteTurn = true;
	}

	private void Update()
	{
		IsHostCamera();

		bool IsInputPressed = false;

		DrawBoard.Update();
		IsInputPressed = GamePropertiesManager.Instance.CheckIfInputIsPressed();

		if( IsInputPressed && IsMyTurn )
		{
			ProcessTurn();	
		}
	}

	private void RemoveHighlights()
	{
		BoardHighlights.Instance.HideHighlights();
	}

	private void HighlightPiece(ChessPiece piece)
	{
		RemoveHighlights();
		BoardHighlights.Instance.HighLightAllowedMoves( piece );
	}

	public void ProcessEnemiesTurn(int Xs, int Ys, int Xd, int Yd)
	{
		selection.Select( Xs, Ys );
		if( !selection.IsPieceSelected )
			return;

		selection.Click( Xd, Yd );

		MoveSelectedChessPiece();
	}

	private void ProcessTurn()
	{
		ClickAction action = ClickAction.None;

		RemoveHighlights();
		action = selection.ProcessClick();

		switch( action )
		{
			case ClickAction.Select:
				DebugLogger.Log( "ProcessTurn", "Select" );
				{
					SelectIfPossible();
				}
				break;

			case ClickAction.DeSelect:
				DebugLogger.Log( "ProcessTurn", "DeSelect" );
				{
					if( selection.IsPieceClicked )
					{
						if( !SelectIfPossible() )
							selection.DeSelectChessPiece();
					}
					else
						selection.DeSelectChessPiece();
				}
				break;

			case ClickAction.Move:
				DebugLogger.Log( "ProcessTurn", "Move" );
				{
					
					SendMoveSelectedChessPieceMessage();
					MoveSelectedChessPiece();
				}
				break;
		}
	}

	#region Select
	private bool SelectIfPossible()
	{
		bool isClickedPieceMyColor = selection.ClickedPiece.isWhite == client.isHost;

		if( isClickedPieceMyColor )
		{
			selection.Select( selection.ClickedField );
			HighlightPiece( selection.ClickedPiece );
		}

		return isClickedPieceMyColor;
	}
	#endregion

	#region Move Chess Piece
	private void MoveSelectedChessPiece()
	{

		DebugLogger.Log( "MoveSelectedChessPiece", "Begin" );
		ChessPiece victimPiece = selection.ClickedPiece;

		if( victimPiece != null && victimPiece.isWhite != selection.SelectedPiece.isWhite )
		{
			if( victimPiece is King )
			{
				EndGame();
				return;
			}

			spawner.RemoveChessPiece( victimPiece );
			DestroyImmediate( victimPiece.gameObject );
		}
		//checkMoveAnimation();
		StartCoroutine( MoveToDestinationInTime() );
		//pawn.Animation();

		selection.MoveChessPiece();

		ToggleTurn();
	}

	private void ToggleTurn()
	{
		if( isWhiteTurn )
			isWhiteTurn = false;
		else
			isWhiteTurn = true;
	}

	private void EndGame()
	{
		if( isWhiteTurn )
			Debug.Log( "White team wins" );
		else
			Debug.Log( "Black team wins" );

		foreach( GameObject go in spawner.activeChessPieces )
			DestroyImmediate( go );

		isWhiteTurn = true;
		RemoveHighlights();

		spawner.SpawnAllPieces();
	}

	private void SendMoveSelectedChessPieceMessage()
	{
		string message = "CMOV|";
		message += BuildCoordinateString( selection.SelectedField ) + "|";
		message += BuildCoordinateString( selection.ClickedField );

		client.Send( message );
	}

	private string BuildCoordinateString(Vector2 coords)
	{
		return ( (int)coords.x ).ToString() + "|" + ( (int)coords.y ).ToString();
	}
	#endregion


	private void DebugPrint(string functionName, string valueName, object value)
	{
		Debug.Log( functionName + ": " + valueName + " = " + value.ToString() );
	}

	private void AddDebugFunctions()
	{
		DebugLogger.Functions.Add( "MoveSelectedChessPiece" );
		DebugLogger.Functions.Add( "ProcessTurn" );
		DebugLogger.Functions.Add( "ProcessClick" );

	}

	public IEnumerator MoveToDestinationInTime()
	{

		timeToArrive = 1.0f;
		//float timePercentage = 0.0f;

		Vector3 start = new Vector3(),
				end = new Vector3();

		ChessPiece pieceToMove = selection.SelectedPiece;

		start.x = selection.SelectedField.x + 0.5f;
		start.z = selection.SelectedField.y + 0.5f;

		end.x = selection.ClickedField.x + 0.5f;
		end.z = selection.ClickedField.y + 0.5f;


		while( timePercentage < 1 )
		{
			timePercentage += Time.deltaTime / timeToArrive;
			Debug.Log( timePercentage );
			pieceToMove.transform.position = Vector3.Lerp( start, end, timePercentage );
			yield return null;
		}
	}

	public void checkMoveAnimation() {
		/*Debug.Log( "# Animation in Pawn aufgerufen #" );
		 ### anim
		//anim = GetComponent<Animator>();
		//anim = pawnWhitePrefab.GetComponent<Animator>();
		anim.SetBool( "isWalking", true );
		*/
		
		/*if(timePercentage < 1.5f) {
			pawn.Animation();
		} */


	}

}
