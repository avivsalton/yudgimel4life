using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;
using System;

namespace SkribblProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Drawer : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 position;
        List<Tuple<Vector2, Color>> positions = new List<Tuple<Vector2, Color>>();
        List<Tuple<Vector2, Color>> sentPos = new List<Tuple<Vector2, Color>>();
        int point = 0;
        Texture2D rectTexture;
        Vector2 cursorPos;
        Color[] data = new Color[10 * 10];
        Client c;
        Controls.Button butt;
        Color currColor = Color.Black;
        Controls.Button[] buttons;
        DrawableObject car;
        Texture2D buttonPic;


        public Drawer()
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
            for (int i = 0; i < data.Length; ++i)
                data[i] = currColor;

            //c = new Client("10.100.102.147", 6967);
            //c.Connect();

            Window.Title = "Drawer";

            rectTexture = new Texture2D(GraphicsDevice, 10, 10);



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
            buttonPic = this.Content.Load<Texture2D>("button");
            buttons = new Controls.Button[]{
                new Controls.Button(Content.Load<Texture2D>("button"), new Vector2(0, 0), Color.Black, new Vector2(0, 0)),
                new Controls.Button(Content.Load<Texture2D>("button"), new Vector2(50, 0), Color.Blue, new Vector2(0, 0)),
                new Controls.Button(Content.Load<Texture2D>("button"), new Vector2(100, 0), Color.Red, new Vector2(0, 0)),
                new Controls.Button(Content.Load<Texture2D>("button"), new Vector2(150, 0), Color.Yellow, new Vector2(0, 0)),
                new Controls.Button(Content.Load<Texture2D>("button"), new Vector2(200, 0), Color.Green, new Vector2(0, 0))
            };

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

            if(Mouse.GetState().LeftButton == ButtonState.Pressed && Mouse.GetState().Y > 50)
            {
                position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

                Tuple<Vector2, Color> pos = new Tuple<Vector2, Color>(position, currColor);
                bool toAdd = true;

                foreach (Tuple<Vector2, Color> poss in positions)
                {
                    if (poss.Item1 == pos.Item1)
                    {
                        toAdd = false;
                        break;
                    }
                }

                if(toAdd)
                {
                    rectTexture.SetData(data);
                    positions.Add(pos);
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].draw(spriteBatch);
                if (buttons[i].getStatus() == Controls.Status.Clicked)
                    currColor = buttons[i].getColor();
            }

            for (int j = 0; j < data.Length; ++j)
            {
                data[j] = currColor;
            }

            cursorPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Texture2D cursor = new Texture2D(GraphicsDevice, 10, 10);
            cursor.SetData(data);
            spriteBatch.Draw(cursor, cursorPos, currColor);

            foreach (Tuple<Vector2, Color> pos in positions)
            {
                spriteBatch.Draw(rectTexture, pos.Item1, pos.Item2);
                if (!sentPos.Contains(pos))
                {
                    //c.Send(Convert.ToString(pos.X) + " " + Convert.ToString(pos.Y));
                    sentPos.Add(pos);
                }
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
