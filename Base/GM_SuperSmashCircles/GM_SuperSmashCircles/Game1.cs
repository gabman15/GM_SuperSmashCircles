using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace GM_SuperSmashCircles
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        private bool firstStep;

        //DllImport(NativeLibName, CallingConvention= CallingConvention.Cdecl);
        //public static extern int SDL_GameControllerAddMapping(string mappingString);
        public List<Entity> Entities { get; set; }
        //public List<User> Users { get; set; }
        public List<Platform> Platforms { get; set; }
        public Gamemode CurrentGamemode { get; set; }

        public double XGravity { get; set; }
        public double YGravity { get; set; }
        public double CollisionPrecision { get; set; }
        
        public Game1()
        {
            GamePad.InitDatabase();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Debug.WriteLine(Joystick.GetState(1).IsConnected);
            
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
            /*
            Entity testentity = Entity.LoadFromFile("testentity.lua");
            testentity.OnCollision.Call();
            testentity.OnUpdate.Call();
            Console.WriteLine("{0} {1} {2} {3} {4}", testentity.X, testentity.Y, testentity.Width, testentity.Height, testentity.RepelAmount);*/
            //Gamemode gamemode = Gamemode.LoadFromFile("testgamemode.lua");

            XGravity = 0;
            YGravity = 0;
            CollisionPrecision = 1;

            firstStep = false;

            Entities = new List<Entity>();
            Platforms = new List<Platform>();
            CurrentGamemode = Gamemode.LoadFromFile("gamemode_default.lua", this);
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
            ContentManager.AddContent(Content.Load<Texture2D>("circle"), "circle");
            ContentManager.AddContent(Content.Load<Texture2D>("platform"), "platform");
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
            if (GamePad.GetState(PlayerIndex.Four).Buttons.B == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(!firstStep)
            {
                CurrentGamemode.OnStart?.Call();
                firstStep = true;
            }

            foreach (Entity e in Entities)
            {
                e.OnUpdate?.Call();
                e.DX += XGravity;
                e.DY += YGravity;
                //collision checking here maybe, although it might need to be somewhere else
            }

            
            foreach(Entity e in Entities)
            {
                //test for collisions with each platform
                //we only care about downward collisions since these are platforms
                bool foundCollision = false;
                foreach(Platform p in Platforms)
                {
                    while (RectangleInRectangle(e.X, e.Y, e.X + e.Width, e.Y + e.Height + e.DY, p.X, p.Y, p.X + p.Width, p.Y + p.Height))
                    {
                        foundCollision = true;
                        e.DY -= CollisionPrecision;
                        if(Math.Abs(e.DY) < CollisionPrecision)
                        {
                            e.DY = 0;
                            break;
                        }
                    }
                    
                }
                if (foundCollision)
                {
                    if (e.DY > CollisionPrecision)
                    {
                        e.DY -= CollisionPrecision;
                    }
                }
            }
            


            CurrentGamemode.OnUpdate?.Call();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach(Entity e in Entities)
            {
                e.Draw(spriteBatch);
            }
            foreach(Platform p in Platforms)
            {
                p.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
        public bool RectangleInRectangle(double ax1, double ay1, double ax2, double ay2, double bx1, double by1, double bx2, double by2)
        {
            //make sure the 1s are top left corner, and the 2s are bottom right corner of their corresponding rectangles
            if(ax1 > ax2)
            {
                double tmp = ax1;
                ax1 = ax2;
                ax2 = tmp;
            }
            if(ay1 > ay2)
            {
                double tmp = ay1;
                ay1 = ay2;
                ay2 = tmp;
            }
            if(bx1 > bx2)
            {
                double tmp = bx1;
                bx1 = bx2;
                bx2 = tmp;
            }
            if(by1 > by2)
            {
                double tmp = by1;
                by1 = by2;
                by2 = tmp;
            }
            return ax1 < bx2 && ax2 > bx1 && ay1 < by2 && ay2 > by1;
        }
        public Entity CreateEntity(string filename)
        {
            Entity entity = Entity.LoadFromFile(filename, this);
            Entities.Add(entity);
            return entity;
        }
        public Platform CreatePlatform(string filename)
        {
            Platform platform = Platform.LoadFromFile(filename, this);
            Platforms.Add(platform);
            return platform;
        }
        public bool CheckCollision(double x1, double y1, double x2, double y2)
        {
            foreach (Platform p in Platforms)
            {
                if (RectangleInRectangle(x1, y1, x2, y2, p.X, p.Y, p.X + p.Width, p.Y + p.Height))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
