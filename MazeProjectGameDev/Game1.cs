using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static MazeProjectGameDev.Maze;
using System.Reflection.Metadata;

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
        private Texture2D m_myTexture;
        private KeyboardState prevState;
        private KeyboardState currentState;
        private Texture2D playerTexture;

        private Texture2D endTexture;

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
            // TODO: Add your initialization logic here
            currentState = Keyboard.GetState();



            isButtonPressed = false;
            GameSize = 400;
            GameWidth = 20;
            GameHeight = 20;
            playerIndex = 0;
            rectangles = new List<Rectangle>();
            isMouseOn = new List<bool>();
            m_myBox = new Rectangle(10, 0, m_graphics.PreferredBackBufferWidth / 10, m_graphics.PreferredBackBufferHeight / 10);
            m_myTexture = this.Content.Load<Texture2D>("square");
            endTexture = this.Content.Load<Texture2D>("IMG_2092");
            playerTexture = this.Content.Load<Texture2D>("saulgoodman");
            for (int i = 0; i < GameHeight; i++)
            {
                for (int j = 0; j < GameWidth; j++)
                {
                    isMouseOn.Add(false);
                    rectangles.Add(new Rectangle(m_graphics.PreferredBackBufferWidth / GameWidth * j, m_graphics.PreferredBackBufferHeight / GameHeight * i, m_graphics.PreferredBackBufferWidth / GameWidth, m_graphics.PreferredBackBufferHeight / GameHeight));
                }
            }
            maze = new Maze(GameHeight, GameWidth);


            for (int i = 0; i < maze.edges1.Count; i++)
            {
                Console.WriteLine(maze.edges1[i].from + " --> " + maze.edges1[i].to);
            }
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

            if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                var buttonList = new List<Buttons>()
                {
                    {Buttons.A},
                    {Buttons.B},
                    {Buttons.Y},
                    {Buttons.X},
                    {Buttons.Start},
                    {Buttons.Back},
                    {Buttons.RightShoulder},
                    {Buttons.LeftShoulder},
                    {Buttons.RightTrigger},
                    {Buttons.LeftTrigger}
                };
                int total = 0;
                foreach (var button in buttonList)
                {
                    if (!GamePad.GetState(PlayerIndex.One).IsButtonDown(button))
                    {
                        total += 1;
                    }
                }
                if (total == 10)
                {
                    isButtonPressed = false;
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Space)) 
            {
                maze = new Maze(GameHeight, GameWidth);
            }

            



            if ((GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D)) && (!maze.edges1.Any(obj => (obj.from == playerIndex && obj.to == playerIndex + 1) || (obj.from == playerIndex + 1 && obj.to == playerIndex))))
            {
                if (currentState.IsKeyDown(Keys.D) == !prevState.IsKeyDown(Keys.D))
                {
                    isButtonPressed = true;

                    // if the player is at index 9, 19, 29, 
                    if ((playerIndex + 1) % GameWidth != 0)
                    {
                        playerIndex += 1;
                    }
                }


            }
           
            if ((GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.A)) && (!maze.edges1.Any(obj => (obj.from == playerIndex && obj.to == playerIndex - 1) || (obj.from == playerIndex - 1 && obj.to == playerIndex))))
            {
                if (currentState.IsKeyDown(Keys.A) == !prevState.IsKeyDown(Keys.A))
                {
                    isButtonPressed = true;

                    if (((playerIndex) % GameWidth != 0 || playerIndex - 1 == 0) && playerIndex - 1 != -1)
                    {
                        playerIndex -= 1;
                    }
                }
            }

            if ((GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.W)) && (!maze.edges1.Any(obj => (obj.from == playerIndex && obj.to == playerIndex - GameWidth) || (obj.from == playerIndex - GameWidth && obj.to == playerIndex))))

            {
                if (currentState.IsKeyDown(Keys.W) == !prevState.IsKeyDown(Keys.W))
                {
                    isButtonPressed = true;

                    if (playerIndex - GameWidth >= 0)
                    {
                        playerIndex -= GameWidth;

                    }
                }


            }
            if ((GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.S)) && (!maze.edges1.Any(obj => (obj.from == playerIndex && obj.to == playerIndex + GameWidth) || (obj.from == playerIndex + GameWidth && obj.to == playerIndex))))

            {
                if (currentState.IsKeyDown(Keys.S) == !prevState.IsKeyDown(Keys.S))
                {
                    isButtonPressed = true;

                    if (playerIndex + GameWidth < GameSize)
                    {
                        playerIndex += GameWidth;
                    }
                }

            }
            
            isMouseOn = isMouseOn.Select(x => x ? false : x).ToList();
            
            isMouseOn[playerIndex] = true;

            /* for (int i = 0; i < isMouseOn.Count; i++)
             {
                 isMouseOn[i] = false;
             }*/

            /*if (isMouseOn.Exists(x => x == true))
            {
                isMouseOn = isMouseOn.Select(x => x ? false : x).ToList();
            }

            if (rectangles.Exists(x => x.Contains(tempPoint))) 
            
            {
                int tempRec =  rectangles.FindIndex(x => x.Contains(tempPoint));
                isMouseOn[tempRec] = true;
                
            }*/

            // TODO: Add your update logic here

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

                

            // TODO: Add your drawing code here
            m_spriteBatch.Begin();
            for (int i = 0; i < rectangles.Count; i++)
            {
                if (isMouseOn[i])
                {
                    m_spriteBatch.Draw(playerTexture, rectangles[i], Color.White);

                }
                else if (i < rectangles.Count - 1)
                {
                    m_spriteBatch.Draw(m_myTexture, rectangles[i], Color.Gray);
                }
                else 
                {
                    m_spriteBatch.Draw(endTexture, rectangles[i], Color.Gray);

                }

            }

            for(int i = 0;i < rectangles.Count;i++)
            {
                int x = rectangles[i].X + rectangles[i].Width;
                int y = rectangles[i].Y + rectangles[i].Height;

                if (maze.edges1.Any(obj => (obj.from == i && obj.to == i + 1) || (obj.from == i + 1 && obj.to == i)))
                {

                    Vector2 startPoint = new Vector2(x, y);
                    Vector2 endPoint = new Vector2(x, y - rectangles[i].Height);
                    Vector2 edge = endPoint - startPoint;
                    float angle = (float)Math.Atan2(edge.Y, edge.X);

                    m_spriteBatch.Draw(
                        pixel,
                        new Rectangle(x,y, (int)edge.Length(), 1),
                        null,
                        Color.Black,
                            angle,
                            new Vector2(0, 0),
                            SpriteEffects.None,
                            0
                       );
                }

                if (maze.edges1.Any(obj => (obj.from == i && obj.to == i + GameWidth) || (obj.from == i + GameWidth && obj.to == i)))
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


                

            }

            m_spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}