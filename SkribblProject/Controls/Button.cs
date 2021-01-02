using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkribblProject.Controls
{
    class Button : DrawableObject
    {
        Texture2D texture;
        Vector2 position;
        Color color;
        Vector2 origin;
        Status stat = Status.None;

        public Button(Texture2D texture, Vector2 position, Color color, Vector2 origin) : base(texture, position, null, color, 0, origin, new Vector2(0.5f), SpriteEffects.None, 0)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
            this.origin = origin;
        }

        public Status getStatus()
        {
            if (Mouse.GetState().X >= getPosition().X && Mouse.GetState().X <= getPosition().X + getWidth()
                && Mouse.GetState().Y >= getPosition().Y && Mouse.GetState().Y <= getPosition().Y + getHeight()
                && Mouse.GetState().LeftButton != ButtonState.Pressed)

                stat = Status.Hovered;

            else if (Mouse.GetState().X >= getPosition().X && Mouse.GetState().X <= getPosition().X + getWidth()
                && Mouse.GetState().Y >= getPosition().Y && Mouse.GetState().Y <= getPosition().Y + getHeight()
                && Mouse.GetState().LeftButton == ButtonState.Pressed)

                stat = Status.Clicked;

            else
                stat = Status.None;

            return stat;
        }

        public Color getColor()
        {
            return this.color;
        }

    }
}