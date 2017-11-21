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
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        #region Variables

        #region Floats
        const float defaultTurnRate = 0.6f;
        const float defaultThrust = 10f;
        #endregion

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
        SpriteFont RobotoRegular36;
        #endregion

        #region Vectors

        Vector2 playerSpeed = new Vector2(0, 0);
        Vector2 playerPosition = new Vector2(0, 0);

        #endregion

        #region UpgradeStates
        float mainThrusterLVL = 1f;
        int bodyLVL = 1;
        float sideThrusterLVL = 1f;
        int payloadLVL = 1;
        int fuelLVL = 1;
        int DecoyLVL = 1;
        #endregion

        #region States
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

        //RotationStates
        enum RotationStates
        {
            D0,D45,D90,D135,D180,D225,D270,D315 //D for degrees
        };

        //starting RotationState
        RotationStates rotationstate = RotationStates.D0;
        #endregion 

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

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


            RobotoRegular36 = Content.Load<SpriteFont>(@"Fonts/Roboto/RobotoRegular36");

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {

            GamePadState gamepad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboard = Keyboard.GetState();

            #region Game Exit
            //back or End exits the game
            if (gamepad.Buttons.Back == ButtonState.Pressed || keyboard.IsKeyDown(Keys.End))
                this.Exit();
            #endregion

            #region Fullscreen
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
                    Controls(gameTime);
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

        public void ESCtoMainMenu()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.Back)) //esc or back to return to Mainmenu
            {
                gameState = GameStates.MainMenu;
            }
        }

        public void Controls(GameTime gameTime) //---------------IMPORTANT----------------------------------------------
        {
            float mainThrusterPower = mainThrusterLVL * 30;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                //accelerate
                switch (rotationstate)
                {
                    case RotationStates.D0:
                        //TODO lägg till luftmotstånd
                        playerSpeed.Y += defaultThrust * mainThrusterLVL;
                        break;

                    case RotationStates.D45:
                        playerSpeed.Y += (defaultThrust * mainThrusterLVL) / 2;
                        playerSpeed.X += (defaultThrust * mainThrusterLVL) / 2;
                        break;

                    case RotationStates.D90:
                        playerSpeed.X += defaultThrust * mainThrusterLVL;
                        break;

                    case RotationStates.D135:
                        playerSpeed.Y += ((defaultThrust * mainThrusterLVL) / 2) * -1;
                        playerSpeed.X += (defaultThrust * mainThrusterLVL) / 2;
                        break;

                    case RotationStates.D180:
                        playerSpeed.Y += defaultThrust * mainThrusterLVL * -1;
                        break;

                    case RotationStates.D225:
                        playerSpeed.Y += (defaultThrust * mainThrusterLVL) / 8;
                        playerSpeed.X += (defaultThrust * mainThrusterLVL) / 8;
                        break;

                    case RotationStates.D270:
                        playerSpeed.X += (defaultThrust * mainThrusterLVL) / 8;
                        break;

                    case RotationStates.D315:
                        playerSpeed.Y += (defaultThrust * mainThrusterLVL) * 1;
                        playerSpeed.X += (defaultThrust * mainThrusterLVL) * 7;
                        break;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                //turn counter-clockwise
                float countDuration = defaultTurnRate / sideThrusterLVL; //time until switch rotationstate
                float currentTime = 0f;

                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //Time passed since last Update() 

                if (currentTime >= countDuration)
                {
                    switch (rotationstate)
                    {
                        case RotationStates.D0:
                            rotationstate = RotationStates.D315;
                            break;

                        case RotationStates.D45:
                            rotationstate = RotationStates.D0;
                            break;

                        case RotationStates.D90:
                            rotationstate = RotationStates.D45;
                            break;

                        case RotationStates.D135:
                            rotationstate = RotationStates.D90;
                            break;

                        case RotationStates.D180:
                            rotationstate = RotationStates.D135;
                            break;

                        case RotationStates.D225:
                            rotationstate = RotationStates.D180;
                            break;

                        case RotationStates.D270:
                            rotationstate = RotationStates.D225;
                            break;

                        case RotationStates.D315:
                            rotationstate = RotationStates.D270;
                            break;

                    }
                    currentTime -= countDuration; // "use up" the time
                    
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                //decelerate
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                // turn clockwise
                float countDuration = defaultTurnRate / sideThrusterLVL; //every  2s.
                float currentTime = 0f;

                currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds; //Time passed since last Update() 

                if (currentTime >= countDuration)
                {
                    switch (rotationstate)
                    {
                        case RotationStates.D0:
                            rotationstate = RotationStates.D45;
                            break;

                        case RotationStates.D45:
                            rotationstate = RotationStates.D90;
                            break;

                        case RotationStates.D90:
                            rotationstate = RotationStates.D135;
                            break;

                        case RotationStates.D135:
                            rotationstate = RotationStates.D180;
                            break;

                        case RotationStates.D180:
                            rotationstate = RotationStates.D225;
                            break;

                        case RotationStates.D225:
                            rotationstate = RotationStates.D270;
                            break;

                        case RotationStates.D270:
                            rotationstate = RotationStates.D315;
                            break;

                        case RotationStates.D315:
                            rotationstate = RotationStates.D0;
                            break;

                    }
                    currentTime -= countDuration; // "use up" the time

                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            switch (gameState)
            {
                #region MainMenu
                case GameStates.MainMenu:

                    spriteBatch.Draw( //Background
                        MainMenuBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);
                    break;
                #endregion


                #region Credits
                case GameStates.Credits:

                    spriteBatch.Draw( //Background
                        CreditsBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);

                    break;
                #endregion


                #region Cheats
                case GameStates.Cheats:

                    spriteBatch.Draw( //Background
                        CheatsBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);


                    break;
                #endregion


                #region Options
                case GameStates.Options:

                    spriteBatch.Draw( //Background
                        OptionsBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);


                    break;
                #endregion

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
                base.Draw(gameTime);
            
        }
    }
}
