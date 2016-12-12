using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        int score = 0;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        FallerManager fallerManager;
        Catcher catcher;
        Texture2D catcherTexture;


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
            graphics.PreferredBackBufferWidth = windowWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = windowHeight;   // set this value to the desired height of your window
            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.ApplyChanges();

            catcherTexture = this.Content.Load<Texture2D>("Bucket.png");
            catcher = new Catcher(catcherTexture);

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            catcher.update();
            fallerManager.update(gameTime);

            for (int i = fallerManager.fallerList.Count - 1; i >= 0; i--)
            {
                if (catcher.hitbox.Intersects(fallerManager.fallerList[i].hitbox))
                {
                    fallerManager.fallerList.RemoveAt(i);
                    score += 10;
                    Debug.WriteLine(score);
                }
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

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            catcher.draw(spriteBatch);
            fallerManager.draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
