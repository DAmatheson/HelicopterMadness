/* GameScene.cs
 * Purpose: Base class for a game scene
 * 
 * Revision History:
 *      Drew Matheson, 2014.11.04: Created
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelicopterMadness.Scenes.BaseScene
{
    /// <summary>
    ///     Base class for a Scene in the game
    /// </summary>
    public abstract class GameScene : DrawableGameComponent
    {
        private readonly GameComponentCollection components;
        private readonly List<IDrawable> drawables;
        private readonly List<IUpdateable> updateables;

        protected readonly SpriteBatch spriteBatch;

        /// <summary>
        ///     Gets the component collection for the GameScene
        /// </summary>
        protected GameComponentCollection Components
        {
            get
            {
                return components;
            }
        }

        /// <summary>
        ///     Initializes a new instance of GameScene with the provided parameters
        /// </summary>
        /// <param name="game">The Game the GameScene belongs to</param>
        /// <param name="spriteBatch">The SpriteBatch the GameScene will draw itself with</param>
        protected GameScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            components = new GameComponentCollection();
            drawables = new List<IDrawable>();
            updateables = new List<IUpdateable>();

            this.spriteBatch = spriteBatch;

            components.ComponentAdded += GameComponentAdded;
            components.ComponentRemoved += GameComponentRemoved;
        }

        /// <summary>
        ///     Responds to a component being added to components
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The GameComponentCollectionEventArgs for the event</param>
        private void GameComponentAdded(object sender, GameComponentCollectionEventArgs e)
        {
            IDrawable drawable = e.GameComponent as IDrawable;

            if (drawable != null)
            {
                drawables.Add(drawable);

                drawables.Sort(
                    (drawable1, drawable2) => drawable1.DrawOrder.CompareTo(drawable2.DrawOrder));
            }

            IUpdateable updateable = e.GameComponent as IUpdateable;

            if (updateable != null)
            {
                updateables.Add(updateable);

                updateables.Sort(
                    (updateable1, updateable2) =>
                        updateable1.UpdateOrder.CompareTo(updateable2.UpdateOrder));
            }
        }

        /// <summary>
        ///     Responds to a component being removed from components
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The GameComponentCollectionEventArgs for the event</param>
        private void GameComponentRemoved(object sender, GameComponentCollectionEventArgs e)
        {
            IDrawable drawable = e.GameComponent as IDrawable;

            if (drawable != null)
            {
                drawables.Remove(drawable);
            }

            IUpdateable updateable = e.GameComponent as IUpdateable;

            if (updateable != null)
            {
                updateables.Remove(updateable);
            }
        }

        /// <summary>
        ///     Updates the Gamescene and all of its enabled components
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (IUpdateable updateable in updateables)
            {
                if (updateable.Enabled)
                {
                    updateable.Update(gameTime);
                }
            }
        }

        /// <summary>
        ///     Draws the GameScene and all of its visible components
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            foreach (IDrawable drawable in drawables)
            {
                if (drawable.Visible)
                {
                    drawable.Draw(gameTime);
                }
            }
        }

        /// <summary>
        ///     Shows and enables the GameScene
        /// </summary>
        public virtual void Show()
        {
            Enabled = true;
            Visible = true;
        }


        /// <summary>
        ///     Hides and disables the GameScene
        /// </summary>
        public virtual void Hide()
        {
            Enabled = false;
            Visible = false;
        }
    }
}