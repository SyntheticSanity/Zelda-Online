using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ZeldaOnline;

namespace ZeldaOnline {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch BaseSpriteBatch;

        int GameServicesIndex = 0;

        public Game() {
            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = true;
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferMultiSampling = false;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            Components.Add(new GamerServicesComponent(this));
            GameServicesIndex = Components.IndexOf(Components.Last());
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            this.IsMouseVisible = true;

            S.Tick += new EventHandler<GameEventArgs>(A.Tick);

            base.Initialize();

            S.KBO = Keyboard.GetState();
            S.KB = Keyboard.GetState();
            S.KBH = new Dictionary<Keys, Boolean>();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            S.BufferTarget = new RenderTarget2D(GraphicsDevice, (int)(GraphicsDevice.Viewport.Width / S.UIScale), (int)(GraphicsDevice.Viewport.Height / S.UIScale), false, GraphicsDevice.DisplayMode.Format, DepthFormat.Depth24, 1, RenderTargetUsage.PreserveContents);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            S.BufferRect = S.BufferTarget.Bounds;

            S.GameFont = Content.Load<SpriteFont>("gfx/GameFont");
            S.Tileset = Content.Load<Texture2D>("gfx/tiles");
            S.UITiles = Content.Load<Texture2D>("gfx/ui");
            S.White = Content.Load<Texture2D>("gfx/white");

            DialogueBox.TW = new TextWindow(
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed ac magna odio, a `>cf0tempor leo`<. Proin commodo venenatis congue. Sed vitae `>09fpurus`< tellus, vitae egestas urna. Maecenas et nisl nulla, in facilisis sem. Mauris aliquam scelerisque magna, sed faucibus velit mattis et. Vestibulum aliquam, turpis vel hendrerit auctor, tellus nunc laoreet sapien, sed bibendum tortor lorem ac purus. Sed non porta leo. Nulla arcu nibh, tempor et blandit a, ultrices euismod elit. Phasellus sed erat libero. Phasellus tincidunt nisi metus. Suspendisse ut turpis quam. Nullam ullamcorper felis eu justo viverra porta. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Praesent bibendum lobortis justo, nec commodo ante tincidunt ut. In id massa augue. Nunc viverra libero sed quam tempor vehicula.",
                10,
                new Rectangle(
                    (int)(32),
                    (int)(S.BufferRect.Height - (4 * (S.GameFont.LineSpacing + 8) + 32)),
                    (int)(S.BufferRect.Width - 64),
                    (int)(3 * S.GameFont.LineSpacing + 8 + 32)
                ),
                3
            );
            DialogueBox.TW.Show = true;
            DialogueBox.TW.Done += delegate(object obj, EventArgs e) {
                DialogueBox.TW.Show = false;
            };
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
            A.Sfx.Clear();
            A.SE.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Retrieve the keyboard state and clear any handled messages from last update.
            S.KB = Keyboard.GetState();
            S.KBH.Clear();

            // Perform game logic.

            // Handle dialogue input overriding.
            if (KeyTest(Keys.Enter)) {
                if (DialogueBox.TW.Show == true) {
                    DialogueBox.TW.FastCarat = true;
                    if (!S.KBO.IsKeyDown(Keys.Enter) && DialogueBox.TW.CaratEnd) {
                        DialogueBox.TW.Page++;
                        A.PlaySfx("text_blip");
                    }
                } else {
                    if (!S.KBO.IsKeyDown(Keys.Enter))
                    DialogueBox.TW.Show = true;
                }
                KeyHandled(Keys.Enter);
            } else {
                DialogueBox.TW.FastCarat = false;
            }
 
            // Handle all the tick events tied to the static tick handler.
            S.Tock(this, new GameEventArgs(gameTime));

            // Store the current keyboard state in the old state variable.
            S.KBO = Keyboard.GetState();
            base.Update(gameTime);
        }

        public bool KeyTest(Keys Key) {
            if (Microsoft.Xna.Framework.GamerServices.Guide.IsVisible == true) return false;
            if (S.KB.IsKeyDown(Key) && (!S.KBH.ContainsKey(Key) || !S.KBH[Key])) return true; else return false;
        }
        public void KeyHandled(Keys Key) {
            if (S.KBH.ContainsKey(Key)) S.KBH[Key] = true; else S.KBH.Add(Key, true);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {

            GraphicsDevice.SetRenderTarget(S.BufferTarget);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone);

            for (int y = 0; y < 32; y++) {
                for (int x = 0; x < 32; x++) {
                    spriteBatch.Draw(S.Tileset, new Rectangle(x * 8, y * 8, 8, 8), new Rectangle(0, 0, 8, 8), Color.FromNonPremultiplied(255 - 2 * y, 255 - 2 * x, 255, 255), 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                    spriteBatch.Draw(S.Tileset, new Rectangle(x * 8, y * 8, 8, 8), new Rectangle(x * 8, y * 8, 8, 8), Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
                }
            }

            if (DialogueBox.TW.Show == true) DialogueBox.TW.Draw(spriteBatch);

            spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone);

            spriteBatch.Draw((Texture2D)S.BufferTarget, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
