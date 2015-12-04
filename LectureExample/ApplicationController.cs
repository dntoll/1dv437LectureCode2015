#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace LectureExample
{
	/// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ApplicationController : Game
    {
        GraphicsDeviceManager graphics;
        GameModel model;
		GameView view;
		IMGUI imgui;

        public ApplicationController()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";	            
			graphics.IsFullScreen = false;		
			model = new GameModel ();

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
            base.Initialize();
				
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            
            //TODO: use this.Content to load your game content here 
			view = new GameView (model, Content, GraphicsDevice);

			imgui = new IMGUI (Content, GraphicsDevice);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
			imgui.startUI ();

			if (model.IsGameOver ()) {
				//TODO: Meny för att starta om spelet...
				if (imgui.doButton(new Rectangle(200, 200, 100, 100), Keys.Space)) {
					model.RestartGame ();
				}
			} else if (model.HasWon () ) {
				//TODO: Meny för att starta om spelet...
				if (imgui.doButton(new Rectangle(200, 0, 100, 100), Keys.Space)) {
					model.RestartGame ();
				}
			} else {
				InGameUpdate (gameTime);

			} 

			if (imgui.doButton (new Rectangle (0, 0, 100, 100), Keys.Escape)) {
				Exit();
			}

            // TODO: Add your update logic here			
            base.Update(gameTime);
        }

		void InGameUpdate (GameTime gameTime)
		{
			if (Keyboard.GetState ().IsKeyDown (Keys.Down)) {
				model.player.down ();
			}
			if (Keyboard.GetState ().IsKeyDown (Keys.Up)) {
				model.player.up ();
			}
			model.Update ((float)gameTime.ElapsedGameTime.TotalSeconds, view);
		}

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
           	graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
		
            //TODO: Add your drawing code here


			view.DrawGame (GraphicsDevice, (float)gameTime.ElapsedGameTime.TotalSeconds);
			imgui.drawUI ();
            
            base.Draw(gameTime);
        }
    }
}

