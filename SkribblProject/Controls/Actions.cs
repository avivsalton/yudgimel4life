using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SkribblProject.Controls
{
    class ColorAction
    {
        Texture2D black;
        Texture2D blue;
        Texture2D red;
        Texture2D yellow;
        Texture2D green;

        Color currColor;
        Color[] blackD = new Color[10 * 10];
        Color[] blueD = new Color[10 * 10];
        Color[] redD = new Color[10 * 10];
        Color[] yellowD = new Color[10 * 10];
        Color[] greenD = new Color[10 * 10];

        public ColorAction(GraphicsDevice GraphicsDevice)
        {
            this.black = new Texture2D(GraphicsDevice, 10, 10);
            this.blue = new Texture2D(GraphicsDevice, 10, 10);
            this.red = new Texture2D(GraphicsDevice, 10, 10);
            this.yellow = new Texture2D(GraphicsDevice, 10, 10);
            this.green = new Texture2D(GraphicsDevice, 10, 10);

            this.currColor = Color.Black;
            for (int i = 0; i < blackD.Length; ++i)
            {
                this.blackD[i] = Color.Black;
                this.blueD[i] = Color.Blue;
                this.redD[i] = Color.Red;
                this.yellowD[i] = Color.Yellow;
                this.greenD[i] = Color.Green;
            }

            black.SetData(blackD);
            blue.SetData(blueD);
            red.SetData(redD);
            yellow.SetData(yellowD);
            green.SetData(greenD);
        }

        public void changeColor(Color color)
        {
            this.currColor = color;
        }

        public Color[] getData()
        {
            if (this.currColor == Color.Black)
                return blackD;
            if (this.currColor == Color.Blue)
                return blueD;
            if (this.currColor == Color.Red)
                return redD;
            if (this.currColor == Color.Yellow)
                return yellowD;
            if (this.currColor == Color.Green)
                return greenD;
            return null;
        }

        public Texture2D getRect()
        {
            if (this.currColor == Color.Black)
                return black;
            if (this.currColor == Color.Blue)
                return blue;
            if (this.currColor == Color.Red)
                return red;
            if (this.currColor == Color.Yellow)
                return yellow;
            if (this.currColor == Color.Green)
                return green;
            return null;
        }
    }
}
