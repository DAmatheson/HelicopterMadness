/* ICollidable.cs
 * Purpose: Interface for objects that require collision detection and management
 * 
 * Revision History:
 *      Drew Matheson, 2014.10.30: Created
 */

using Microsoft.Xna.Framework;

namespace HelicopterMadness.Scenes.ActionComponents
{
    /// <summary>
    ///     Represents an object that can collide with other objects
    /// </summary>
    public interface ICollidable : IUpdateable
    {
        /// <summary>
        ///     Gets the bounds of the collidable object
        /// </summary>
        /// <returns>The <see cref="Rectangle"/> bounds of the object</returns>
        Rectangle GetBounds();

        /// <summary>
        ///     Used to notify the object that it has been in a collision
        /// </summary>
        /// <param name="otherCollidable">The ICollidable the object collided with</param>
        void OnCollision(ICollidable otherCollidable);
    }
}