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

        #region Player
        Texture2D PlayerTexture;
        float playerAccel;
        float playerAngle;
        float playerMaxSpeed = 10;
        float playerTurnRate = 0.04f;
        Vector2 playerOrigin;
        Vector2 playerPos;
        Vector2 playerDirection;
        bool playerDead = false;
        Rectangle playerRect;
        #endregion

        #region Floats
        float gravity = 0.1f;
        float gravityMomentum; 
        float airResistence = 0.1f;
        float windowMaxX;
        float windowMaxY;
        float mainThrusterPower;
        float sideThrusterPower;
        float totalFuel;
        float fuel;
        #endregion

        #region Integers
        int selectionIndex;
        int selected = 0;
        long money = 0; // it is 64bit to ensure player can have a lot of money
        const int coin1Size = 24;
        const int defaultfuel = 10000;

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
        Texture2D Explosion1Tex;
        Texture2D Coin1;
        #region Level1
        Texture2D Level1BG;
        Texture2D Building1;
        Rectangle building1Rect;
        #endregion

        #endregion

        #region Booleans
        bool cheatsActive = false; // TODO : Create Cheats
        bool KeyIsUp = false;
        bool firstRun = true;
        bool building1Dead;
        bool coin1IsActive_0;
        bool coin1IsActive_1;
        bool coin1IsActive_2;
        bool coin1IsActive_3;
        bool coin1IsActive_4;
        bool coin1IsActive_5;
        bool coin1IsActive_6;
        bool coin1IsActive_7;
        bool coin1IsActive_8;
        bool coin1IsActive_9;
        bool GameIsActive = false;
        #endregion

        #region Strings

        // TODO : string[] MainMenuOptions = new string[] { "Campaign", "Level Select", "Shop", "Options", "Exit" };
        string moneyStr;
        string fuelStr;
        #endregion

        #region Soundeffects
        SoundEffect Explosion1Sound;
        SoundEffect Coin1Sound;
        #endregion

        #region Fonts
        SpriteFont RobotoRegular36;
        SpriteFont RobotoBold36;
        #endregion 

        #region Vectors
        Vector2 explosion1Origin;
        #endregion

        #region Rectangles
        Rectangle explosion1Rect;
        Rectangle coin1Rect_0;
        Rectangle coin1Rect_1;
        Rectangle coin1Rect_2;
        Rectangle coin1Rect_3;
        Rectangle coin1Rect_4;
        Rectangle coin1Rect_5;
        Rectangle coin1Rect_6;
        Rectangle coin1Rect_7;
        Rectangle coin1Rect_8;
        Rectangle coin1Rect_9;
        Rectangle windowMaxYRect;

        #endregion

        #region Upgrades
        // TODO : Finish Upgrade Levels
        float mainThrusterLVL = 0.4f;
        // int bodyLVL = 1;
        float sideThrusterLVL = 1f;
        // int payloadLVL = 1;
        float fuelLVL = 1;
        float fuelEfficency = 10f;
        // int DecoyLVL = 1;
        #endregion

        // EXPERIMENTAL

        // ------------

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
            // TODO : Make more Levels
            Level1,
            #endregion
            Shop,
            Campaign,
            Gameover,
            Exit
        };

        //starting gamestate
        GameStates gameState = GameStates.Level1;
        #endregion 

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO : Create a setting in-game for resulution
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
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
            Explosion1Tex = Content.Load<Texture2D>(@"Textures/Explosion1");
            Coin1 = Content.Load<Texture2D>(@"Textures/Coin1");
            #region Level1
            Level1BG = Content.Load<Texture2D>(@"Textures/Level1BG");
            PlayerTexture = Content.Load<Texture2D>(@"Textures/RocketTest");
            Building1 = Content.Load<Texture2D>(@"Textures/Building1");
            #endregion

            #endregion


            #region Soundeffects
            Explosion1Sound = Content.Load<SoundEffect>(@"Sounds/Explosion1");
            Coin1Sound = Content.Load<SoundEffect>(@"Sounds/Coin1");
            #endregion

            #region Fonts
            RobotoRegular36 = Content.Load<SpriteFont>(@"Fonts/Roboto/RobotoRegular36");
            RobotoBold36 = Content.Load<SpriteFont>(@"Fonts/Roboto/RobotoBold36");
            #endregion

            windowMaxX = graphics.GraphicsDevice.Viewport.Width;
            windowMaxY = graphics.GraphicsDevice.Viewport.Height;
            playerOrigin.X = PlayerTexture.Width / 2;
            playerOrigin.Y = PlayerTexture.Height / 2;
            explosion1Origin.X = Explosion1Tex.Width / 2;
            explosion1Origin.Y = Explosion1Tex.Height / 2;
            windowMaxYRect = new Rectangle(0, (int)windowMaxY - 1, (int)windowMaxX, 1);
            
        }

        protected override void UnloadContent()
        {
        }

        public void ResetGame()
        {
            switch (gameState) // Starting positions
            {
                case GameStates.Level1:
                    playerPos.X = 40;
                    playerPos.Y = this.Window.ClientBounds.Height - 120;
                    break;
            }
            GameIsActive = false;
            playerAccel = 0;
            playerAngle = 4.71f;
            gravityMomentum = 0;
            fuel = totalFuel;
            coin1IsActive_0 = true;
            coin1IsActive_1 = true;
            coin1IsActive_2 = true;
            coin1IsActive_3 = true;
            coin1IsActive_4 = true;
            coin1IsActive_5 = true;
            building1Dead = false;
            playerDead = false;
        }

        public void PlayerExplode()
        {
            playerDead = true;
            explosion1Rect = new Rectangle((int)playerPos.X, (int)playerPos.Y, 100, 100);
            if (explosion1Rect.Intersects(building1Rect))
            {
                building1Dead = true;
            }
            Explosion1Sound.Play();
            //gameState = GameStates.Gameover; 
        }

        public void CoinIntersection()
        {
            // TODO : Make a CoinManager
            if (playerRect.Intersects(coin1Rect_0) && coin1IsActive_0) { coin1IsActive_0 = false; money++; Coin1Sound.Play(); }
            if (playerRect.Intersects(coin1Rect_1) && coin1IsActive_1) { coin1IsActive_1 = false; money++; Coin1Sound.Play(); }
            if (playerRect.Intersects(coin1Rect_2) && coin1IsActive_2) { coin1IsActive_2 = false; money++; Coin1Sound.Play(); }
            if (playerRect.Intersects(coin1Rect_3) && coin1IsActive_3) { coin1IsActive_3 = false; money++; Coin1Sound.Play(); }
            if (playerRect.Intersects(coin1Rect_4) && coin1IsActive_4) { coin1IsActive_4 = false; money++; Coin1Sound.Play(); }
            if (playerRect.Intersects(coin1Rect_5) && coin1IsActive_5) { coin1IsActive_5 = false; money++; Coin1Sound.Play(); }
            if (playerRect.Intersects(coin1Rect_6) && coin1IsActive_6) { coin1IsActive_6 = false; money++; Coin1Sound.Play(); }
            if (playerRect.Intersects(coin1Rect_7) && coin1IsActive_7) { coin1IsActive_7 = false; money++; Coin1Sound.Play(); }
            if (playerRect.Intersects(coin1Rect_8) && coin1IsActive_8) { coin1IsActive_8 = false; money++; Coin1Sound.Play(); }
            if (playerRect.Intersects(coin1Rect_9) && coin1IsActive_9) { coin1IsActive_9 = false; money++; Coin1Sound.Play(); }
        }

        protected override void Update(GameTime gameTime)
        {

            GamePadState gamepad = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboard = Keyboard.GetState();

            #region to main menu
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.Back)) //esc or back to return to Mainmenu
            {
                gameState = GameStates.MainMenu;
            }
            #endregion

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

            #region Strings
            moneyStr = "Money : " + money.ToString();
            fuelStr = "Fuel : " + Math.Round(fuel, 0).ToString();
            #endregion

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

                    if (Keyboard.GetState().IsKeyDown(Keys.O)) //numpad1 to enter Level1
                    {
                        gameState = GameStates.Level1;
                    }

                    break;
                #endregion 

                #region Credits
                case GameStates.Credits:


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


                    break;
                #endregion

                #region Options
                case GameStates.Options:


                    break;
                #endregion

                #region LevelSelect
                case GameStates.LevelSelect:

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


                    break;
                #endregion

                #region Level1
                case GameStates.Level1:
                    if (!playerDead)
                    {
                        #region Physics
                        // TODO : make momentum last longer
                        if (firstRun)
                        {
                            building1Rect = new Rectangle(
                               (int)windowMaxX - Building1.Width - Convert.ToInt32(Building1.Height * 0.2f),
                               (int)windowMaxY - Building1.Height - Convert.ToInt32(Building1.Height * 0.3f),
                               Building1.Width,
                               Building1.Height
                           );

                            coin1Rect_0 = new Rectangle(40, (int)windowMaxY - 240, coin1Size, coin1Size);
                            coin1Rect_1 = new Rectangle(40, 240, coin1Size, coin1Size);
                            coin1Rect_2 = new Rectangle(240, 240, coin1Size, coin1Size);
                            coin1Rect_3 = new Rectangle(360, 240, coin1Size, coin1Size);
                            coin1Rect_4 = new Rectangle((int)windowMaxX / 2, (int)windowMaxY / 2, coin1Size, coin1Size);
                            coin1Rect_5 = new Rectangle((int)windowMaxX - 480, (int)windowMaxY - 240, coin1Size, coin1Size);

                            // total thruster power
                            mainThrusterPower = (float)Math.Pow(mainThrusterLVL, 1.5f);
                            sideThrusterPower = (float)Math.Pow(sideThrusterLVL, 1.5f);
                            // fuel
                            totalFuel = (float)Math.Pow(defaultfuel, fuelLVL);
                            ResetGame();
                            firstRun = false;
                        }

                        // Collision with the ground
                        if (playerRect.Intersects(windowMaxYRect))
                        {
                            PlayerExplode();
                        }

                        if (GameIsActive)
                        {
                        // Gravity
                        gravityMomentum += gravity;
                        playerPos.Y += gravityMomentum;
                        // Air resistence 
                        if (playerAccel >= 2 * airResistence) playerAccel -= airResistence;
                        }
                        // Direction
                        playerDirection = new Vector2((float)Math.Cos(playerAngle), (float)Math.Sin(playerAngle));
                        playerRect = new Rectangle((int)playerPos.X, (int)playerPos.Y, PlayerTexture.Width, PlayerTexture.Height);
                        


                        #endregion

                        #region Objects
                        // coins
                        CoinIntersection();
                        // collison with building
                        if (building1Rect.Intersects(playerRect))
                        {
                            PlayerExplode();
                        }
                        #endregion

                        #region W key
                        if (Keyboard.GetState().IsKeyDown(Keys.W) && fuel > 0)
                        {
                            //reduce gravity momentum
                            gravityMomentum *= 0.9f;
                            // Accelerate
                            playerAccel += mainThrusterPower;
                            // Use fuel
                            fuel -= fuelEfficency;
                            if (fuel < 0) fuel = 0;
                            // Speedlimit
                            if (playerAccel > playerMaxSpeed)
                            {
                                playerAccel = playerMaxSpeed;
                            }
                            // Start game on first press of W
                            if (!GameIsActive)
                            {
                                GameIsActive = true;
                            }
                        }
                        #endregion
                        if (GameIsActive)
                        {
                            #region A key
                            if (Keyboard.GetState().IsKeyDown(Keys.A) && fuel > 0)
                            {
                                //rotate counter-clockwise
                                playerAngle -= playerTurnRate;
                                fuel -= fuelEfficency / 3;
                            }
                            #endregion

                            #region S key
                            if (Keyboard.GetState().IsKeyDown(Keys.S)) // TODO : What to do with this?
                            {
                                //decelerate
                                playerAccel *= 0.9f;
                            }
                            #endregion

                            #region D key
                            if (Keyboard.GetState().IsKeyDown(Keys.D) && fuel > 0)
                            {
                                //rotate clockwise
                                playerAngle += playerTurnRate;
                                fuel -= fuelEfficency / 3;
                            }
                            #endregion

                            #region Space Key
                            if (Keyboard.GetState().IsKeyDown(Keys.Space) && GameIsActive)
                            {
                                PlayerExplode();
                            }
                            #endregion

                            // Update Player Position
                            playerPos += playerDirection * playerAccel;

                        }
                    }
                    #region R Key
                        if (Keyboard.GetState().IsKeyDown(Keys.R))
                        {
                            ResetGame();
                        }
                        #endregion
                    
                   break;
                #endregion

                #region Shop
                case GameStates.Shop:


                    
                    break;
                #endregion

                #region Campaign
                case GameStates.Campaign:



                    break;
                #endregion

                #region Gameover
                case GameStates.Gameover:
                    firstRun = true;

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


        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            switch (gameState)
            {
                        //template
                    /*
                    spriteBatch.Draw
                        (
                        RocketTest,         // Texture
                        playerPosition,     // position
                        null,               // Hitbox
                        Color.Black,        // Color
                        playerRotation,     // Rotation
                        rocketOrigin,       // Origin
                        0.3f,               // Scale
                        SpriteEffects.None, // Effects
                        0f                  // Layer
                        );
                    */

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

                #region LevelSelect
                case GameStates.LevelSelect:

                    spriteBatch.Draw( //Background
                        LevelSelectBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);


                    break;
                #endregion

                #region Levels
                    #region Level 1
                    case GameStates.Level1:

                        spriteBatch.Draw // Background
                            ( 
                            Level1BG,
                            new Rectangle(0, 0,
                            this.Window.ClientBounds.Width,
                            this.Window.ClientBounds.Height),
                            Color.White
                            );

                    spriteBatch.DrawString // Money
                        (
                        RobotoRegular36,
                        moneyStr,
                        new Vector2(0, 0),
                        Color.White
                        );
                    if(fuel > 0)
                    {
                    spriteBatch.DrawString // Fuel
                        (
                        RobotoRegular36,
                        fuelStr,
                        new Vector2(0, 50),
                        Color.White
                        );
                    }
                    else if (fuel < totalFuel / 5)
                    {
                        spriteBatch.DrawString // Fuel
                            (
                            RobotoRegular36,
                            fuelStr,
                            new Vector2(0, 50),
                            Color.Orange
                            );
                    }
                    else
                    {
                        spriteBatch.DrawString // Fuel
                            (
                            RobotoRegular36,
                            fuelStr,
                            new Vector2(0, 50),
                            Color.Red
                            );
                    }

                        spriteBatch.Draw // Building 1
                            (
                            Building1,
                            building1Rect,
                            null,
                            Color.White,
                            0f,
                            new Vector2(0, 0),
                            SpriteEffects.None,
                            0.5f
                            );
                    if(!playerDead)
                    {


                        spriteBatch.Draw // Player
                            (
                            PlayerTexture,
                            new Rectangle((int)playerPos.X, (int)playerPos.Y, (int)PlayerTexture.Width, (int)PlayerTexture.Height), 
                            null,
                            Color.White,
                            playerAngle,
                            playerOrigin,
                            SpriteEffects.None,
                            1f
                            );
                    }
                    else
                    {
                        spriteBatch.Draw // Explosion
                            (
                            Explosion1Tex,
                            explosion1Rect,
                            null,
                            Color.White,
                            0f,
                            explosion1Origin,
                            SpriteEffects.None,
                            1f
                            );
                    }
                    if (coin1IsActive_0) spriteBatch.Draw(Coin1, coin1Rect_0, Color.White);
                    if (coin1IsActive_1) spriteBatch.Draw(Coin1, coin1Rect_1, Color.White);
                    if (coin1IsActive_2) spriteBatch.Draw(Coin1, coin1Rect_2, Color.White);
                    if (coin1IsActive_3) spriteBatch.Draw(Coin1, coin1Rect_3, Color.White);
                    if (coin1IsActive_4) spriteBatch.Draw(Coin1, coin1Rect_4, Color.White);
                    if (coin1IsActive_5) spriteBatch.Draw(Coin1, coin1Rect_5, Color.White);

                    break;
                    #endregion
                #endregion

                #region Shop
                case GameStates.Shop:

                    spriteBatch.Draw( //Background
                        ShopBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);
                    break;
                #endregion

                #region Campaign
                case GameStates.Campaign:

                    spriteBatch.Draw( //Background
                        CampaignBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);
                    break;
                #endregion

                #region Gameover
                case GameStates.Gameover:

                    spriteBatch.Draw( //Background
                        GameoverBG,
                        new Rectangle(0, 0,
                        this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);
                    break;
                    #endregion
            }

            spriteBatch.End();
                base.Draw(gameTime);
            
        }
    }
}
