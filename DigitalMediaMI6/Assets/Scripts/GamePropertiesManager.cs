using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum InputType
{
	VR,
	Mouse
}

enum ClientMode
{
	OnePlayer,
	TwoPlayers
}

public class GamePropertiesManager
{
	private static GamePropertiesManager instance;

	private ClientMode clientMode;
	private InputType inputType;

	public static GamePropertiesManager Instance
	{
		get
		{
			if (instance == null)
				instance = new GamePropertiesManager();

			return instance;
		}
	}

	public GamePropertiesManager()
	{
		clientMode = ClientMode.OnePlayer;
		inputType = InputType.Mouse;
	}

	#region Code
	public bool CheckIfInputIsPressed()
	{
		bool IsInputPressed = false;

		if( inputType == InputType.Mouse )
			IsInputPressed = Input.GetMouseButtonDown( 0 );

		else if( inputType == InputType.VR )
			IsInputPressed = OVRInput.Get( OVRInput.Button.One );

		return IsInputPressed;
	}

	public bool CheckIfFieldIsHitted( out RaycastHit hittedField )
	{
		bool isFieldHitted = false;

		if( inputType == InputType.Mouse )
			isFieldHitted = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hittedField, 25.0f, LayerMask.GetMask("ChessPlane"));
		else //if( inputType == InputType.VR )
			isFieldHitted = GameObject.Find("PR_Pointer").GetComponent<Pointer>().GetHittedField( out hittedField );

		return isFieldHitted;
	}

	public int Players
	{
		get
		{
			return clientMode == ClientMode.OnePlayer ? 1 : 2;
		}
	}
	#endregion
}

public static class DebugLogger
{
	private static List<string> loggingFunctions = new List<string>();
	private static bool logFunctionNames = true;

	public static List<string> Functions
	{
		get
		{
			return loggingFunctions;
		}
	}

	public static bool LogFunctionNames
	{
		get
		{
			return logFunctionNames;
		}

		set
		{
			logFunctionNames = value;
		}
	}

	public static void Log( string function, string message )
	{
		string finalMessage = "";

		if( !loggingFunctions.Contains( function ) )
			return;

		if( logFunctionNames )
			finalMessage += function + ": ";

		finalMessage += message;

		Debug.Log( finalMessage );
	}

	public static void LogValue( string function, string valueName, object value )
	{
		Log( function, valueName + " = " + value.ToString() );
	}
}