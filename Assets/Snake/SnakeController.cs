using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum CellType
{
    Empty,
    Wall,
    SnakeHead,
    SnakeBody,
    Food,
    MovingObstacle
}
public class Cell
{
    public CellType Type { get; set; }
    public Vector2Int Position { get; }

    public Cell(int x, int y)
    {
        Position = new Vector2Int(x, y);
        Type = CellType.Empty;
    }
}
public class MovingObstacle
{
    public List<Vector2Int> positions;
    public Vector2Int dir;
    public Vector2Int oppositeDir;
    public int moveDistance;

    public bool isOppositeDir = false;
    public int distancePassed = 0;
}
public class SnakeGame
{
    public int Width { get; } = 30;
    public int Height { get; } = 20;
    public Cell[,] Grid { get; private set; }
    public List<Vector2Int> SnakeSegments { get; private set; } = new List<Vector2Int>();
    public Vector2Int Direction { get; set; } = Vector2Int.right;
    public bool IsAlive { get; private set; } = true;

    private Vector2Int nextDirection;
    private System.Random random = new System.Random();

    Vector2Int _foodCellBuffer;

    List<MovingObstacle> _movingObstacles = new() 
    { 
        new() { dir = Vector2Int.left, oppositeDir = Vector2Int.right, moveDistance = 7, positions = new() { new(2, 2),new(3, 2), new(4, 2), } },
        new() { dir = Vector2Int.left, oppositeDir = Vector2Int.right, moveDistance = 7, positions = new() { new(2, 8),new(3, 8), new(4, 8), } },
        new() { dir = Vector2Int.left, oppositeDir = Vector2Int.right, moveDistance = 7, positions = new() { new(2, 14),new(3, 14), new(4, 14), } }
    };
    public void Initialize()
    {
        // Создаем сетку
        Grid = new Cell[Width, Height];
        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                Grid[x, y] = new Cell(x, y);

        // Добавляем стены
        for (int x = 0; x < Width; x++)
        {
            Grid[x, 0].Type = CellType.Wall;
            Grid[x, Height - 1].Type = CellType.Wall;
        }
        for (int y = 0; y < Height; y++)
        {
            Grid[0, y].Type = CellType.Wall;
            Grid[Width - 1, y].Type = CellType.Wall;
        }

        // Создаем змейку
        int startX = Width / 2;
        int startY = Height / 2;
        SnakeSegments.Add(new Vector2Int(startX, startY));
        SnakeSegments.Add(new Vector2Int(startX - 1, startY));
        SnakeSegments.Add(new Vector2Int(startX - 2, startY));

        // Обновляем сетку
        UpdateGrid();
        SpawnFood();
    }

    public void UpdateDirection(Vector2Int newDirection)
    {
        // Запрет разворота на 180°
        if (newDirection + Direction != Vector2Int.zero)
            nextDirection = newDirection;
    }

    public void Move()
    {
        if (!IsAlive) return;

        HandleMovingObstacles();

        // Обновляем направление
        Direction = nextDirection;

        // Новая позиция головы
        Vector2Int head = SnakeSegments[0];
        Vector2Int newHead = head + Direction;

        // Проверка столкновений
        if (IsCollision(newHead))
        {
            IsAlive = false;
            return;
        }

        // Добавляем новую голову
        SnakeSegments.Insert(0, newHead);

        // Проверяем съедение еды
        if (Grid[newHead.x, newHead.y].Type == CellType.Food)
        {
            SpawnFood();
        }
        else
        {
            // Удаляем хвост если не съели еду
            Vector2Int tail = SnakeSegments[SnakeSegments.Count - 1];
            SnakeSegments.RemoveAt(SnakeSegments.Count - 1);
            Grid[tail.x, tail.y].Type = CellType.Empty;
        }

        UpdateGrid();
    }

    private bool IsCollision(Vector2Int position)
    {
        // Проверка стен и тела змеи
        return position.x < 0 || position.y < 0 ||
               position.x >= Width || position.y >= Height ||
               Grid[position.x, position.y].Type == CellType.Wall ||
               Grid[position.x, position.y].Type == CellType.SnakeBody;
    }

    private void SpawnFood()
    {
        List<Vector2Int> emptyCells = new List<Vector2Int>();

        for (int x = 1; x < Width - 1; x++)
            for (int y = 1; y < Height - 1; y++)
                if (Grid[x, y].Type == CellType.Empty)
                    emptyCells.Add(new Vector2Int(x, y));

        if (emptyCells.Count > 0)
        {
            int index = random.Next(emptyCells.Count);
            Grid[emptyCells[index].x, emptyCells[index].y].Type = CellType.Food;
        }
    }

    private void UpdateGrid()
    {
        // Обновляем все сегменты змеи
        foreach (var segment in SnakeSegments)
        {
            Grid[segment.x, segment.y].Type =
                segment == SnakeSegments[0] ?
                CellType.SnakeHead :
                CellType.SnakeBody;
        }
    }

    void HandleMovingObstacles()
    {
        foreach (var item in _movingObstacles)
        {
            foreach (var pos in item.positions)
            {
                Grid[pos.x, pos.y].Type = CellType.Empty;
            }

            item.distancePassed++;
            if (item.distancePassed >= item.moveDistance)
            {
                item.distancePassed = 0;
                item.isOppositeDir = !item.isOppositeDir;
            }

            Vector2Int dir;
            if (item.isOppositeDir)
            {
                dir = item.dir;
            }
            else
            {
                dir = item.oppositeDir;
            }

            for (int i = 0; i < item.positions.Count; i++)
            {
                item.positions[i] += dir;
                if (Grid[item.positions[i].x, item.positions[i].y].Type == CellType.Food)
                {
                    _foodCellBuffer = item.positions[i];
                }
            }

            foreach (var pos in item.positions)
            {
                Grid[pos.x, pos.y].Type = CellType.Wall;
            }

            if (_foodCellBuffer != new Vector2Int(int.MinValue, int.MinValue) && Grid[_foodCellBuffer.x, _foodCellBuffer.y].Type == CellType.Empty)
            {
                Grid[_foodCellBuffer.x, _foodCellBuffer.y].Type = CellType.Food;
                _foodCellBuffer = new Vector2Int(int.MinValue, int.MinValue);
            }
        }
    }
}

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float moveInterval = 0.2f;
    private SnakeGame game;
    private float moveTimer;

    private void Start()
    {
        game = new SnakeGame();
        game.Initialize();
    }

    private void Update()
    {
        HandleInput();
        moveTimer += Time.deltaTime;

        if (moveTimer >= moveInterval)
        {
            game.Move();
            moveTimer = 0;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) game.UpdateDirection(Vector2Int.up);
        if (Input.GetKeyDown(KeyCode.S)) game.UpdateDirection(Vector2Int.down);
        if (Input.GetKeyDown(KeyCode.A)) game.UpdateDirection(Vector2Int.left);
        if (Input.GetKeyDown(KeyCode.D)) game.UpdateDirection(Vector2Int.right);
    }

    public SnakeGame GetGameState() => game;
}
