using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Vector2 _mapSize;
    [SerializeField] private float _borderSize;
    [SerializeField] private float _deltaSize;
    [SerializeField] private int _countEnemies;
    [SerializeField] private int _countFruits;
    [Space]
    [SerializeField] private ObjectFactory _factory;
    [SerializeField] private StartPanel _startPanel;
    [SerializeField] private GamePanel _gamePanel;
    [SerializeField] private EndPanel _endPanel;
    [SerializeField] private PlayerView _playerView;
    private PlayerPresenter _playerPresenter;
    private PlayerModel _playerModel;
    private MapGenerator _map;
    private Spawner _spawner;
    private CollisionChecker _checker;
    private EnemyCollection _enemies;
    private FruitCollection _fruits;

    private void Awake()
    {
        _playerModel = new PlayerModel();
        _playerPresenter = new PlayerPresenter(_playerView, _playerModel, _gamePanel);

        _map = new MapGenerator(_mapSize, _borderSize, _deltaSize);
        _map.Generate(_factory);

        _spawner = new Spawner(_map.MapBorder, _factory, _countEnemies, _countFruits, _playerModel.Position);
        _spawner.FillMap();
        _enemies = _spawner.Enemies;
        _enemies.Die();
        _fruits = _spawner.Fruits;
        _fruits.Hide();

        _checker = new CollisionChecker(_playerModel);
    }

    private void OnEnable()
    {
        _checker.Collision += OnCollision;
        _playerModel.Death += OnPlayerDeath;
        _startPanel.GameStarted += OnGameStarted;
        _endPanel.RestartClick += OnGameStarted;
        _playerPresenter.Enable();
        _enemies.Enable();
        _fruits.Enable();
    }

    private void OnDisable()
    {
        _checker.Collision -= OnCollision;
        _playerModel.Death -= OnPlayerDeath;
        _startPanel.GameStarted -= OnGameStarted;
        _endPanel.RestartClick -= OnGameStarted;
        _playerPresenter.Disable();
        _enemies.Disable();
        _fruits.Disable();
    }

    private void Update()
    {
        if(_enemies != null)
            _enemies.UpdatePosition();

        if(_checker != null && _checker.IsReady)
            _checker.CheckAllCollisionablesWithPlayer();
    }

    private void OnGameStarted()
    {
        _playerModel.GameStart(_map.MapBorder, Vector3.zero);
        _gamePanel.OnGameStart();
        _fruits.Show();
        _enemies.Start();
        _checker.GameStart(_spawner.Collisionables);
    }

    private void OnCollision(ICollisionable collisionable)
    {
        _spawner.ResetCollisionable(collisionable);
    }

    private void OnPlayerDeath(int score)
    {
        _enemies.Die();
        _fruits.Hide();
        _checker.GameEnd();
        _gamePanel.OnDeath();
        _endPanel.OnDeath(score);
    }
}
