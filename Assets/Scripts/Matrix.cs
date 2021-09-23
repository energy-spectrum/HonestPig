using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    static public Cell[,] matrixCells;
    static public List<int>[] adj;
    static public int[,] vertexMatrix;
    static public PairOfIndexes[] vertexCoordinates;
    static public Vector2 GetFinishPosition(PairOfIndexes pair, Directions direction)
    {
        bool can = false;
        switch (direction)
        {
            case Directions.Up:
                can = pair.i >= 0;
                break;
            case Directions.Right:
                can = pair.j < matrixCells.GetLength(1);
                break;
            case Directions.Down:
                can = pair.i < matrixCells.GetLength(0);
                break;
            case Directions.Left:
                can = pair.j >= 0;
                break;
            default:
                print("Ban");
                break;
        }
        if (can)                   
        { 
            if(matrixCells[pair.i, pair.j].empty)
                return matrixCells[pair.i, pair.j].position; 
        }
        return AdditionalFunctions.NoneFinishPosition;
    }
    void Awake()
    {
        int height = 9, width = 17;
        //Инициализация matrixCells
        matrixCells = new Cell[height, width];
        adj = new List<int>[height * width];
        vertexMatrix = new int[height, width];
        vertexCoordinates = new PairOfIndexes[height * width];

        int i = 0, vertex = 0;
        foreach (Transform horizontal in transform)
        {
            int j = 0;
            foreach(Cell cell in horizontal.GetComponentsInChildren<Cell>())
            {
                adj[vertex] = new List<int>();
                matrixCells[i, j] = cell;
                //Клетке присваеваится номер строки и столбца
                cell.pair = new PairOfIndexes(i, j);

                if(i % 2 == 1 && j % 2 == 1)
                {
                    cell.empty = false;
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        if (j != 0) adj[vertex].Add(vertex - 1);
                        if (j != width - 1) adj[vertex].Add(vertex + 1);
                    }
                    if(j % 2 == 0)
                    {
                        if (i != 0) adj[vertex].Add(vertex - width);
                        if (i != height - 1) adj[vertex].Add(vertex + width);
                    }
                }
                
                cell.numerVertex = vertex;
                vertexMatrix[i, j] = vertex;
                vertexCoordinates[vertex] = new PairOfIndexes(i, j);

                vertex++;

                j++;
            }
            i++;
        }
    }
}
