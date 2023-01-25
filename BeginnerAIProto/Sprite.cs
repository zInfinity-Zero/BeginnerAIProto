using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeginnerAIProto
{
    class Sprite
    {
        public Vector2 spritePosition { get; set; }
        public Vector2 spriteSize { get; set; }
        public Texture2D spriteTexture { get; set; }

        public Color spriteColor { get; set; } = Color.White;

        public Sprite()
        {

        }

        public Sprite(Texture2D tex, Vector2 pos, Vector2 size)
        {
            this.spriteTexture = tex;
            this.spritePosition = pos;
            this.spriteSize = size;
        }


        public void DrawSprite(SpriteBatch s, Texture2D t)
        {

            s.Begin();

            s.Draw(t, new Rectangle((int)spritePosition.X, (int)spritePosition.Y, (int)spriteSize.X, (int)spriteSize.Y), spriteColor);

            s.End();


        }





    }
}