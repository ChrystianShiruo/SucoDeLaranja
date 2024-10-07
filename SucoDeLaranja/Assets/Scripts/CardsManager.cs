using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour {

    public static float cardScaleMultiplier;

    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private RectTransform _boardPanel;

    private GameData _gameData;
    private GridLayoutGroup _boardPanelLayoutGroup;

    public void Init(GameData gameData) {
        _gameData = gameData;

        _boardPanelLayoutGroup = _boardPanel.GetComponent<GridLayoutGroup>();
        _boardPanelLayoutGroup.constraintCount = _gameData.Board.GetLength(1);

        SetBoardLayout(_boardPanel, new Vector2Int(_gameData.Board.GetLength(0), _gameData.Board.GetLength(1)));
        InstantiateCards(_gameData.Board);

    }


    private void InstantiateCards(CardState[,] board) {

        int width = board.GetLength(0);
        int height = board.GetLength(1);

        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                GameObject go = Instantiate(_cardPrefab, _boardPanel);
                go.GetComponent<Card>().Init(board[x, y]);
            }
        }
    }

    private void SetBoardLayout(RectTransform boardPanel, Vector2Int dimensions) {
        float x = boardPanel.rect.width;
        float y = boardPanel.rect.height;
        Debug.Log($"{x}, {y}");
        x = x / dimensions.x;
        y = y / dimensions.y;
        cardScaleMultiplier = (x > y ? y : x) / 100f;
        _boardPanelLayoutGroup.cellSize = new Vector2(x, y);
    }
}