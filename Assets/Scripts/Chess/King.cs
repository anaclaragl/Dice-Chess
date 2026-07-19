using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> r = new List<Vector2Int>();

        // Right
        if (currentX + 1 < tileCountX)
        {
            // Right
            if (board[currentX + 1, currentY] == null || board[currentX + 1, currentY].team != team)
                r.Add(new Vector2Int(currentX + 1, currentY));
            // Top Right
            if (currentY + 1 < tileCountY)
                if (board[currentX + 1, currentY + 1] == null || board[currentX + 1, currentY + 1].team != team)
                    r.Add(new Vector2Int(currentX + 1, currentY + 1));
            // Bottom Right
            if (currentY - 1 >= 0)
                if (board[currentX + 1, currentY - 1] == null || board[currentX + 1, currentY - 1].team != team)
                    r.Add(new Vector2Int(currentX + 1, currentY - 1));
        }

        // Left
        if (currentX - 1 >= 0)
        {
            // Left
            if (board[currentX - 1, currentY] == null || board[currentX - 1, currentY].team != team)
                r.Add(new Vector2Int(currentX - 1, currentY));
            // Top Left
            if (currentY + 1 < tileCountY)
                if (board[currentX - 1, currentY + 1] == null || board[currentX - 1, currentY + 1].team != team)
                    r.Add(new Vector2Int(currentX - 1, currentY + 1));
            // Bottom Left
            if (currentY - 1 >= 0)
                if (board[currentX - 1, currentY - 1] == null || board[currentX - 1, currentY - 1].team != team)
                    r.Add(new Vector2Int(currentX - 1, currentY - 1));
        }

        // Up
        if (currentY + 1 < tileCountY)
            if (board[currentX, currentY + 1] == null || board[currentX, currentY + 1].team != team)
                r.Add(new Vector2Int(currentX, currentY + 1));

        // Down
        if (currentY - 1 >= 0)
            if (board[currentX, currentY - 1] == null || board[currentX, currentY - 1].team != team)
                r.Add(new Vector2Int(currentX, currentY - 1));

        return r;
    }

    public override SpecialMove GetSpecialMoves(ref ChessPiece[,] board, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves)
    {
        SpecialMove r = SpecialMove.None;

        // Castling
        int ourY = (team == 0) ? 0 : 7;
        
        // Find if the king has moved
        bool kingMove = false;
        foreach (var move in moveList)
        {
            if (board[move[1].x, move[1].y] != null && board[move[1].x, move[1].y].type == ChessPieceType.King && board[move[1].x, move[1].y].team == team)
            {
                kingMove = true;
                break;
            }
        }

        if (!kingMove)
        {
            // Right rook
            bool rightRookMove = false;
            foreach (var move in moveList)
            {
                if (board[move[1].x, move[1].y] != null && board[move[1].x, move[1].y].type == ChessPieceType.Rook && board[move[1].x, move[1].y].team == team && move[0].x == 7)
                {
                    rightRookMove = true;
                    break;
                }
            }

            if (!rightRookMove)
            {
                if (board[5, ourY] == null && board[6, ourY] == null)
                {
                    if (board[7, ourY] != null && board[7, ourY].type == ChessPieceType.Rook && board[7, ourY].team == team)
                    {
                        availableMoves.Add(new Vector2Int(6, ourY));
                        r = SpecialMove.Castling;
                    }
                }
            }

            // Left rook
            bool leftRookMove = false;
            foreach (var move in moveList)
            {
                if (board[move[1].x, move[1].y] != null && board[move[1].x, move[1].y].type == ChessPieceType.Rook && board[move[1].x, move[1].y].team == team && move[0].x == 0)
                {
                    leftRookMove = true;
                    break;
                }
            }

            if (!leftRookMove)
            {
                if (board[1, ourY] == null && board[2, ourY] == null && board[3, ourY] == null)
                {
                    if (board[0, ourY] != null && board[0, ourY].type == ChessPieceType.Rook && board[0, ourY].team == team)
                    {
                        availableMoves.Add(new Vector2Int(2, ourY));
                        r = SpecialMove.Castling;
                    }
                }
            }
        }

        return r;
    }
}
