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
        const float defaultThrusterPower = 10f;
        float playerRotation = 0.0f;
        const float circle = 6.28318f;
        float gravity = 0.3f;
        #endregion

        #region Integers
        int selectionIndex;

        int selected = 0;

        #endregion

        #region 2DTextures
        Texture2D MainMenuBG;
        Texture2D CreditsBG;
        Texture2D CheatsBG;
        Texture2D OptionsBG;
        Texture2D LevelSelectBG;
        Texture2D ShopBG;
        Texture2D CampaignBG;
        Texture2D GameoverBG;

        #region Level1
        Texture2D Level1BG;
        Texture2D RocketTest;
        #endregion

        #endregion

        #region Booleans
        bool cheatsActive = false;
        bool KeyIsUp = false;

        #endregion

        #region Strings

        string[] MainMenuOptions = new string[] { "Campaign", "Level Select", "Shop", "Options", "Exit" };

        #endregion

        #region Fonts
        SpriteFont RobotoRegular36;
        SpriteFont RobotoBold36;
        #endregion

        #region Vectors

        Vector2 playerSpeed = new Vector2(0, 0);
        Vector2 playerPosition = new Vector2(300, 550);
        Vector2 rocketOrigin = new Vector2(0, 0);

        #endregion

        #region Rectangles
        Rectangle playerRectangle = new Rectangle();
        #endregion

        #region UpgradeStates
        float mainThrusterLVL = 4f;
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
            Gameover,
            Exit
        };

        //starting gamestate
        GameStates gameState = GameStates.MainMenu;
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
            this.graphics.PreferredBackBufferWidth = 1600;
            this.graphics.PreferredBackBufferHeight = 900;
            this.graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            #region 2DTextures
            MainMenuBG = Content.Load<Texture2D>(@"Textures/MainMenuBG");
            CreditsBG = Content.Load<Texture2D>(@"Textures/CreditsBG");
            CheatsBG = Content.Load<Texture2D>(@"Textures/CheatsBG");
            OptionsBG = Content.Load<Texture2D>(@"Textures/OptionsBG");
            LevelSelectBG = Content.Load<Texture2D>(@"Textures/LevelSelectBG");
            ShopBG = Content.Load<Texture2D>(@"Textures/ShopBG");
            CampaignBG = Content.Load<Texture2D>(@"Textures/CampaignBG");
            GameoverBG = Content.Load<Texture2D>(@"Textures/GameoverBG");

            #region Level1
            Level1BG = Content.Load<Texture2D>(@"Textures/Level1BG");
            RocketTest = Content.Load<Texture2D>(@"Textures/RocketTest");

            #endregion

            /// = Content.Load<Texture2D>(@"Textures/");

            #endregion


            RobotoRegular36 = Content.Load<SpriteFont>(@"Fonts/Roboto/RobotoRegular36");
            RobotoBold36 = Content.Load<SpriteFont>(@"Fonts/Roboto/RobotoBold36");

            rocketOrigin.X = RocketTest.Width / 2;
            rocketOrigin.Y = RocketTest.Height / 2;

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

            float countDuration = 0.3f; // in seconds
            float currentTime = 0f;

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            Console.WriteLine(currentTime);
            if (currentTime >= countDuration)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) //detect if key is down
                {
                    if (selected < selectionIndex) { selected++; Console.WriteLine(selected); currentTime -= countDuration; }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    if (selected > 0) { selected--; Console.WriteLine(selected); currentTime -= countDuration; }
                }
            }

            switch (gameState)
            {
                #region Mainmenu
                case GameStates.MainMenu:

                    selectionIndex = 5; //number of menu options

                     #region Menu Controls
                    if (Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.Up)) { KeyIsUp = true; } // detect if key is up
                    if (KeyIsUp)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) //detect if key is down
                        {
                            KeyIsUp = false;
                            if (selected < selectionIndex) { selected++; }
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            KeyIsUp = false;
                            if (selected > 0) { selected--; }
                        }
                    }


                    if (Keyboard.GetState().IsKeyDown(Keys.Enter)) // menu options
                    {
                        switch (selected) {
                            case 0: gameState = GameStates.Campaign;
                                break;
                            case 1: gameState = GameStates.LevelSelect;
                                break;
                            case 2: gameState = GameStates.Shop;
                                break;
                            case 3: gameState = GameStates.Options;
                                break;
                            case 4: gameState = GameStates.Credits;
                                break;
                            case 5: gameState = GameStates.Exit;
                                break;
                                }
                    }
                    #endregion

                    if (Keyboard.GetState().IsKeyDown(Keys.C)) //C to enter Credits
                    {
                        gameState = GameStates.Credits;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.NumPad1)) //numpad1 to enter Level1
                    {
                        gameState = GameStates.Level1;
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

                    #region Menu Controls
                    if (Keyboard.GetState().IsKeyUp(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.Down) && Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.Up)) { KeyIsUp = true; } // detect if key is up
                    if (KeyIsUp)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyDown(Keys.Down)) //detect if key is down
                        {
                            KeyIsUp = false;
                            if (selected < selectionIndex) { selected++; }
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.W) || Keyboard.GetState().IsKeyDown(Keys.Up))
                        {
                            KeyIsUp = false;
                            if (selected > 0) { selected--; }
                        }
                    }


                    if (Keyboard.GetState().IsKeyDown(Keys.Enter)) // menu options
                    {
                        switch (selected)
                        {
                            case 0:
                                gameState = GameStates.Campaign;
                                break;
                            case 1:
                                gameState = GameStates.LevelSelect;
                                break;
                            case 2:
                                gameState = GameStates.Shop;
                                break;
                            case 3:
                                gameState = GameStates.Options;
                                break;
                            case 4:
                                gameState = GameStates.Credits;
                                break;
                            case 5:
                                gameState = GameStates.Exit;
                                break;
                        }
                    }
                    #endregion


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
                    #region Physics
                    float mainThrusterPower = (float)Math.Pow(mainThrusterLVL, 1.5f); //total thruster power

                    playerPosition += playerSpeed;
                    //TODO add air-resistance
                    // gravity
                    playerSpeed.Y += gravity;

                    //used for deciding thrust ratios
                    float ThrustRatioX;
                    float ThrustRatioY;

                    // % of circle
                    ThrustRatioY = playerRotation / circle;
                    ThrustRatioX = 1 - playerRotation / circle;

                    //ensure player rotation stays within the limit
                    if (playerRotation < 0) { playerRotation = circle; }
                    if (playerRotation > circle) { playerRotation = 0; }

                    float rotationRatio = ThrustRatioY;
                    Console.WriteLine(ThrustRatioX);
                    Console.WriteLine(ThrustRatioY);
                    #endregion 


                    #region W key
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        //accelerate
                        //  -Y +X
                        if (playerRotation > 0.25 && playerRotation < 0.75) {
                            playerSpeed.Y = mainThrusterPower * ThrustRatioY * -1;
                            playerSpeed.X = mainThrusterPower * ThrustRatioX; 
                        }
                        





                    }
                    #endregion

                    #region A key
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        //rotate counter-clockwise
                        playerRotation -= 0.01234f;
                    }
                    #endregion

                    #region S key
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        //decelerate
                        playerSpeed.Y *= 0.1f;
                    }
                    #endregion

                    #region D key
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        //rotate clockwise
                        playerRotation += 0.01234f;

                    }
                    #endregion

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

                #region Exit
                case GameStates.Exit:
                    this.Exit();
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

                    //campaign
                    string campaign = "Campaign";
                    Vector2 campaingOrigin = RobotoRegular36.MeasureString(campaign) / 2;
                    Vector2 campaingPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 100 * 15);
                    if (selected == 0)
                    {
                        spriteBatch.DrawString(RobotoBold36, campaign, campaingPos, Color.White,
                                0, campaingOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawString(RobotoRegular36, campaign, campaingPos, Color.White,
                                0, campaingOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }

                    //Level select
                    string levelSelect = "Level Select";
                    Vector2 levelSelectOrigin = RobotoRegular36.MeasureString(levelSelect) / 2;
                    Vector2 levelSelectPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 100 * 25);
                    if (selected == 1)
                    {
                        spriteBatch.DrawString(RobotoBold36, levelSelect, levelSelectPos, Color.White,
                                0, levelSelectOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawString(RobotoRegular36, levelSelect, levelSelectPos, Color.White,
                                0, levelSelectOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }

                    //shop
                    string shop = "Shop";
                    Vector2 shopOrigin = RobotoRegular36.MeasureString(shop) / 2;
                    Vector2 shopPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 100 * 35);
                    if (selected == 2)
                    {
                        spriteBatch.DrawString(RobotoBold36, shop, shopPos, Color.White,
                                0, shopOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawString(RobotoRegular36, shop, shopPos, Color.White,
                                0, shopOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }

                    //options
                    string options = "Options";
                    Vector2 optionsOrigin = RobotoRegular36.MeasureString(options) / 2;
                    Vector2 optionsPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 100 * 45);
                    if (selected == 3)
                    {
                        spriteBatch.DrawString(RobotoBold36, options, optionsPos, Color.White,
                                0, optionsOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawString(RobotoRegular36, options, optionsPos, Color.White,
                                0, optionsOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }

                    //Credits
                    string credits = "Credits";
                    Vector2 creditsOrigin = RobotoRegular36.MeasureString(credits) / 2;
                    Vector2 creditsPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 100 * 55);
                    if (selected == 4)
                    {
                        spriteBatch.DrawString(RobotoBold36, credits, creditsPos, Color.White,
                                0, creditsOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawString(RobotoRegular36, credits, creditsPos, Color.White,
                                0, creditsOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }

                    //Exit
                    string exit = "Exit";
                    Vector2 exitOrigin = RobotoRegular36.MeasureString(exit) / 2;
                    Vector2 exitPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 100 * 65);
                    if (selected == 5)
                    {
                        spriteBatch.DrawString(RobotoBold36, exit, exitPos, Color.White,
                                0, exitOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
                    else
                    {
                        spriteBatch.DrawString(RobotoRegular36, exit, exitPos, Color.White,
                                0, exitOrigin, 1.0f, SpriteEffects.None, 0.5f);
                    }
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

                    spriteBatch.Draw(RocketTest, //Rocket
                        playerPosition,
                        null,
                        Color.Black,
                        playerRotation,
                        rocketOrigin,
                        1.0f,
                        SpriteEffects.None,
                        0f);
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
            }

            spriteBatch.End();
                base.Draw(gameTime);
            
        }
    }
}
