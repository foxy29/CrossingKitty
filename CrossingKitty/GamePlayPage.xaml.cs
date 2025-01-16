using CrossingKitty.ViewModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Timers;

namespace CrossingKitty;

public partial class GamePlayPage : ContentPage
{
    private readonly Stopwatch _stopwatch;
    private readonly Game _game;
    private readonly Player _player;
    private readonly Random _random = new();
    private readonly List<Coin> _coins = new();
    private readonly List<Obstacle> _obstacles = new();
    private int _rows;
    private int _cols;
    private int _score;
    private int _lives;
    private int GetGridRow(View view) => (int)((view.TranslationY + view.Height / 2) / 100);
    private int GetGridCol(View view) => (int)((view.TranslationX + view.Width / 2) / 100);

    private double GetXPosition(int col) => col * 100;
    private double GetYPosition(int row) => row * 100;


    public GamePlayPage()
	{
		InitializeComponent();

        _stopwatch = new Stopwatch();
        _stopwatch.Start();

        Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            UpdateTime();
            return true;
        });

        _game = new Game();
        _score = 0;

        _player = _game.Player!;
        _rows = _game.GameField!.GridRows;
        _cols = _game.GameField!.GridRows;
        _lives = _player.Lives;

        SetupGrid();

        PlayerImg.TranslateTo((_rows/2)*100, _cols*100);
        GameGrid.Children.Add(PlayerImg);

        AddSwipeGestureRecognizers();
    }

    private void UpdateTime()
    {
        if (_game.IsGameOver)
            _stopwatch.Stop();
        var time = _stopwatch.Elapsed;
        TimeLabel.Text = $"Time: {time.Minutes:D2}:{time.Seconds:D2}";
    }

    private void SetupGrid()
    {
        GameGrid.RowDefinitions.Clear();
        GameGrid.ColumnDefinitions.Clear();

        for (var i = 0; i < _rows; i++)
        {
            GameGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
        }

        for (var i = 0; i < _cols; i++)
        {
            GameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }
    }

    private void UpdateGame(object sender, ElapsedEventArgs e)
    {
        if (!_game.IsGameOver) return;

        if (_random.NextDouble() < 0.05)
            SpawnItem("coin");

        if (_random.NextDouble() < 0.1)
            SpawnItem("obstacle");

        foreach (var coin in _coins.ToArray())
        {
            if (GetGridRow(coin) == _player.Row && GetGridCol(coin) == _player.Col)
            {
                _coins.Remove(coin);
                GameGrid.Children.Remove(coin);
                _score++;
            }
        }

        foreach (var obstacle in _obstacles.ToArray())
        {
            MoveItem(obstacle);
            if (GetGridRow(obstacle) == _player.Row && GetGridCol(obstacle) == _player.Col)
            {
                _obstacles.Remove(obstacle);
                GameGrid.Children.Remove(obstacle);
                _player.Lives--;
                if (_player.Lives <= 0) EndGame();
            }
        }
    }

    private void SpawnItem(string type)
    {
        var row = _random.Next(0, _rows);
        var col = _cols - 1;

        var image = new Image
        {
            Source = type == "coin" ? "coin.png" : "obstacle.png",
            WidthRequest = 50,
            HeightRequest = 50,
        };

        image.TranslateTo(row, col);

        GameGrid.Children.Add(image);
    }

    private void MoveItem(Image item)
    {
        var col = GetGridCol(item) - 1;
        if (col < 0)
        {
            if (_obstacles.Contains(item)) _obstacles.Remove((Obstacle)item);
            GameGrid.Children.Remove(item);
        }
        else
        {
            item.TranslationX = GetXPosition(col);
        }
    }

    private void EndGame()
    {
        _game.IsGameOver = true;
        _stopwatch.Stop();

       
        DisplayAlert("Game Over", $"Score: {_score}", "Restart");
        var game = new Game();
    }

    private void AddSwipeGestureRecognizers()
    {
        BoxView boxView = new BoxView { Color = Colors.Teal };
        SwipeGestureRecognizer leftSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
        leftSwipeGesture.Swiped += OnSwiped;
        SwipeGestureRecognizer rightSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
        rightSwipeGesture.Swiped += OnSwiped;
        SwipeGestureRecognizer upSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Up };
        upSwipeGesture.Swiped += OnSwiped;
        SwipeGestureRecognizer downSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Down };
        downSwipeGesture.Swiped += OnSwiped;

        boxView.GestureRecognizers.Add(leftSwipeGesture);
        boxView.GestureRecognizers.Add(rightSwipeGesture);
        boxView.GestureRecognizers.Add(upSwipeGesture);
        boxView.GestureRecognizers.Add(downSwipeGesture);
    }

    void OnSwiped(object sender, SwipedEventArgs e)
    {
        if (e.Direction == SwipeDirection.Up && _player.Row > 0)
            _player.Row--;
        else if (e.Direction == SwipeDirection.Down && _player.Row < _game.GameField.GridRows - 1)
            _player.Row++;
        else if (e.Direction == SwipeDirection.Left && _player.Col > 0)
            _player.Col--;
        else if (e.Direction == SwipeDirection.Right && _player.Col < _game.GameField.GridCols - 1)
            _player.Col++;



        PlayerImg.TranslateTo(_player.Col * 100, _player.Row * 100);
    }
}


