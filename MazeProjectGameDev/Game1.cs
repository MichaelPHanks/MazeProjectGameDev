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

namespace MazeProjectGameDev
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager m_graphics;
        private SpriteBatch m_spriteBatch;

        private Maze maze;

        private List<Rectangle> rectangles;
        private List<bool> isMouseOn;

        private List<int> tempGameMovement;
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

        private Texture2D endTexture;

        private GraphNode playerNode;

        private const int GAME_MOVEMENT = 1;

        private int GameSize;
        private int GameWidth;
        private int GameHeight;
        private Texture2D pixel; // 1x1 pixel texture to represent the line
       

        private bool isButtonPressed;



        public Game1()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            m_graphics.PreferredBackBufferWidth = 1920;
            m_graphics.PreferredBackBufferHeight = 1080;
            m_graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            currentState = Keyboard.GetState();
            isButtonPressed = false;
            GameSize = 400;
            GameWidth = 20;
            GameHeight = 20;
            playerIndex = 0;
            rectangles = new List<Rectangle>();
            isMouseOn = new List<bool>();
            m_myBox = new Rectangle(m_graphics.PreferredBackBufferWidth / GameWidth / 4, m_graphics.PreferredBackBufferHeight / GameWidth / 4, m_graphics.PreferredBackBufferWidth / GameWidth / 2, m_graphics.PreferredBackBufferHeight / GameHeight / 2);
            backGroundBoth = this.Content.Load<Texture2D>("backgroundBoth");
            backGroundBottom = this.Content.Load<Texture2D>("backgroundBottom");
            backGroundRight = this.Content.Load<Texture2D>("backgroundRight");
            backGroundDefault = this.Content.Load<Texture2D>("mazeBackgroundNone");
            endTexture = this.Content.Load<Texture2D>("IMG_2092");
            playerTexture = this.Content.Load<Texture2D>("saulgoodman");
            shortestPathTexture = this.Content.Load<Texture2D>("pixilMoney");
            for (int i = 0; i < GameHeight; i++)
            {
                for (int j = 0; j < GameWidth; j++)
                {
                    isMouseOn.Add(false);
                    rectangles.Add(new Rectangle(m_graphics.PreferredBackBufferWidth / GameWidth * j, m_graphics.PreferredBackBufferHeight / GameHeight * i, m_graphics.PreferredBackBufferWidth / GameWidth, m_graphics.PreferredBackBufferHeight / GameHeight));
                }
            }
            maze = new Maze(GameWidth, GameHeight, 0);


            

            playerNode = maze.nodes[0];
            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_spriteBatch = new SpriteBatch(GraphicsDevice);
            pixel = new Texture2D(GraphicsDevice, 5, 5);
            Color[] colors = new Color[5 * 5];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.White;
            }
            pixel.SetData(colors);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            prevState = currentState;
            currentState = Keyboard.GetState();
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





            if ((GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D)) && playerNode.getRight() != null)
            {
                if (currentState.IsKeyDown(Keys.D) == !prevState.IsKeyDown(Keys.D))
                {
                    isButtonPressed = true;

                    // if the player is at index 9, 19, 29,
                    /*if (playerNode.getRight() == maze.shortestPath.First())
                    {
                        maze.shortestPath.Pop();
                    }
                    else
                    {
                        maze.shortestPath.Push(playerNode);
                    }*/
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
                    m_myBox.X += m_graphics.PreferredBackBufferWidth / GameWidth;

                }


            }

            if ((GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.A)) && playerNode.getLeft() != null)
            {
                if (currentState.IsKeyDown(Keys.A) == !prevState.IsKeyDown(Keys.A))
                {
                    isButtonPressed = true;
                    if (maze.shortestPath.Any()) 
                    {
                        /*if (playerNode.getLeft() == maze.shortestPath.First())
                        {
                            maze.shortestPath.Pop();
                        }
                        else
                        {
                            maze.shortestPath.Push(playerNode);
                        }*/
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
                        m_myBox.X -= m_graphics.PreferredBackBufferWidth / GameWidth;

                    }

                }

            }

            if ((GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.W)) && playerNode.getTop() != null)

            {
                if (currentState.IsKeyDown(Keys.W) == !prevState.IsKeyDown(Keys.W))
                {
                    isButtonPressed = true;
                    /*  if (playerNode.getTop() == maze.shortestPath.First())
                  {
                      maze.shortestPath.Pop();
                  }
                  else
                  {
                      maze.shortestPath.Push(playerNode);
                  }*/
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
                    m_myBox.Y -= m_graphics.PreferredBackBufferHeight / GameHeight;


                }


            }
            if ((GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.S)) && playerNode.getBottom() != null)

            {
                if (currentState.IsKeyDown(Keys.S) == !prevState.IsKeyDown(Keys.S))
                {
                    /*if (playerNode.getBottom() == maze.shortestPath.First())
                    {
                        maze.shortestPath.Pop();
                    }
                    else
                    {
                        maze.shortestPath.Push(playerNode);
                    }*/

                    if (playerIndex + GameWidth == maze.locations[0])
                    {
                        maze.locations.RemoveAt(0);
                    }
                    else
                    {
                        maze.locations.Insert(0, playerIndex);
                    }
                    isButtonPressed = true;
                    playerNode = playerNode.getBottom();
                    playerIndex += GameWidth;
                    m_myBox.Y += m_graphics.PreferredBackBufferHeight / GameHeight;

                }

            }


            if (Keyboard.GetState().IsKeyDown (Keys.H)) 
            {
                GameWidth = 10;
                GameHeight = 10;
                GameSize = 100;
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
                m_myBox = new Rectangle(m_graphics.PreferredBackBufferWidth / GameWidth / 4, m_graphics.PreferredBackBufferHeight / GameWidth / 4, m_graphics.PreferredBackBufferWidth / GameWidth / 2, m_graphics.PreferredBackBufferHeight / GameHeight / 2);
                playerIndex = 0;
            }







            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);



            // TODO: Add your drawing code here
            m_spriteBatch.Begin();
            for (int i = 0; i < rectangles.Count; i++)
            {
                /*if (playerNode == maze.nodes[i])
                {
                    Vector2 textureOrigin = new Vector2(playerTexture.Width / 2f, playerTexture.Height / 2f);

                    // Calculate the position to center the texture within the rectangle
                    Vector2 texturePosition = new Vector2(
                        m_myBox.X + m_myBox.Width / 2f - textureOrigin.X,
                        m_myBox.Y + m_myBox.Height / 2f - textureOrigin.Y
                    );

                    // Draw the texture centered on the rectangle
                    //m_spriteBatch.Draw(playerTexture, m_myBox, null, Color.White, 0f, textureOrigin,SpriteEffects.None, 0f);
                    m_spriteBatch.Draw(playerTexture, m_myBox, Color.White);


                }*/
                if (i < rectangles.Count - 1)
                {
                    /*if (maze.shortestPath.Contains(maze.nodes[i]))
                    {

                        //m_spriteBatch.Draw(backGroundBoth, rectangles[i], Color.Blue);
                        m_spriteBatch.Draw(backGroundDefault, rectangles[i], Color.White);


                    }*/
                    /*else
                    {*/
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

                    /*}*/
                }
                else
                {
                    m_spriteBatch.Draw(endTexture, rectangles[i], Color.Gray);

                }

            }


            for(int i = 0; i < maze.locations.Count; i++)
            {
                Console.WriteLine("Yeah");
                Rectangle tempRec = new Rectangle(rectangles[maze.locations[i]].X + m_graphics.PreferredBackBufferWidth / GameWidth / 4, rectangles[maze.locations[i]].Y + m_graphics.PreferredBackBufferHeight / GameWidth / 4, m_graphics.PreferredBackBufferWidth / GameWidth / 2, m_graphics.PreferredBackBufferHeight / GameHeight / 2);
                m_spriteBatch.Draw(shortestPathTexture, tempRec, Color.White);


            }

            // Render the player:
            m_spriteBatch.Draw(playerTexture, m_myBox, Color.White);



            /*for (int i = 0; i < rectangles.Count; i++)
            {
                int x = rectangles[i].X + rectangles[i].Width;
                int y = rectangles[i].Y + rectangles[i].Height;

                if (maze.nodes[i].getRight() == null)
                {

                    Vector2 startPoint = new Vector2(x, y);
                    Vector2 endPoint = new Vector2(x, y - rectangles[i].Height);
                    Vector2 edge = endPoint - startPoint;
                    float angle = (float)Math.Atan2(edge.Y, edge.X);

                    m_spriteBatch.Draw(
                        pixel,
                        new Rectangle(x, y, (int)edge.Length(), 1),
                        null,
                        Color.Black,
                            angle,
                            new Vector2(0, 0),
                            SpriteEffects.None,
                            0
                       );
                }

                if (maze.nodes[i].getBottom() == null)
                {

                    Vector2 startPoint = new Vector2(x, y);
                    Vector2 endPoint = new Vector2(x - rectangles[i].Width, y);
                    Vector2 edge = endPoint - startPoint;
                    float angle = (float)Math.Atan2(edge.Y, edge.X);

                    m_spriteBatch.Draw(
                        pixel,
                        new Rectangle(x, y, (int)edge.Length(), 1),
                        null,
                        Color.Black,
                            angle,
                            new Vector2(0, 0),
                            SpriteEffects.None,
                            0
                       );
                }




            }*/

            m_spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}