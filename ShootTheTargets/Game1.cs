using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using InputHandler;

namespace ShootTheTargets;

public class Game1 : Game
{
    private readonly Dictionary<string, List<IInputAction>> _inputMap = new()
    {
        { "Up", new List<IInputAction> { new KeyboardAction(Keys.W), new KeyboardAction(Keys.Up), new GamePadAction(Buttons.DPadUp) } },
        { "Down", new List<IInputAction> { new KeyboardAction(Keys.S), new KeyboardAction(Keys.Down), new GamePadAction(Buttons.DPadDown) } },
        { "Left", new List<IInputAction> { new KeyboardAction(Keys.A), new KeyboardAction(Keys.Left), new GamePadAction(Buttons.DPadLeft) } },
        { "Right", new List<IInputAction> { new KeyboardAction(Keys.D), new KeyboardAction(Keys.Right), new GamePadAction(Buttons.DPadRight) } },
        { "MoveAnalog", new List<IInputAction> { new GamePadAction(Buttons.LeftThumbstickRight), new GamePadAction(Buttons.LeftThumbstickLeft), new GamePadAction(Buttons.LeftThumbstickUp), new GamePadAction(Buttons.LeftThumbstickDown) } },
        { "PrimaryAction", new List<IInputAction> { new KeyboardAction(Keys.Space), new MouseClickedAction("Left"), new GamePadAction(Buttons.A), new GamePadAction(Buttons.B) } },
    };

    private readonly InputHandler.InputHandler _inputHandler;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Player _player;
    private List<Target> _targets;
    private Texture2D _targetSprite;
    private Random _rnd;

    public static float Delta { get; private set; }

    public Game1()
    {
        _inputHandler = new InputHandler.InputHandler(_inputMap);
        _targets = new List<Target>();
        _rnd = new Random();
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _player = new Player
        {
            Pos = new Vector2(240, 120)
        };
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _player.Sprite = Content.Load<Texture2D>("images/crosshairs");
        _targetSprite = Content.Load<Texture2D>("images/target");
    }

    protected override void Update(GameTime gameTime)
    {
        Delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (_rnd.Next(200) < 1)
        {
            _targets.Add(new Target(new Vector2(_rnd.Next(600), _rnd.Next(480))));
        }

        _inputHandler.InputByActionMap(_inputHandler.IsActionTriggered("MoveAnalog")
            ? _player.MoveAnalogActionMap
            : _player.MoveDiscreetActionMap);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        foreach (var target in _targets)
        {
            _spriteBatch.Draw(_targetSprite, target.Pos, Color.White);
        }
        _spriteBatch.Draw(_player.Sprite, _player.Pos, Color.White);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}