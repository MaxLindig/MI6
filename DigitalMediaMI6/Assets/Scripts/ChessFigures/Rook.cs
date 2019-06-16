using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece {
    public override bool[, ] PossibleMove () {
        bool[, ] r = new bool[8, 8];

        ChessPiece c;
        int i;

        //rechts
        i = X;
        while (true) {
            i++;
            if (i >= 8) {
                break;
            }

            c = spawner.ChessPieces[i, Y];
            if (c == null) {
                r[i, Y] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[i, Y] = true;
                }
                break;
            }

        }

        //links
        i = X;
        while (true) {
            i--;
            if (i < 0) {
                break;
            }

            c = spawner.ChessPieces[i, Y];
            if (c == null) {
                r[i, Y] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[i, Y] = true;
                }
                break;
            }

        }

        //hoch
        i = Y;
        while (true) {
            i++;
            if (i >= 8) {
                break;
            }

            c = spawner.ChessPieces[X, i];
            if (c == null) {
                r[X, i] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[X, i] = true;
                }
                break;
            }

        }

        //hoch
        i = Y;
        while (true) {
            i--;
            if (i < 0) {
                break;
            }

            c = spawner.ChessPieces[X, i];
            if (c == null) {
                r[X, i] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[X, i] = true;
                }
                break;
            }

        }

        return r;

    }
}