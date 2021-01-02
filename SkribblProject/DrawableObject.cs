using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SkribblProject
{
    class DrawableObject
    {
        #region Data
        Texture2D texture;
        Vector2 position;
        Rectangle? sourceRectangle;
        Color color;
        float rotation;
        Vector2 origin;
        Vector2 scale; SpriteEffects effects;
        float layerDepth;
        #endregion

        #region ctor
        public DrawableObject(Texture2D texture, Vector2 position, Rectangle? sourceRectangle,
            Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            this.texture = texture;
            this.position = position;
            this.sourceRectangle = sourceRectangle;
            this.color = color;
            this.rotation = rotation;
            this.origin = origin;
            this.scale = scale;
            this.effects = effects;
            this.layerDepth = layerDepth;
        }
        #endregion

        public void draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public float getWidth()
        {
            return texture.Width;
        }

        public float getHeight()
        {
            return texture.Height;
        }

        public Texture2D getTexture()
        {
            return texture;
        }
    }
}
