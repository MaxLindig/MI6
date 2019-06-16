using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece {

    public override bool[, ] PossibleMove () {
        bool[, ] r = new bool[8, 8];

        //UpLeft
        KnightMove (X - 1, Y + 2, ref r);
        //UpRight
        KnightMove (X + 1, Y + 2, ref r);
        //RightUp
        KnightMove (X + 2, Y + 1, ref r);
        //RightDown
        KnightMove (X + 2, Y - 1, ref r);
        //DownLeft
        KnightMove (X - 1, Y - 2, ref r);
        //DownRight
        KnightMove (X + 1, Y - 2, ref r);
        //LeftUp
        KnightMove (X - 2, Y + 1, ref r);
        //LeftDown
        KnightMove (X - 2, Y - 1, ref r);

        return r;
    }

    public void KnightMove (int x, int y, ref bool[, ] r) {
        ChessPiece c;
        if (x >= 0 && x < 8 && y >= 0 && y < 8) {
            c = spawner.ChessPieces[x, y];
            if (c == null)
                r[x, y] = true;
            else if (isWhite != c.isWhite)
                r[x, y] = true;
        }
    }
}