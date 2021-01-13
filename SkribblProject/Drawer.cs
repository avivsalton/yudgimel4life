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
        bool isEraser = false;
        Controls.ColorAction color;
        Texture2D cursor;
        Texture2D eraser;
        Vector2 cursorPos;
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

            //c = new Client("10.100.102.147", 6967);
            //c.Connect();

            Window.Title = "Drawer";
            
            color = new Controls.ColorAction(GraphicsDevice);

            cursor = new Texture2D(GraphicsDevice, 10, 10);
            cursor = new Texture2D(GraphicsDevice, 20, 20);

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
                new Controls.Button(Content.Load<Texture2D>("button"), new Vector2(200, 0), Color.Green, new Vector2(0, 0)),
                new Controls.Button(Content.Load<Texture2D>("eraser"), new Vector2(250, 0), Color.White, new Vector2(0, 0))
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
                    if (poss.Item1 == pos.Item1 && poss.Item2 == pos.Item2)
                    {
                        toAdd = false;
                        break;
                    }
                }

                if (toAdd)
                {
                    if (!isEraser)
                        positions.Add(pos);
                    else
                    {
                        foreach (Tuple<Vector2, Color> poss in positions)
                        {
                            if (Mouse.GetState().X <= poss.Item1.X + eraser.Width && Mouse.GetState().X >= poss.Item1.X - eraser.Width
                            && Mouse.GetState().Y <= poss.Item1.Y + eraser.Height && Mouse.GetState().Y >= poss.Item1.Y - eraser.Height)
                            {
                                positions.Remove(poss);
                                break;
                            }
                        }
                    }
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
                if (i != 5)
                {
                    if (buttons[i].getStatus() == Controls.Status.Clicked)
                    {
                        currColor = buttons[i].getColor();
                        isEraser = false;
                    }
                }
                else
                {
                    if (buttons[i].getStatus() == Controls.Status.Clicked)
                    {
                        currColor = Color.Black;
                        isEraser = true;
                    }

                }
            }

            color.changeColor(currColor);


            cursorPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            cursor.SetData(color.getData());
            if (!isEraser)
                spriteBatch.Draw(cursor, cursorPos, currColor);
            else
                spriteBatch.Draw(eraser, cursorPos, currColor);

            foreach (Tuple<Vector2, Color> pos in positions)
            {
                color.changeColor(pos.Item2);
                spriteBatch.Draw(color.getRect(), pos.Item1, pos.Item2);
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
