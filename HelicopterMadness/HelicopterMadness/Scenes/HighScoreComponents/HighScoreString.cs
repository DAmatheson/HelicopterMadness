using System;
using System.Collections.Generic;
using System.Linq;
using HelicopterMadness.Scenes.CommonComponents;
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
    public class HighScoreString : TextDisplay
    {
        private int delay = 15;
        private bool flashFlag = false;
        private int delayCounter;
        public HighScoreString(Game game, SpriteBatch spriteBatch, SpriteFont font, Vector2 position,Color color)
            : base(game, spriteBatch, font, position, color)
        {
            // TODO: Construct any child components here


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

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            //oldKeyState = KeyState;
            //KeyState = Keyboard.GetState();

            delayCounter++;
            if (delayCounter > delay)
            {
                delayCounter = 0;
                flashFlag = !flashFlag;
            }

            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (flashFlag)
            {
                spriteBatch.DrawString(Font, Message, Position, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            }
            base.Draw(gameTime);
        }
    }
}
