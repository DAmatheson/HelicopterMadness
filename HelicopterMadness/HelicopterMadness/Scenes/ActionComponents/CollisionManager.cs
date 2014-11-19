/* CollisionManager.cs
 * Purpose: Manages collision detection for objects that implement ICollidable
 * 
 * Revision History:
 *      Drew Matheson, 2014.10.31: Created
 */

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace HelicopterMadness.Scenes.ActionComponents
{
    /// <summary>
    ///     Manages collisions and notifies ICollidables if they have collided with another ICollidable
    /// </summary>
    public class CollisionManager : GameComponent
    {
        private readonly List<ICollidable> collidingComponents;
        private readonly ICollidable playerComponent;

        /// <summary>
        ///     Initializes a new instance of CollisionManager with the provided arguments
        /// </summary>
        /// <param name="game">The game the CollisionManager belongs to</param>
        /// <param name="collidingComponents">The enumerable of ICollidable objects to manager</param>
        /// <param name="playerComponent">The ICollidable that the player controls</param>
        public CollisionManager(Game game, IEnumerable<ICollidable> collidingComponents, ICollidable playerComponent) : base(game)
        {
            this.collidingComponents = new List<ICollidable>(collidingComponents);
            this.playerComponent = playerComponent;
        }

        /// <summary>
        ///     Checks to see if any ICollidable components have collided
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (playerComponent.Enabled)
            {
                foreach (var collidable in collidingComponents)
                {
                    if (playerComponent.GetBounds().Intersects(collidable.GetBounds()))
                    {
                        collidable.OnCollision(playerComponent);
                        playerComponent.OnCollision(collidable);
                    }
                }
            }
        }

        /// <summary>
        ///     Adds an IEnumerable of ICollidables to the list of collision managed objects
        /// </summary>
        /// <param name="items">The IEnumerable of ICollidables to add to the CollisionManager</param>
        public void AddCollidableRange(IEnumerable<ICollidable> items)
        {
            collidingComponents.AddRange(items);
        }
    }
}