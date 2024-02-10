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

    public static float Delta { get; private set; }

    public Game1()
    {
        _inputHandler = new InputHandler.InputHandler(_inputMap);
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
    }

    protected override void Update(GameTime gameTime)
    {
        Delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _inputHandler.InputByActionMap(_player.ActionMap);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        _spriteBatch.Draw(_player.Sprite, _player.Pos, Color.White);

        _spriteBatch.End();
        base.Draw(gameTime);
    }
}