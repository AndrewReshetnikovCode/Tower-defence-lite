using UnityEngine;
using UnityEngine.Tilemaps;

public class SnakeRenderer : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase emptyTile;
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase snakeHeadTile;
    [SerializeField] private TileBase snakeBodyTile;
    [SerializeField] private TileBase foodTile;

    private SnakeController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<SnakeController>();
    }

    private void Update()
    {
        RenderGame();
    }

    private void RenderGame()
    {
        tilemap.ClearAllTiles();
        SnakeGame game = gameController.GetGameState();

        if (game == null) return;

        for (int x = 0; x < game.Width; x++)
            for (int y = 0; y < game.Height; y++)
            {
                TileBase tileToSet = null;
                Vector3Int position = new Vector3Int(x, y, 0);

                switch (game.Grid[x, y].Type)
                {
                    case CellType.Empty:
                        tileToSet = emptyTile;
                        break;
                    case CellType.Wall:
                        tileToSet = wallTile;
                        break;
                    case CellType.SnakeHead:
                        tileToSet = snakeHeadTile;
                        break;
                    case CellType.SnakeBody:
                        tileToSet = snakeBodyTile;
                        break;
                    case CellType.Food:
                        tileToSet = foodTile;
                        break;
                }

                if (tileToSet != null)
                    tilemap.SetTile(position, tileToSet);
            }
    }
}