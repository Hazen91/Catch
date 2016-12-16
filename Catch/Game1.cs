using Catch.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Catch
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static int windowWidth = 1280;
        public static int windowHeight = 720;

        public enum gameState { mainMenu, paused, playing, gameOver };

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        StartButton startButton;
        ExitButton exitButton;
        MouseState mouseState = Mouse.GetState();
        MouseState oldMouseState = Mouse.GetState();
        KeyboardState keyboardState = Keyboard.GetState();
        KeyboardState oldKeyboardState = Keyboard.GetState();

        private SpriteFont font;

        FallerManager fallerManager;
        Catcher catcher;
        Texture2D catcherTexture;

        Texture2D backgroundImage;
        Texture2D startButtonTexture;
        Texture2D exitButtonTexture;
        Texture2D speedBarTexture;
        Bar speedBar;

        SoundEffect fireball;
        SoundEffectInstance soundEffectInstance;

        private static gameState currentState = gameState.mainMenu;

        public static gameState CurrentState
        {
            get
            {
                return currentState;
            }

            set
            {
                currentState = value;
            }
        }

        private static int lifes = 3;

        public static int Lifes
        {
            get
            {
                return lifes;
            }

            set
            {
                lifes = value;
            }
        }

        private static int score = 0;

        public static int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }

        public void quit()
        {
            this.Exit();
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = windowWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = windowHeight;   // set this value to the desired height of your window
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();

            font = Content.Load<SpriteFont>("Arial");

            backgroundImage = this.Content.Load<Texture2D>("hubblebackground.png");

            catcherTexture = this.Content.Load<Texture2D>("Ship.png");
            catcher = new Catcher(catcherTexture);

            startButtonTexture = this.Content.Load<Texture2D>("startButton.png");
            exitButtonTexture = this.Content.Load<Texture2D>("quitButton.png");
            startButton = new StartButton(Content, startButtonTexture, new Vector2(windowWidth / 2 - startButtonTexture.Width / 2, 50));
            exitButton = new ExitButton(Content, exitButtonTexture, new Vector2(windowWidth / 2 - startButtonTexture.Width / 2, 250));
            speedBarTexture = Content.Load<Texture2D>("bars.png");
            speedBar = new Bar(speedBarTexture);

            fireball = Content.Load<SoundEffect>("Fireball");
            soundEffectInstance = fireball.CreateInstance();
            soundEffectInstance.Volume = 0.2f;

            fallerManager = new FallerManager(this.Content);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            { Exit(); }


            switch (CurrentState)
            {
                case gameState.mainMenu:
                    this.IsMouseVisible = true;

                    mouseState = Mouse.GetState();
                    
                    if (mouseState.LeftButton == ButtonState.Pressed && startButton.hitbox.Contains(mouseState.X, mouseState.Y) && oldMouseState.LeftButton == ButtonState.Released)
                    {
                        startButton.click(fallerManager);
                    }
                    if (mouseState.LeftButton == ButtonState.Pressed && exitButton.hitbox.Contains(mouseState.X, mouseState.Y) && oldMouseState.LeftButton == ButtonState.Released)
                    {
                        exitButton.click(this);
                    }

                    oldMouseState = mouseState;
                    
                    break;

                case gameState.playing:
                    this.IsMouseVisible = false;
                    catcher.update(gameTime);
                    fallerManager.update(gameTime);
                    speedBar.Update((int) catcher.Velocity);

                    for (int i = fallerManager.fallerList.Count - 1; i >= 0; i--)
                    {
                        if (catcher.hitbox.Intersects(fallerManager.fallerList[i].hitbox))
                        {
                            if (fallerManager.fallerList[i].GetType() == typeof(Fallers.SpeedPowerUp))
                            {
                                catcher.Velocity += 100;
                                
                            }
                            else
                            {
                                soundEffectInstance.Play();
                                Score += (int) (((fallerManager.fallerList[i].Velocity.Y / 8) + (Math.Abs(fallerManager.fallerList[i].Velocity.X) / 10)) / 5);
                            }

                            fallerManager.fallerList.RemoveAt(i);
                            
                        }
                        else if (fallerManager.fallerList[i].hitbox.Location.Y >= windowHeight)
                        {
                            if (fallerManager.fallerList[i].GetType() == typeof(Fallers.Star))
                            { Lifes -= 1; }

                            fallerManager.fallerList.RemoveAt(i);
                            
                            if (Lifes <= 0)
                            {
                                currentState = gameState.gameOver;
                            }
                        }
                    }

                    keyboardState = Keyboard.GetState();
                    if (oldKeyboardState.IsKeyUp(Keys.P) && keyboardState.IsKeyDown(Keys.P))
                    {
                        if (CurrentState == gameState.playing)
                        {
                            currentState = gameState.paused;
                        }
                        
                    }
                    oldKeyboardState = keyboardState;
                    break;

                case gameState.paused:
                    keyboardState = Keyboard.GetState();
                    if (oldKeyboardState.IsKeyUp(Keys.P) && keyboardState.IsKeyDown(Keys.P))
                    {
                        if (CurrentState == gameState.paused)
                        {
                            currentState = gameState.playing;
                        }
                    }
                    oldKeyboardState = keyboardState;
                    break;

                case gameState.gameOver:
                    this.IsMouseVisible = true;

                    mouseState = Mouse.GetState();

                    if (mouseState.LeftButton == ButtonState.Pressed && startButton.hitbox.Contains(mouseState.X, mouseState.Y) && oldMouseState.LeftButton == ButtonState.Released)
                    {
                        startButton.click(fallerManager);
                    }
                    if (mouseState.LeftButton == ButtonState.Pressed && exitButton.hitbox.Contains(mouseState.X, mouseState.Y) && oldMouseState.LeftButton == ButtonState.Released)
                    {
                        exitButton.click(this);
                    }

                    oldMouseState = mouseState;
                    break;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (CurrentState)
            {
                
                case gameState.mainMenu:
                    this.IsMouseVisible = true;
                    spriteBatch.Begin();
                    startButton.draw(spriteBatch);
                    exitButton.draw(spriteBatch);
                    spriteBatch.End();
                    break;

                case gameState.playing:
                    this.IsMouseVisible = false;
                    spriteBatch.Begin();
                    spriteBatch.Draw(backgroundImage, new Vector2(0, 0), Color.White);
                    catcher.draw(spriteBatch);
                    fallerManager.draw(spriteBatch);

                    spriteBatch.DrawString(font,"Score: "+Score, new Vector2(20, 8),Color.White);
                    spriteBatch.DrawString(font, "Lifes: " + Lifes+"          Speed:", new Vector2(150, 8), Color.White);
                    speedBar.Draw(spriteBatch, new Vector2(300, 8));
                    spriteBatch.End();
                    break;

                case gameState.gameOver:
                    this.IsMouseVisible = true;
                    spriteBatch.Begin();
                    spriteBatch.DrawString(font, "Game Over", new Vector2(350, 0), Color.Black);
                    spriteBatch.DrawString(font, "Score: "+Score, new Vector2(450, 0), Color.Black);
                    startButton.draw(spriteBatch);
                    exitButton.draw(spriteBatch);
                    spriteBatch.End();
                    break;
            }

            

            base.Draw(gameTime);
        }
    }
}
