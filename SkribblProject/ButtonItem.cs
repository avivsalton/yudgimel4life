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
    class ButtonItem
    {
        Texture2D texture;
        Vector2 position;
        Color color;
        Vector2 origin;
        Controls.Status stat = Controls.Status.None;

        public ButtonItem(Texture2D texture, Vector2 position, Color color, Vector2 origin)
        {
            this.texture = texture;
            this.position = position;
            this.color = color;
            this.origin = origin;
        }

        public Controls.Status getStatus()
        {
            if (Mouse.GetState().X >= this.position.X && Mouse.GetState().X <= this.position.X + this.texture.Width
                && Mouse.GetState().Y >= this.position.Y && Mouse.GetState().Y <= this.position.Y + this.texture.Height
                && Mouse.GetState().LeftButton != ButtonState.Pressed)

                stat = Controls.Status.Hovered;

            else if (Mouse.GetState().X >= this.position.X && Mouse.GetState().X <= this.position.X + this.texture.Width
                && Mouse.GetState().Y >= this.position.Y && Mouse.GetState().Y <= this.position.Y + this.texture.Height
                && Mouse.GetState().LeftButton == ButtonState.Pressed)

                stat = Controls.Status.Clicked;

            else
                stat = Controls.Status.None;

            return stat;
        }

        public void draw(SpriteBatch sb)
        {
            sb.Draw(texture, position, null, color, 0, origin, new Vector2(0.3f), SpriteEffects.None, 0);
        }
    }
}
