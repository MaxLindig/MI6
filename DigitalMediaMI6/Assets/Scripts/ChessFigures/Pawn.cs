using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece {

    public override bool[, ] PossibleMove () {
        bool[, ] r = new bool[8, 8];

        ChessPiece c, c2;

        //weiß
        if (isWhite) {
            //diagonal links
            if (X != 0 && Y != 7) {
                c = spawner.ChessPieces[X - 1, Y + 1];
                if (c != null && !c.isWhite) {
                    r[X - 1, Y + 1] = true;
                }
            }
            //diagonal rechts
            if (X != 7 && Y != 7) {
                c = spawner.ChessPieces[X + 1, Y + 1];
                if (c != null && !c.isWhite) {
                    r[X + 1, Y + 1] = true;
                }
            }
            //gerade
            if (Y != 7) {
                c = spawner.ChessPieces[X, Y + 1];
                if (c == null) {
                    r[X, Y + 1] = true;
                }
            }
            //gerade erster Zug
            if (Y == 1) {
                c = spawner.ChessPieces[X, Y + 1];
                c2 = spawner.ChessPieces[X, Y + 2];
                if (c == null && c2 == null) {
                    r[X, Y + 2] = true;
                }
            }
            //en passant
        } else {
            //diagonal links
            if (X != 0 && Y != 0) {
                c = spawner.ChessPieces[X - 1, Y - 1];
                if (c != null && c.isWhite) {
                    r[X - 1, Y - 1] = true;
                }
            }
            //diagonal rechts
            if (X != 7 && Y != 0) {
                c = spawner.ChessPieces[X + 1, Y - 1];
                if (c != null && c.isWhite) {
                    r[X + 1, Y + 1] = true;
                }
            }
            //gerade
            if (Y != 0) {
                c = spawner.ChessPieces[X, Y - 1];
                if (c == null) {
                    r[X, Y - 1] = true;
                }
            }
            //gerade erster Zug
            if (Y == 6) {
                c = spawner.ChessPieces[X, Y - 1];
                c2 = spawner.ChessPieces[X, Y - 2];
                if (c == null && c2 == null) {
                    r[X, Y - 2] = true;
                }
            }

        }
        return r;
    }

}