using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static MazeProjectGameDev.Maze;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input.Touch;
using System.Resources;
using MazeProjectGameDev.InputHandling;
namespace MazeProjectGameDev
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;
        private ControllerInput m_inputController;
        private const string mainString = "Maze-ta";
        private const string newGame5 = "F1 - New Game 5x5";

        private const string newGame10 = "F2 - New Game 10x10";

        private const string newGame15 = "F3 - New Game 15x15";
        private const string newGame20 = "F4 - New Game 20x20";
        private const string highScore = "F5 - Display High Scores";

        private const string credits = "F6 - Display Credits";

        private const string shortestPathString = "Press 'p' to show shortest path";
        private const string hintString = "Press 'h' for hint";
        private const string breadcrumbsString = "Press 'b' to show breadcrumbs";




        private Maze maze;

        private bool giveHint;
        private bool giveBreadCrumbs;
        private bool showShortestPath;

        private List<Rectangle> rectangles;
        private List<bool> isMouseOn;

        private int playerIndex;
        private Rectangle m_myBox;

        private Texture2D backGroundBoth;
        private Texture2D backGroundBottom;
        private Texture2D backGroundRight;
        private Texture2D backGroundDefault;
        private KeyboardState prevState;
        private KeyboardState currentState;
        private Texture2D playerTexture;
        private Texture2D shortestPathTexture;
        private KeyboardInput m_inputKeyboard;
        private Texture2D endTexture;
        private bool isPlaying;
        private GraphNode playerNode;
        private Rectangle mazeRectangle;
        private Rectangle mainMenu;
        private SpriteFont m_font1;



        private int GameWidth;
        private int GameHeight;
       




        public Game1()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            /*m_graphics.PreferredBackBufferWidth = 1920;
            m_graphics.PreferredBackBufferHeight = 1080;
            m_graphics.ApplyChanges();*/
            // TODO: Add your initialization logic here
            isPlaying = false;
            giveBreadCrumbs = false;
            giveHint = false;
            showShortestPath = false;
            rectangles = new List<Rectangle>();
            mazeRectangle = new Rectangle(m_graphics.PreferredBackBufferWidth / 8, m_graphics.PreferredBackBufferHeight / 8, m_graphics.PreferredBackBufferWidth - m_graphics.PreferredBackBufferWidth / 4, m_graphics.PreferredBackBufferHeight - m_graphics.PreferredBackBufferHeight / 4);
            playerNode = new GraphNode();

            /*GameWidth = 20;
            GameHeight = 20;
            playerIndex = 0;
            mainMenu = new Rectangle(0,0, m_graphics.PreferredBackBufferWidth, m_graphics.PreferredBackBufferHeight);
            mazeRectangle = new Rectangle(m_graphics.PreferredBackBufferWidth / 8, m_graphics.PreferredBackBufferHeight / 8, m_graphics.PreferredBackBufferWidth - m_graphics.PreferredBackBufferWidth / 4, m_graphics.PreferredBackBufferHeight - m_graphics.PreferredBackBufferHeight / 4);
            rectangles = new List<Rectangle>();
            isMouseOn = new List<bool>();
            m_myBox = new Rectangle(mazeRectangle.X + mazeRectangle.Width / GameWidth / 4, mazeRectangle.Y + mazeRectangle.Height / GameHeight / 4, mazeRectangle.Width / GameWidth / 2, mazeRectangle.Height / GameHeight / 2);
            
            for (int i = 0; i < GameHeight; i++)
            {
                for (int j = 0; j < GameWidth; j++)
                {
                    isMouseOn.Add(false);
                    rectangles.Add(new Rectangle(mazeRectangle.X + mazeRectangle.Width / GameWidth * j, mazeRectangle.Y + mazeRectangle.Height / GameHeight * i, mazeRectangle.Width / GameWidth, mazeRectangle.Height / GameHeight));
                }
            }
            maze = new Maze(GameWidth, GameHeight, 0);


            

            playerNode = maze.nodes[0];
            playerNode.isPlayerVisited = true;*/


            m_inputKeyboard = new KeyboardInput();
            m_inputKeyboard.registerCommand(Keys.W, true, new IInputDevice.CommandDelegate(onMoveUp));
            m_inputKeyboard.registerCommand(Keys.S, true, new IInputDevice.CommandDelegate(onMoveDown));
            m_inputKeyboard.registerCommand(Keys.A, true, new IInputDevice.CommandDelegate(onMoveLeft));
            m_inputKeyboard.registerCommand(Keys.D, true, new IInputDevice.CommandDelegate(onMoveRight));
            m_inputKeyboard.registerCommand(Keys.Up, true, new IInputDevice.CommandDelegate(onMoveUp));
            m_inputKeyboard.registerCommand(Keys.Down, true, new IInputDevice.CommandDelegate(onMoveDown));
            m_inputKeyboard.registerCommand(Keys.Left, true, new IInputDevice.CommandDelegate(onMoveLeft));
            m_inputKeyboard.registerCommand(Keys.Right, true, new IInputDevice.CommandDelegate(onMoveRight));
            m_inputKeyboard.registerCommand(Keys.H, true, new IInputDevice.CommandDelegate(hintToggle));
            m_inputKeyboard.registerCommand(Keys.B, true, new IInputDevice.CommandDelegate(breadCrumbsToggle));
            m_inputKeyboard.registerCommand(Keys.F1, true, new IInputDevice.CommandDelegate(create5By5Game));

            m_inputKeyboard.registerCommand(Keys.F2, true, new IInputDevice.CommandDelegate(create10By10Game));

            m_inputKeyboard.registerCommand(Keys.F3, true, new IInputDevice.CommandDelegate(create15By15Game));
            m_inputKeyboard.registerCommand(Keys.F4, true, new IInputDevice.CommandDelegate(create20By20Game));

            m_inputKeyboard.registerCommand(Keys.F5, true, new IInputDevice.CommandDelegate(breadCrumbsToggle));
            m_inputKeyboard.registerCommand(Keys.F6, true, new IInputDevice.CommandDelegate(breadCrumbsToggle));



            m_inputKeyboard.registerCommand(Keys.P, true, new IInputDevice.CommandDelegate(shortestPathToggle));




            m_inputController = new ControllerInput();
            m_inputController.registerCommand(Buttons.DPadUp, true, new IInputDevice.CommandDelegate(onMoveUp));
            m_inputController.registerCommand(Buttons.DPadDown, true, new IInputDevice.CommandDelegate(onMoveDown));
            m_inputController.registerCommand(Buttons.DPadLeft, true, new IInputDevice.CommandDelegate(onMoveLeft));
            m_inputController.registerCommand(Buttons.DPadRight, true, new IInputDevice.CommandDelegate(onMoveRight));
            m_inputController.registerCommand(Buttons.Y, true, new IInputDevice.CommandDelegate(hintToggle));

            m_inputController.registerCommand(Buttons.LeftTrigger, true, new IInputDevice.CommandDelegate(breadCrumbsToggle));

            m_inputController.registerCommand(Buttons.RightTrigger, true, new IInputDevice.CommandDelegate(shortestPathToggle));



            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_font1 = this.Content.Load<SpriteFont>("Fonts/MainFont");

            backGroundBoth = this.Content.Load<Texture2D>("backgroundBoth");
            backGroundBottom = this.Content.Load<Texture2D>("backgroundBottom");
            backGroundRight = this.Content.Load<Texture2D>("backgroundRight");
            backGroundDefault = this.Content.Load<Texture2D>("mazeBackgroundNone");
            endTexture = this.Content.Load<Texture2D>("IMG_2092");
            playerTexture = this.Content.Load<Texture2D>("pixil-frame-0 (3)");
            shortestPathTexture = this.Content.Load<Texture2D>("pixilMoney");
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            


            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        ///     Processes input of player
        /// </summary>
        private void processInput(GameTime gameTime)
        {
            m_inputKeyboard.Update(gameTime);
            m_inputController.Update(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {

            processInput(gameTime);
            prevState = currentState;
            //currentState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Point tempPoint = Mouse.GetState().Position;
            if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.A))
            {
                GamePad.SetVibration(PlayerIndex.One, 100f, 100f);
            }
            else
            {
                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);

            }

            


            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                maze = new Maze(GameWidth, GameHeight, playerIndex);
                playerNode = maze.nodes[playerIndex];

            }






            if (Keyboard.GetState().IsKeyDown (Keys.H)) 
            {
                GameWidth = 10;
                GameHeight = 10;
                rectangles.Clear();
                for (int i = 0; i < GameHeight; i++)
                {
                    for (int j = 0; j < GameWidth; j++)
                    {
                        rectangles.Add(new Rectangle(m_graphics.PreferredBackBufferWidth / GameWidth * j, m_graphics.PreferredBackBufferHeight / GameHeight * i, m_graphics.PreferredBackBufferWidth / GameWidth, m_graphics.PreferredBackBufferHeight / GameHeight));
                    }
                }
                maze = new Maze(10,10, 0);
                playerNode = maze.nodes[0];
                m_myBox = new Rectangle(m_graphics.PreferredBackBufferWidth / GameWidth , m_graphics.PreferredBackBufferHeight / GameWidth, m_graphics.PreferredBackBufferWidth / GameWidth, m_graphics.PreferredBackBufferHeight / GameHeight);
                playerIndex = 0;
            }







            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            

            // TODO: Add your drawing code here
            m_spriteBatch.Begin();

            if (!isPlaying)
            {

                float scale = m_graphics.PreferredBackBufferWidth * 0.001f;
                Vector2 string2Size = m_font1.MeasureString(mainString) * scale;
                m_spriteBatch.DrawString(
                    m_font1,
                    mainString,
                    new Vector2(m_graphics.PreferredBackBufferWidth / 2 - string2Size.X / 2, m_graphics.PreferredBackBufferHeight / 4),
                    Color.Black,
                    0,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    0);


                string2Size = m_font1.MeasureString(newGame5) * scale;
                m_spriteBatch.DrawString(
                    m_font1,
                    newGame5,
                    new Vector2(m_graphics.PreferredBackBufferWidth / 2 - string2Size.X / 2, m_graphics.PreferredBackBufferHeight / 4 + string2Size.Y / 1.25f),
                    Color.Black,
                    0,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    0);
                string2Size = m_font1.MeasureString(newGame10) * scale;
                m_spriteBatch.DrawString(
                    m_font1,
                    newGame10,
                    new Vector2(m_graphics.PreferredBackBufferWidth / 2 - string2Size.X / 2, m_graphics.PreferredBackBufferHeight / 4 + 2 * string2Size.Y / 1.25f),
                    Color.Black,
                    0,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    0);
                string2Size = m_font1.MeasureString(newGame15) * scale;
                m_spriteBatch.DrawString(
                    m_font1,
                    newGame15,
                    new Vector2(m_graphics.PreferredBackBufferWidth / 2 - string2Size.X / 2, m_graphics.PreferredBackBufferHeight / 4 + 3 * string2Size.Y / 1.25f),
                    Color.Black,
                    0,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    0);
                string2Size = m_font1.MeasureString(newGame20) * scale;
                m_spriteBatch.DrawString(
                    m_font1,
                    newGame20,
                    new Vector2(m_graphics.PreferredBackBufferWidth / 2 - string2Size.X / 2, m_graphics.PreferredBackBufferHeight / 4 + 4 * string2Size.Y / 1.25f),
                    Color.Black,
                    0,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    0);
                



            }
            else
            {


                for (int i = 0; i < rectangles.Count; i++)
                {

                    if (i < rectangles.Count - 1)
                    {

                        if (maze.nodes[i].getBottom() == null && maze.nodes[i].getRight() == null)
                        {
                            m_spriteBatch.Draw(backGroundBoth, rectangles[i], Color.White);
                        }
                        else if (maze.nodes[i].getBottom() != null && maze.nodes[i].getRight() == null)
                        {
                            m_spriteBatch.Draw(backGroundRight, rectangles[i], Color.White);

                        }
                        else if (maze.nodes[i].getBottom() == null && maze.nodes[i].getRight() != null)
                        {
                            m_spriteBatch.Draw(backGroundBottom, rectangles[i], Color.White);
                        }
                        else
                        {
                            m_spriteBatch.Draw(backGroundDefault, rectangles[i], Color.White);

                        }

                    }
                    else
                    {
                        m_spriteBatch.Draw(endTexture, rectangles[i], Color.Gray);

                    }

                }


                if (showShortestPath)
                {
                    for (int i = 0; i < maze.locations.Count; i++)
                    {
                        Rectangle tempRec = new Rectangle(rectangles[maze.locations[i]].X + mazeRectangle.Width / GameWidth / 4, rectangles[maze.locations[i]].Y + mazeRectangle.Height / GameWidth / 4, mazeRectangle.Width / GameWidth / 2, mazeRectangle.Height / GameHeight / 2);
                        m_spriteBatch.Draw(shortestPathTexture, tempRec, Color.White);


                    }
                }




                // Render the player:
                m_spriteBatch.Draw(playerTexture, m_myBox, Color.White);
            }



            


            m_spriteBatch.End();
            base.Draw(gameTime);
        }

        private void onMoveUp(GameTime gameTime)
        {
            if (playerNode.getTop() != null && isPlaying)
            {
                if (playerIndex - GameWidth == maze.locations[0])
                {
                    maze.locations.RemoveAt(0);
                }
                else
                {
                    maze.locations.Insert(0, playerIndex);
                }
                playerNode = playerNode.getTop();
                playerIndex -= GameWidth;
                m_myBox.Y -= mazeRectangle.Height / GameHeight;
                playerNode.isPlayerVisited = true;

            }
        }
        private void onMoveDown(GameTime gameTime)
        {
            if (playerNode.getBottom() != null && isPlaying)
            {

                if (playerIndex + GameWidth == maze.locations[0])
                {
                    maze.locations.RemoveAt(0);
                }
                else
                {
                    maze.locations.Insert(0, playerIndex);
                }
                playerNode = playerNode.getBottom();
                playerIndex += GameWidth;
                m_myBox.Y += mazeRectangle.Height / GameHeight;
                playerNode.isPlayerVisited = true;

            }
        }
        private void onMoveLeft(GameTime gameTime)
        {
            if (playerNode.getLeft() != null && isPlaying)
            {
                if (playerIndex - 1 == maze.locations[0])
                {
                    maze.locations.RemoveAt(0);
                }
                else
                {
                    maze.locations.Insert(0, playerIndex);
                }
                playerNode = playerNode.getLeft();
                playerIndex -= 1;
                m_myBox.X -= mazeRectangle.Width / GameWidth;
                playerNode.isPlayerVisited = true;

            }
        }
        private void onMoveRight(GameTime gameTime)
        {
            if (playerNode.getRight() != null && isPlaying)
            {

                if (playerIndex + 1 == maze.locations[0])
                {
                    maze.locations.RemoveAt(0);
                }
                else
                {
                    maze.locations.Insert(0, playerIndex);
                }
                playerNode = playerNode.getRight();
                playerIndex += 1;
                m_myBox.X += mazeRectangle.Width / GameWidth;
                playerNode.isPlayerVisited = true;

            }
        }
        private void hintToggle(GameTime gameTime)
        { 
            this.giveHint = !this.giveHint;
        }
        private void breadCrumbsToggle(GameTime gameTime)
        { 
            this.giveBreadCrumbs = !this.giveBreadCrumbs;
        }

        private void shortestPathToggle(GameTime gameTime)
        { 
            this.showShortestPath = !this.showShortestPath;
        }

        private void create5By5Game(GameTime gameTime)
        {
            isPlaying = true;
            GameWidth = 5;
            GameHeight = 5;
            maze = new Maze(5, 5,0);
            rectangles.Clear();
            for (int i = 0; i < GameHeight; i++)
            {
                for (int j = 0; j < GameWidth; j++)
                {
                    rectangles.Add(new Rectangle(mazeRectangle.X + mazeRectangle.Width / GameWidth * j, mazeRectangle.Y + mazeRectangle.Height / GameHeight * i, mazeRectangle.Width / GameWidth, mazeRectangle.Height / GameHeight));
                }
            }
            playerNode = maze.nodes[0];
            m_myBox = new Rectangle(mazeRectangle.X + mazeRectangle.Width / GameWidth / 4, mazeRectangle.Y + mazeRectangle.Height / GameHeight / 4, mazeRectangle.Width / GameWidth / 2, mazeRectangle.Height / GameHeight / 2);
            playerIndex = 0;
        }
        private void create10By10Game(GameTime gameTime)
        {
            isPlaying = true;
            isPlaying = true;
            GameWidth = 10;
            GameHeight = 10;
            maze = new Maze(10, 10, 0);
            rectangles.Clear();
            for (int i = 0; i < GameHeight; i++)
            {
                for (int j = 0; j < GameWidth; j++)
                {
                    rectangles.Add(new Rectangle(mazeRectangle.X + mazeRectangle.Width / GameWidth * j, mazeRectangle.Y + mazeRectangle.Height / GameHeight * i, mazeRectangle.Width / GameWidth, mazeRectangle.Height / GameHeight));
                }
            }
            playerNode = maze.nodes[0];
            m_myBox = new Rectangle(mazeRectangle.X + mazeRectangle.Width / GameWidth / 4, mazeRectangle.Y + mazeRectangle.Height / GameHeight / 4, mazeRectangle.Width / GameWidth / 2, mazeRectangle.Height / GameHeight / 2);
            playerIndex = 0;

        }
        private void create15By15Game(GameTime gameTime)
        {
            isPlaying = true;
            isPlaying = true;
            GameWidth = 15;
            GameHeight = 15;
            maze = new Maze(15, 15, 0);
            rectangles.Clear();
            for (int i = 0; i < GameHeight; i++)
            {
                for (int j = 0; j < GameWidth; j++)
                {
                    rectangles.Add(new Rectangle(mazeRectangle.X + mazeRectangle.Width / GameWidth * j, mazeRectangle.Y + mazeRectangle.Height / GameHeight * i, mazeRectangle.Width / GameWidth, mazeRectangle.Height / GameHeight));
                }
            }
            playerNode = maze.nodes[0];
            m_myBox = new Rectangle(mazeRectangle.X + mazeRectangle.Width / GameWidth / 4, mazeRectangle.Y + mazeRectangle.Height / GameHeight / 4, mazeRectangle.Width / GameWidth / 2, mazeRectangle.Height / GameHeight / 2);
            playerIndex = 0;

        }
        private void create20By20Game(GameTime gameTime)
        {
            isPlaying = true;
            isPlaying = true;
            GameWidth = 20;
            GameHeight = 20;
            maze = new Maze(20, 20, 0);
            rectangles.Clear();
            for (int i = 0; i < GameHeight; i++)
            {
                for (int j = 0; j < GameWidth; j++)
                {
                    rectangles.Add(new Rectangle(mazeRectangle.X + mazeRectangle.Width / GameWidth * j, mazeRectangle.Y + mazeRectangle.Height / GameHeight * i, mazeRectangle.Width / GameWidth, mazeRectangle.Height / GameHeight));
                }
            }
            playerNode = maze.nodes[0];
            m_myBox = new Rectangle(mazeRectangle.X + mazeRectangle.Width / GameWidth / 4, mazeRectangle.Y + mazeRectangle.Height / GameHeight / 4, mazeRectangle.Width / GameWidth / 2, mazeRectangle.Height / GameHeight / 2);
            playerIndex = 0;

        }


    }
}