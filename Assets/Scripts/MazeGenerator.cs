using UnityEngine;

public class MazeGenerator : MonoBehaviour
{ 
    public int width = 11;
    public int height = 11;
    private int[,] _maze;

    public CustomObjectPool wallPool;
    public CustomObjectPool pathPool;
    public CustomObjectPool startPool;
    public CustomObjectPool endPool;

    void Start()
    {
        _maze = new int[width, height];
        GenerateMaze(1, 1);
        PlaceStartAndEnd();
        VisualizeMaze();
    }

    void GenerateMaze(int x, int y)
    {
        _maze[x, y] = 1;

        int[] directions = { 0, 1, 2, 3 };
        Shuffle(directions);

        foreach (int dir in directions)
        {
            int nx = x + (dir == 1 ? 2 : dir == 3 ? -2 : 0);
            int ny = y + (dir == 0 ? -2 : dir == 2 ? 2 : 0);

            if (nx >= 0 && nx < width && ny >= 0 && ny < height && _maze[nx, ny] == 0)
            {
                _maze[nx, ny] = 1;
                _maze[x + (dir == 1 ? 1 : dir == 3 ? -1 : 0), y + (dir == 0 ? -1 : dir == 2 ? 1 : 0)] = 1;
                GenerateMaze(nx, ny);
            }
        }
    }

    void Shuffle(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }

    void PlaceStartAndEnd()
    {
        _maze[1, 1] = 2;
        _maze[width - 2, height - 2] = 3;
    }

    void VisualizeMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x, 0, y);
                GameObject obj = null;

                if (_maze[x, y] == 0)
                {
                    obj = wallPool.GetObject();
                    if (obj != null)
                    {
                        obj.transform.position = position;
                    }
                }
                else if (_maze[x, y] == 1)
                {
                    obj = pathPool.GetObject();
                    if (obj != null)
                    {
                        obj.transform.position = position;
                    }
                }
                else if (_maze[x, y] == 2)
                {
                    obj = startPool.GetObject();
                    if (obj != null)
                    {
                        obj.transform.position = position;
                    }
                }
                else if (_maze[x, y] == 3)
                {
                    obj = endPool.GetObject();
                    if (obj != null)
                    {
                        obj.transform.position = position;
                    }
                }
            }
        }
    }
}
