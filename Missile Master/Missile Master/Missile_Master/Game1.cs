using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Missile_Master
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Variables

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region 2DTextures
        Texture2D MainMenuBG;
        Texture2D CreditsBG;
        Texture2D CheatsBG;
        Texture2D OptionsBG;
        Texture2D LevelSelectBG;
        Texture2D Level1BG;
        Texture2D ShopBG;
        Texture2D CampaignBG;
        Texture2D GameoverBG;
        #endregion

        #region Booleans
        bool cheatsActive = false;
        #endregion

        #region Strings

        string[] MainMenuOptions = new string[] { "Campaign", "Level Select", "Shop", "Options", "Exit" };

        #endregion

        #region Fonts
        
        #endregion

        //gamestates
        enum GameStates
        {
            MainMenu,
            Credits,
            Cheats,
            Options,
            LevelSelect,
            #region Levels
            Level1,
            #endregion
            Shop,
            Campaign,
            Gameover
        };

        //starting gamestate
        GameStates gameState = GameStates.MainMenu;
        #endregion
        /*       
           public char InGameControlls()
        {
            bool wHasBeenReleased = true;
            bool aHasBeenReleased = true;
            bool sHasBeenReleased = true;
            bool dHasBeenReleased = true;
            bool spaceHasBeenReleased = true;

        }
        */

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
            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.PreferredBackBufferHeight = 1200;
            this.graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region 2DTextures
            MainMenuBG = Content.Load<Texture2D>(@"Textures/MainMenuBG");
            CreditsBG = Content.Load<Texture2D>(@"Textures/CreditsBG");
            CheatsBG = Content.Load<Texture2D>(@"Textures/CheatsBG");
            OptionsBG = Content.Load<Texture2D>(@"Textures/OptionsBG");
            LevelSelectBG = Content.Load<Texture2D>(@"Textures/LevelSelectBG");
            Level1BG = Content.Load<Texture2D>(@"Textures/Level1BG");
            ShopBG = Content.Load<Texture2D>(@"Textures/ShopBG");
            CampaignBG = Content.Load<Texture2D>(@"Textures/CampaignBG");
            GameoverBG = Content.Load<Texture2D>(@"Textures/GameoverBG");
            /// = Content.Load<Texture2D>(@"Textures/");

            #endregion


            RobotoRegular = Content.Load<SpriteFont>("res/RobotoRegular");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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

            GamePadState gamepad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboard = Keyboard.GetState();

            #region Game Exit
            //back or End exits the game
            if (gamepad.Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.End))
                this.Exit();

            //f to toggle fullscreen
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }
            #endregion

            switch (gameState)
            {
                #region Mainmenu
                case GameStates.MainMenu:

                    if (Keyboard.GetState().IsKeyDown(Keys.C)) //C to enter Credits
                    {
                        gameState = GameStates.Credits;
                    }


                    break;
                #endregion 

                #region Credits
                case GameStates.Credits:

                    ESCtoMainMenu(); //esc or backspace to return to Mainmenu

                    if (Keyboard.GetState().IsKeyDown(Keys.Insert)) //insert goes to activatecheats screen
                    {
                        gameState = GameStates.Cheats;
                    }
                    break;
                #endregion

                #region Cheats
                case GameStates.Cheats:
                    if (Keyboard.GetState().IsKeyDown(Keys.PageUp)) // Activate cheats
                    {
                        cheatsActive = true;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.PageDown)) // Deactivate cheats
                    {
                        cheatsActive = false;
                    }

                    ESCtoMainMenu(); //esc or backspace to return to Mainmenu

                    break;
                #endregion

                #region Options
                case GameStates.Options:

                    ESCtoMainMenu(); //esc or backspace to return to Mainmenu

                    break;
                #endregion

                #region LevelSelect
                case GameStates.LevelSelect:

                    ESCtoMainMenu(); //esc or backspace to return to Mainmenu

                    break;
                #endregion

                #region Level1
                case GameStates.Level1:
                    break;
                #endregion

                #region Shop
                case GameStates.Shop:

                    ESCtoMainMenu(); //esc or backspace to return to Mainmenu


                    break;
                #endregion

                #region Campaign
                case GameStates.Campaign:

                    ESCtoMainMenu(); //esc or backspace to return to Mainmenu


                    break;
                #endregion

                #region Gameover
                case GameStates.Gameover:

                    ESCtoMainMenu(); //esc or backspace to return to Mainmenu

                    break;
                    #endregion

            }





            base.Update(gameTime);
        }
        public void ESCtoMainMenu ()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.Back)) //esc or backspace to return to Mainmenu
            {
                gameState = GameStates.MainMenu;
            }

        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            switch (gameState)
            {
                case GameStates.MainMenu:

                        spriteBatch.Draw( //Background
                            MainMenuBG,
                            new Rectangle(0, 0,
                            this.Window.ClientBounds.Width,
                            this.Window.ClientBounds.Height),
                            Color.White);
                    break;

                case GameStates.Credits:
            
                    spriteBatch.Draw( //Background
                        CreditsBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);

                    break;

                case GameStates.Cheats:

                    spriteBatch.Draw( //Background
                        CheatsBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);


                    break;

                case GameStates.Options:

                    spriteBatch.Draw( //Background
                        OptionsBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);


                    break;

                case GameStates.LevelSelect:

                    spriteBatch.Draw( //Background
                        LevelSelectBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);


                    break;

                case GameStates.Level1:

                    spriteBatch.Draw( //Background
                        Level1BG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);


                    break;

                case GameStates.Shop:

                    spriteBatch.Draw( //Background
                        ShopBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);


                    break;

                case GameStates.Campaign:

                    spriteBatch.Draw( //Background
                        CampaignBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);


                    break;

                case GameStates.Gameover:

                    spriteBatch.Draw( //Background
                        GameoverBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);


                    break;
/*
*                case GameStates.:
*
                    spriteBatch.Draw( //Background
                        BG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);


                    break;
*/
            }

            spriteBatch.End();
                // TODO: Add your drawing code here

                base.Draw(gameTime);
            
        }
    }
}
