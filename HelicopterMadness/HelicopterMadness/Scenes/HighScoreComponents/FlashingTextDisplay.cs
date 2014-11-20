using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace HelicopterMadness.Scenes.HighScoreComponents
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class FlashingTextDisplay : DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        protected string message;
        protected SpriteFont font;
        protected Color color;
        protected Vector2 position;
        protected int delay;
        protected int delayCounter;
        protected bool toDraw;

        public FlashingTextDisplay(Game game, SpriteBatch spriteBatch, SpriteFont font, 
             Color color, int delay)
            : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.position = position;
            this.color = color;
            this.delay = delay;

            Stop();
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        public void Start()
        {
            Enabled = true;
            Visible = true;
        }

        public void Stop()
        {
            Enabled = false;
            Visible = false;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                delayCounter = 0;
                toDraw = !toDraw;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (toDraw)
            {
                spriteBatch.DrawString(font, Message, position, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }

            base.Draw(gameTime);
        }
    }
}
