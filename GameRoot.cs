﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Chapter6Game.Content;
using System.Diagnostics;
using MonoGame.Extended;

using Chapter6Game.Content.Objects;
using MonoGame.Extended.ViewportAdapters;
using MiniMan.Content.Objects;

namespace Chapter6Game
{
    public class GameRoot : Game
    {
        OrthographicCamera camera;
        
        InputManager Input;
        GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);
       
        // Put Camera at X: 745 when at end of level
        public Vector2 CameraPos;
       public Player player = new Player();
        Terrain terrain = new Terrain();
        Coins coin;
        bool flip = false;
        Enemy enemy = new Enemy();
        bool paused = false;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        
       
        private Texture2D background { get; set; }
        /// <summary>
        ///  Player Sprites and Animation Field
        /// </summary>
        int AnimState;
        //Player Animation
        private Texture2D idle, Jump, run, Damaged, punch, Fist;
        //coin animations
        private Texture2D coinIdle, coinSpark;

        //Red Enemy
        private Texture2D Redidle, patrol, stomp;
        //Blue Enemy
        private Texture2D Bluedle, Bluepatrol, blueHit;
        // Samurai Boss 
        private Texture2D Samuraiidle, Slash, samuraiRun,  hit;
       
        public SpriteAnimation[] animations = new SpriteAnimation[5];
        public SpriteAnimation[] coinAnims = new SpriteAnimation[2];
        public SpriteAnimation[] blueAnimations = new SpriteAnimation[3];
        public SpriteAnimation[] redAnimations = new SpriteAnimation[3];
        public SpriteAnimation[] samuraiAnimations = new SpriteAnimation[4];

        UIHearts hearts = new UIHearts();
        public Effect effect;

        
        public GameRoot()
        {
            Input = new InputManager(this);
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            coin = new Coins(this );
        }

        protected override void Initialize()
        {
            this.Components.Add(Input);
            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 360, 360);
            camera = new OrthographicCamera(viewportAdapter);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.ApplyChanges();
            player.Initialize();
            terrain.Initialize();
            hearts.Initialize();

                   

            
            CameraPos = player.position -= new Vector2(-35, 50);
            base.Initialize();
        }

        protected override void LoadContent()
        {
                       
            _spriteBatch = new SpriteBatch(GraphicsDevice);

 
            idle = Content.Load<Texture2D>("Characters/Player/PlayerIdle");
            run = Content.Load<Texture2D>("Characters/Player/PlayerRun");
            punch = Content.Load<Texture2D>("Characters/Player/PlayerPunch");
            Fist = Content.Load<Texture2D>("Characters/Player/PlayerFist");
            Jump = Content.Load<Texture2D>("Characters/Player/PlayerJump");
            Damaged = Content.Load<Texture2D>("Characters/Player/PlayerDamaged");

            coinIdle = Content.Load<Texture2D>("Collectibles/Coins");
            coinSpark = Content.Load<Texture2D>("Collectibles/CoinSpark");



            animations[0] = new SpriteAnimation(idle, 4, 8);
            animations[1] = new SpriteAnimation(run, 3, 8);
            animations[2] = new SpriteAnimation(Jump, 2, 1);
            animations[3] = new SpriteAnimation(punch, 4, 8);
            animations[4] = new SpriteAnimation(Damaged, 1, 1);

            coinAnims[0] = new SpriteAnimation(coinIdle, 4, 8);
            coinAnims[1] = new SpriteAnimation(coinSpark, 4, 7);
            coinAnims[1].IsLooping = false;

            
            
            
            // Sets the default animation to Idle
            player.anim = animations[0];
            coin.anim = coinAnims[0];
            
           
            background = Content.Load<Texture2D>("Terrain/Sky");
            terrain.LoadContent(Content);
            hearts.LoadContent(Content);

            effect = Content.Load<Effect>("PixelShader");
         
        }

      
        protected override void Update(GameTime gameTime)
        {
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            

            #region Pausing

            if (!paused)
            {
                camera.LookAt(CameraPos);
            }
            Input.Update(gameTime);
            if (Input.IsPressed(Keys.P))
            {
                paused = true;
            } else if (Input.IsPressed(Keys.O))
            {
                paused = false;
            }

            #endregion

            #region Keyboard Input
            if (Input.IsPressed(Keys.W) && !player.hasjumped)
                {
                
                    player.position.Y -= 14;
                    player.gravity = -7.5f;
                    player.hasjumped = true;
                Debug.WriteLine(AnimState);
            }

   


            if (Input.IsPressed(Keys.K) )
            {
                AnimState = 3;               
            }
            else
            {
                AnimState = 0;
             
            }

            if (Input.IsPressed(Keys.A) && !player.isCollidingside)
            {
                    flip = true;
                    AnimState = 1;
                    player.position.X -= player.speed;
                    CameraPos.X -= player.speed;
                hearts.Positions[0].X -= player.speed;
                hearts.Positions[1].X -= player.speed;
                hearts.Positions[2].X -= player.speed;
                Debug.WriteLine(AnimState);
            }
            else
            {
                player.anim = animations[0];
            }
            
                if (Input.IsPressed(Keys.D) && !player.isCollidingside)
                {

                    flip = false;
                    CameraPos.X += player.speed;
                    player.position.X = player.position.X + player.speed;
                hearts.Positions[0].X += player.speed ;
                hearts.Positions[1].X += player.speed;
                hearts.Positions[2].X += player.speed ;
                AnimState = 1;
                }

            HandleAnimationCollsions();
            #endregion
            if (!paused)
            {
                
                coin.Update(gameTime);
                AnimStates();
                player.Update(gameTime);
            }

           
            #region Controller Input
            if (capabilities.IsConnected)
            {
                AnimStates();
          
                GamePadState state = GamePad.GetState(PlayerIndex.One);
                if (state.IsButtonDown(Buttons.B))
                {
                    AnimState = 3;
                }
                if (state.IsButtonDown(Buttons.Y))
                {
                    AnimState = 3;
                }
                if (state.IsButtonDown(Buttons.A) && !player.hasjumped)
                {
                   
                    player.position.Y -= 14;
                    player.gravity = -7.5f;
                    player.hasjumped = true;
                    
                }
                if (state.IsButtonDown(Buttons.X) && !player.hasjumped)
                {
                    
                    player.position.Y -= 14;
                    player.gravity = -7.5f;
                    player.hasjumped = true;
                   
                }

                if (state.IsButtonDown(Buttons.DPadLeft) && !player.isCollidingside)
                {
                    flip = true;
                    AnimState = 1;
                    player.position.X -= player.speed;
                    CameraPos.X -= player.speed;
                    hearts.Positions[0].X -= player.speed;
                    hearts.Positions[1].X -= player.speed;
                    hearts.Positions[2].X -= player.speed;
                    Debug.WriteLine("Animation Position: " + player.anim.Position);
                }
                
               
                if (state.IsButtonDown(Buttons.DPadRight) && !player.isCollidingside)
                {
                    Debug.WriteLine(AnimState);
                    flip = false;
                    AnimState = 1;
                    player.position.X += player.speed;
                    CameraPos.X += player.speed;
                    hearts.Positions[0].X += player.speed;
                    hearts.Positions[1].X += player.speed;
                    hearts.Positions[2].X += player.speed;
                }


                if (capabilities.HasLeftXThumbStick)
                {
                    //Moves player with the Thumbstick
                    if(state.ThumbSticks.Left.X < -0.5f && !player.isCollidingside)
                    {
                        flip = true;
                        AnimState = 1;
                        player.position.X -= player.speed;
                        CameraPos.X -= player.speed;
                        hearts.Positions[0].X -= player.speed;
                        hearts.Positions[1].X -= player.speed;
                        hearts.Positions[2].X -= player.speed;
                    }
                    if (state.ThumbSticks.Left.X > .5f && !player.isCollidingside)
                    {
                        flip = false;
                        AnimState = 1;
                        player.position.X += player.speed;
                        CameraPos.X += player.speed;
                        hearts.Positions[0].X += player.speed;
                        hearts.Positions[1].X += player.speed;
                        hearts.Positions[2].X += player.speed;
                    }
                }
            }

            #endregion

            base.Update(gameTime);
        }
        #region debug
        public void DebugPlayer()
        {

           
            if (Input.IsHeld(Keys.NumPad0))
            {
                player.speed -= 1 ;
                Debug.WriteLine(player.speed);
            }
            if (Input.IsHeld(Keys.NumPad1))
            {
                player.speed += 1;
                Debug.WriteLine(player.speed);
            }
            if (Input.IsPressed(Keys.NumPad2))
            {
                player.gravity--;
                Debug.WriteLine(player.gravity);
            }
            if (Input.IsPressed(Keys.NumPad3))
            {
                player.gravity++;
                Debug.WriteLine(player.gravity);
            }

            if (Input.IsHeld(Keys.NumPad4))
            {
                player.playerRect.Width--;
            }

            if (Input.IsHeld(Keys.NumPad5))
            {
                player.playerRect.Width++;
                Debug.WriteLine(player.playerRect.Width);
            }
            
            if (Input.IsHeld(Keys.NumPad6))
            {
                player.playerRect.Height--;
                Debug.WriteLine(player.playerRect.Height);
            }
            if (Input.IsHeld(Keys.NumPad7))
            {
                player.playerRect.Height++;
                Debug.WriteLine(player.playerRect.Height);
            }
        }
        #endregion
        public void HandleAnimationCollsions()
        {
            if (coin.BoundingCircle.Intersects(player.playerRect))
            {
                coin.anim = coinAnims[1];
            }
            if (player.playerRect.Intersects(enemy.enemyRect))
            {
                player.anim = animations[4];
            }
            else
            {
                player.anim = animations[0];
            }
        }

        
        public  void AnimStates()
        {
            switch (AnimState)
            {
                case 1:
                    AnimState = 1;
                    player.anim = animations[1];
                    break;
                case 2:
                    AnimState = 2;
                    player.anim = animations[4];
                    break;
                case 3:
                    AnimState = 3;
                    player.anim = animations[3];
                    break;
                default:
                    AnimState = 0;
                    player.anim = animations[0];
                    break;
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            var transFormMatrix = camera.GetViewMatrix();

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, transformMatrix: transFormMatrix);
            #region Textures and Shader
            effect.CurrentTechnique.Passes[0].Apply();
            _spriteBatch.Draw(background, new Vector2(-1413, 50), Color.White);
            _spriteBatch.Draw(background, new Vector2(-942, 50), Color.White);
            _spriteBatch.Draw(background, new Vector2(-471, 50), Color.White);
                _spriteBatch.Draw(background, new Vector2(0, 50), Color.White);
                _spriteBatch.Draw(background, new Vector2(471, 50), Color.White);
            _spriteBatch.Draw(background, new Vector2(942, 50), Color.White);
            #endregion

            terrain.Draw(_spriteBatch);

            
                if (flip)
                {
                    player.anim.Draw(_spriteBatch, SpriteEffects.FlipHorizontally);
                }
                else
                {
                    player.anim.Draw(_spriteBatch, SpriteEffects.None);
                }
            
            coin.anim.Draw(_spriteBatch, SpriteEffects.None);
            hearts.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
