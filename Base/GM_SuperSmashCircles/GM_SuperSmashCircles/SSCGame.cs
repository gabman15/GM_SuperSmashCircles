using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GM_SuperSmashCircles.Input;
using GM_SuperSmashCircles.UserInterface;

namespace GM_SuperSmashCircles
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SSCGame : Game
    {
        /// <summary>
        /// graphics device manager for the game
        /// </summary>
        private GraphicsDeviceManager graphics;
        /// <summary>
        /// sprite batch for drawing things in the game
        /// </summary>
        private SpriteBatch spriteBatch;

        /// <summary>
        /// if we have not just switched to a gamemode
        /// </summary>
        private bool firstStep;

        /// <summary>
        /// list of all of the entities in the game
        /// </summary>
        public List<Entity> Entities { get; set; }
        /// <summary>
        /// list of all the users in the game
        /// </summary>
        public List<User> Users { get; set; }
        /// <summary>
        /// list of all the platforms in the game
        /// </summary>
        public List<Platform> Platforms { get; set; }

        internal GamePadState GetGamepadState(PlayerIndex player)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// list of ui elements to draw
        /// </summary>
        public List<UIElement> UIElements { get; set; }
        /// <summary>
        /// the current gamemode
        /// </summary>
        public Gamemode CurrentGamemode { get; set; }

        /// <summary>
        /// event emitter for the game
        /// </summary>
        public EventEmitter Events { get; set; }

        /// <summary>
        /// maximum number of users
        /// </summary>
        public int MaxUsers { get; set; }
        /// <summary>
        /// gravity on the x axis
        /// </summary>
        public double XGravity { get; set; }
        /// <summary>
        /// gravity on the y axis
        /// </summary>
        public double YGravity { get; set; }
        /// <summary>
        /// friction when entity is not colliding with a platform
        /// </summary>
        public double AirFriction { get; set; }
        /// <summary>
        /// how precise collision checks are
        /// </summary>
        public double CollisionPrecision { get; set; }
        /// <summary>
        /// whether to listen for users trying to join
        /// </summary>
        public bool ListenForUsers { get; set; }
        /// <summary>
        /// list of possible input modules that can be used
        /// </summary>
        public List<InputModule> PossibleInputModules { get; set; }
        /// <summary>
        /// list of inputs that can be used to trigger an input module
        /// </summary>
        public List<string> PossibleInputs { get; set; }
        /// <summary>
        /// list of colors to use for players
        /// </summary>
        public List<Color> UserColorOrder { get; set; }
        /// <summary>
        /// make a new game object
        /// </summary>
        public SSCGame()
        {
            //GamePad.InitDatabase(); //what is this
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Events = new EventEmitter();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            MaxUsers = 4;
            XGravity = 0;
            YGravity = 0;
            AirFriction = 0.3;
            CollisionPrecision = 1;
            PossibleInputModules = new List<InputModule>();
            PossibleInputModules.Add(new LeftKeyboardInputModule(this));
            PossibleInputModules.Add(new RightKeyboardInputModule(this));
            PossibleInputModules.Add(new GamepadInputModule(this, PlayerIndex.One));
            PossibleInputModules.Add(new GamepadInputModule(this, PlayerIndex.Two));
            PossibleInputModules.Add(new GamepadInputModule(this, PlayerIndex.Three));
            PossibleInputModules.Add(new GamepadInputModule(this, PlayerIndex.Four));
            PossibleInputs = new List<string>();
            PossibleInputs.Add("jump");
            PossibleInputs.Add("up");
            PossibleInputs.Add("down");
            PossibleInputs.Add("left");
            PossibleInputs.Add("right");
            PossibleInputs.Add("dash");
            PossibleInputs.Add("special");
            UserColorOrder = new List<Color>();
            UserColorOrder.Add(Color.Red);
            UserColorOrder.Add(Color.Blue);
            UserColorOrder.Add(Color.Green);
            UserColorOrder.Add(Color.Yellow);
            UserColorOrder.Add(Color.Purple);
            UserColorOrder.Add(Color.Cyan);
            UserColorOrder.Add(Color.Orange);
            UserColorOrder.Add(Color.White);
            UserColorOrder.Add(Color.Black);
            ListenForUsers = true;
            firstStep = false;
            Users = new List<User>();
            Entities = new List<Entity>();
            Platforms = new List<Platform>();
            UIElements = new List<UIElement>();
            //--begin testing purposes--
            //Users.Add(new User(this, 0, new LeftKeyboardInputModule(this)));
            //--end testing purposes--
            CurrentGamemode = Gamemode.LoadFromFile("gamemode_default.lua", this);
            base.Initialize();

            Events.Emit("initialize");
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

            Events.Emit("content");
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(!firstStep)
            {
                CurrentGamemode.OnStart?.Call();
                firstStep = true;
            }

            foreach (Entity e in Entities)
            {
                e.DX += XGravity;
                e.DY += YGravity;
            }

            
            foreach(Entity e in Entities)
            {
                double fric = e.Friction;
                //test for collisions with each platform
                //we only care about downward collisions since these are platforms
                if (e.CollideWithPlatforms)
                {
                    bool foundCollision = false;
                    List<Platform> collidedPlatforms = new List<Platform>();
                    foreach (Platform p in Platforms)
                    {
                        if(RectangleInRectangle(e.X, e.Y, e.X + e.Width, e.Y + e.Height, p.X, p.Y, p.X + p.Width, p.Y + p.Height))
                        {
                            //we're inside it, don't collide with it
                            break;
                        }
                        while (RectangleInRectangle(e.X, e.Y, e.X + e.Width, e.Y + e.Height + e.DY, p.X, p.Y, p.X + p.Width, p.Y + p.Height))
                        {
                            foundCollision = true;
                            collidedPlatforms.Add(p);
                            e.DY -= CollisionPrecision;
                            if (Math.Abs(e.DY) < CollisionPrecision)
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
                        e.OnSolidCollision?.Call();
                        //we know we're on ground, so use ground friction
                        //get the average friction for all of the platforms we collided with
                        double gFric = 0;
                        foreach(Platform p in collidedPlatforms)
                        {
                            gFric += p.Friction;
                        }
                        gFric /= collidedPlatforms.Count;
                        fric += gFric;
                    }
                    else
                    {
                        //we're not on the ground, so use air friction
                        fric += AirFriction;
                    }
                    //apply the friction
                    //for now we will only be caring about x-axis friction, but this can be changed in the future
                    if (Math.Abs(e.DX) < fric)
                    {
                        e.DX = 0;
                    }
                    else
                    {
                        e.DX -= fric * Math.Sign(e.DX);
                    }
                }
                if(e.CollideWithEntities)
                {
                    Rectangle eRect = e.GetRectangle();
                    foreach(Entity target in Entities)
                    {
                        if(target.CollideWithEntities && target != e)
                        {
                            if(eRect.Intersects(target.GetRectangle()))
                            {
                                e.OnEntityCollision?.Call(target);
                                //repel
                                int side = Math.Sign(e.X - target.X); //negative -> left, positive -> right
                                e.DX += target.RepelAmount * side;
                            }
                        }
                    }
                }
            }
            foreach (Entity e in Entities)
            {
                e.X += e.DX;
                e.Y += e.DY;
            }

            if(ListenForUsers)
            {
                InputModule mod = GetInputModule();
                if(mod != null)
                {
                    User user = new User(this, Users.Count + 1, mod);
                    Users.Add(user);
                    Console.WriteLine("created new user with module " + mod.Name);
                    CurrentGamemode.OnNewUser?.Call(user);
                }
            }
            CurrentGamemode.OnUpdate?.Call();

            base.Update(gameTime);

            Events.Emit("update");
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
            foreach(UIElement elem in UIElements)
            {
                elem.Draw(spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
            //Events.Emit("draw");
        }
        /// <summary>
        /// gets the input module currently being pressed
        /// </summary>
        /// <returns>the input module currently being pressed, null if none</returns>
        public InputModule GetInputModule()
        {
            foreach(InputModule mod in PossibleInputModules)
            {
                foreach(string name in PossibleInputs)
                {
                    if(mod.Get(name))
                    {
                        PossibleInputModules.Remove(mod);
                        return mod;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// checks if a given rectangle is in another given rectangle
        /// </summary>
        /// <param name="ax1">first rectangle top left x coordinate</param>
        /// <param name="ay1">first rectangle top left y coordinate</param>
        /// <param name="ax2">first rectangle bottom right x coordinate</param>
        /// <param name="ay2">first rectangle bottom right y coordinate</param>
        /// <param name="bx1">second rectangle top left x coordinate</param>
        /// <param name="by1">second rectangle top left y coordinate</param>
        /// <param name="bx2">second rectangle bottom right x coordinate</param>
        /// <param name="by2">second rectangle bottom right y coordinate</param>
        /// <returns>if the two given rectangles intersect</returns>
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
        /// <summary>
        /// creates a lua-based entity from the given filename
        /// </summary>
        /// <param name="filename">name of the lua file to load</param>
        /// <returns>the created entity</returns>
        public Entity CreateEntity(string filename)
        {
            Entity entity = Entity.LoadFromFile(filename, this);
            Entities.Add(entity);
            return entity;
        }
        /// <summary>
        /// creates a lua-based platform from the given filename
        /// </summary>
        /// <param name="filename">the name of the lua file to load</param>
        /// <returns>the created platform</returns>
        public Platform CreatePlatform(string filename)
        {
            Platform platform = Platform.LoadFromFile(filename, this);
            Platforms.Add(platform);
            return platform;
        }
        /// <summary>
        /// checks collision of the given rectangle with all of the instances of platforms in the game
        /// </summary>
        /// <param name="x1">top left x coordinate of the rectangle</param>
        /// <param name="y1">top left y coordinate of the rectangle</param>
        /// <param name="x2">bottom right x coordinate of the rectangle</param>
        /// <param name="y2">bottom right y coordinate of the rectangle</param>
        /// <returns>if the given rectangle intersects with any of the existing platforms</returns>
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
        /// <summary>
        /// merges input items together (ease of access)
        /// </summary>
        /// <param name="items">the items to merge</param>
        /// <returns>the merged input item</returns>
        public static MultiInputItem MergeInputItems(params IInputItem[] items)
        {
            MultiInputItem newItem = new MultiInputItem();
            foreach(IInputItem item in items)
            {
                newItem.InputItems.Add(item);
            }
            return newItem;
        }
    }
}
