using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece {
    public override bool[, ] PossibleMove () {
        bool[, ] r = new bool[8, 8];

        ChessPiece c;
        int i, j;

        //Top Side
        i = X - 1;
        j = Y + 1;
        if (Y != 7) {
            for (int k = 0; k < 3; k++) {
                if (i >= 0 && i < 8) {
                    c = spawner.ChessPieces[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }

                i++;
            }
        }

        //Down Side
        i = X - 1;
        j = Y - 1;
        if (Y != 0) {
            for (int k = 0; k < 3; k++) {
                if (i >= 0 && i < 8) {
                    c = spawner.ChessPieces[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }

                i++;
            }
        }

        //Middle Left
        if (X != 0) {
            c = spawner.ChessPieces[X - 1, Y];
            if (c == null)
                r[X - 1, Y] = true;
            else if (isWhite != c.isWhite)
                r[X - 1, Y] = true;

        }

        //Middle Right
        if (X != 7) {
            c = spawner.ChessPieces[X + 1, Y];
            if (c == null)
                r[X + 1, Y] = true;
            else if (isWhite != c.isWhite)
                r[X + 1, Y] = true;

        }

        return r;
    }

}