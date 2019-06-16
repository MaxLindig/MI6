using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBoard
{
	public static void Update()
	{
		Vector3 widthLine = Vector3.right * 8;
		Vector3 heigthLine = Vector3.forward * 8;

		for (int i = 0; i <= 8; i++)
		{
			Vector3 start = Vector3.forward * i;
			Debug.DrawLine(start, start + widthLine, Color.green);
			for (int j = 0; j <= 8; j++)
			{
				start = Vector3.right * j;
				Debug.DrawLine(start, start + heigthLine, Color.green);

			}
		}
	}
}

