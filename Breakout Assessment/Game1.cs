using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Breakout_Assessment
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Velocity-based code
        const int Winwidth = 800;
        const int Winheight = 600;
        Vector2 Velocity = new Vector2(0.20f, 0.20f);

        // Call-outs for Textures and Vector Classes
        Texture2D Ball;
        Vector2 Ballpos;

        Texture2D Player1;
        Vector2 Player1pos;

        Texture2D Background;
        Vector2 Backgroundpos;

        Texture2D[] Bricks = new Texture2D[40];
        Vector2[] Brickspos = new Vector2[40];

        Rectangle Player1hitbox;
        Rectangle Brickshitbox;
        Rectangle Ballhitbox;

        List<Texture2D> BrickSprite = new List<Texture2D>();
        SpriteFont somerandomfont;

        int score = 0;
        int count = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Winwidth;
            graphics.PreferredBackBufferHeight = Winheight;
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Ball = Content.Load<Texture2D>("Ballhedition");
            Ballpos = new Vector2(384.0f, 250.0f);

            Player1 = Content.Load<Texture2D>("player1he");
            Player1pos = new Vector2(380.0f, 550.0f);

            for (int count = 0; count <= 0; count++)
            {
                Bricks[count] = Content.Load<Texture2D>("Brick");
            }

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                  Bricks[count] = Content.Load<Texture2D>("Brick");
                  Brickspos[count].X = 55 + (x * 95);
                  Brickspos[count].Y = 20 + (y * 40);
                  count++;
                }
            }

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            // Keyboard Stuff
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.A))
            {
                Player1pos.X -= 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                Player1pos.X += 0.5f * gameTime.ElapsedGameTime.Milliseconds;
            }

            // Ball movements
            Ballpos.X += (int)(Velocity.X * gameTime.ElapsedGameTime.Milliseconds);
            Ballpos.Y += (int)(Velocity.Y * gameTime.ElapsedGameTime.Milliseconds);
            BounceTopBottom();

            // Hitboxes and collision detection
            Player1hitbox = new Rectangle((int)Player1pos.X, (int)Player1pos.Y, Player1.Width, Player1.Height);
            Ballhitbox = new Rectangle((int)Ballpos.X, (int)Ballpos.Y, Ball.Width, Ball.Height);
            //Brickshitbox = new Rectangle((int)Brickspos[count].X, (int)Brickspos[count].Y, Bricks[count].Width, Bricks[count].Height);

            // Check if collides
            if (Player1hitbox.Intersects(Ballhitbox))
            {
                Velocity.Y *= -1;
            }

            if (Brickshitbox.Intersects(Ballhitbox))
            {
                Velocity.Y *= -1;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Spritebatch stuff
            spriteBatch.Begin();

            //Actual stuff
            spriteBatch.Draw(Player1, Player1pos, Color.White);
            spriteBatch.Draw(Ball, Ballpos, Color.White);

            for (count = 0; count < 40; count++)
            {
                spriteBatch.Draw(Bricks[count], Brickspos[count], Color.White);
            }

                spriteBatch.End();



            base.Draw(gameTime);
        }

        private void BounceTopBottom()

            // Check for Ballposition reaching outside of gamewindow return velocity if it does.
        {
            // bounce off top
            if (Ballpos.Y < 0)
            {
                
                Velocity.Y *= -1;
            }
            // bounce off bottom
            else if (Ballpos.Y > (Winheight - Ball.Height))
            {
                
                Velocity.Y *= -1;
            }

                //bounce off sides
            if (Ballpos.X < 0)
            {
                Velocity.X *= -1;
            }
            else if (Ballpos.X > (Winwidth - Ball.Width))
            {
                Velocity.X *= -1;
            }
        }
    }
}
