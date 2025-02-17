﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System;

namespace SkribblProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 position;
        ArrayList positions = new ArrayList();
        ArrayList sentPos = new ArrayList();
        int point = 0;
        Texture2D rectTexture;
        Vector2 cursorPos;
        Color[] data = new Color[10 * 10];
        Client c;


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
            for (int i = 0; i < data.Length; ++i)
                data[i] = Color.Black;

            c = new Client("127.0.0.1", 1616);
            c.Connect();

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
            Console.WriteLine(gameTime.ElapsedGameTime.TotalSeconds);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                position = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

                rectTexture.SetData(data);
                if (!positions.Contains(position))
                    positions.Add(position);
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

            cursorPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            Texture2D cursor = new Texture2D(GraphicsDevice, 10, 10);
            cursor.SetData(data);
            spriteBatch.Draw(cursor, cursorPos, Color.Black);

            foreach (Vector2 pos in positions)
            {
                spriteBatch.Draw(rectTexture, pos, Color.Black);
                if (!sentPos.Contains(pos))
                {
                    c.Send(Convert.ToString(pos.X) + "," + Convert.ToString(pos.Y));
                    sentPos.Add(sentPos);
                }
            }

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
