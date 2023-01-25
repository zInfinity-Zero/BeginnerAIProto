using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

namespace BeginnerAIProto
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Sprite[,] ff = new Sprite[3, 3];
        private Sprite[,] rf = new Sprite[3, 3];
        private Sprite[,] lf = new Sprite[3, 3];
        private Sprite[,] uf = new Sprite[3, 3];
        private Sprite[,] df = new Sprite[3, 3];
        private Sprite[,] bf = new Sprite[3, 3];
        private CubeM cube = new CubeM();
        private Texture2D tex;

        // beginnerai variables
        private Color[][,] storedarray = new Color[6][,];
        private int[,] edges = new int[2, 4]
        {
            { 0,1,1,2 },
            { 1,0,2,1 }
        };
        List<int> solvedstringlist = new List<int>();
        private string solvetextstring;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.PreferredBackBufferWidth = 1800;
            _graphics.ApplyChanges();
            cube.InitialiseCube();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            tex = Content.Load<Texture2D>("rubik");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    ff[i, j] = new Sprite(tex, new Vector2(500 + 100 * i, 300 + 100 * j), new Vector2(100, 100));
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    rf[i, j] = new Sprite(tex, new Vector2(800 + 100 * i, 300 + 100 * j), new Vector2(100, 100));
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    lf[i, j] = new Sprite(tex, new Vector2(200 + 100 * i, 300 + 100 * j), new Vector2(100, 100));
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    df[i, j] = new Sprite(tex, new Vector2(500 + 100 * i, 600 + 100 * j), new Vector2(100, 100));
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    uf[i, j] = new Sprite(tex, new Vector2(500 + 100 * i, 100 * j), new Vector2(100, 100));
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    bf[i, j] = new Sprite(tex, new Vector2(1300 + 100 * i, 100 + 100 * j), new Vector2(100, 100));
                }
            }
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Color temp = cube.cubeorientationfront[i, j];
                    ff[i, j].spriteColor = temp;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Color temp = cube.cubeorientationup[i, j];
                    uf[i, j].spriteColor = temp;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Color temp = cube.cubeorientationdown[i, j];
                    df[i, j].spriteColor = temp;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Color temp = cube.cubeorientationright[i, j];
                    rf[i, j].spriteColor = temp;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Color temp = cube.cubeorientationleft[i, j];
                    lf[i, j].spriteColor = temp;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Color temp = cube.cubeorientationback[i, j];
                    bf[i, j].spriteColor = temp;
                }
            }
            //beginner ai codes
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                if (cube.cubeorientationfront[1, 1] != Color.Green || cube.cubeorientationup[1, 1] != Color.Yellow)//if (wholecube[0][1, 1] != Color.Blue || wholecube[2][1, 1] != Color.Yellow)
                {
                    cube.YTurn();
                    cube.YTurn();
                    cube.XTurn();
                    cube.XTurn();
                }
                Thread.Sleep(200);




                for (int z = 0; z <= 5; z++)//5 is storedarray.Length
                {
                    for (int i = 0; i < 4; i++)//4 is edges.getupperbound
                    {
                        if (cube.wholecube[z][edges[i, 0], edges[i, 1]] == Color.White)//check all edges of faces to see if the are white
                        {
                            if (edges[i, 0] == 0)//only the [0,1] case
                            {    //left edge

                                switch (z)
                                {
                                    case 0:
                                        if (cube.cubeorientationleft[2, 1] == Color.Red)//check witht the face's left face's 2,1(FOLLOW ^^^ PATTERN FBTDRL
                                        {
                                            cube.Turn(7);
                                            solvetextstring += "7";
                                            solvedstringlist.Add(7);
                                        }
                                        break;
                                    case 1:
                                        if (cube.cubeorientationright[2, 1] == Color.Red)
                                        {
                                            cube.Turn(1);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(7);

                                            solvetextstring += "15577";
                                            solvedstringlist.Add(1);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);

                                        }
                                        break;
                                    case 2:
                                        if (cube.cubeorientationleft[0, 1] == Color.Red)
                                        {
                                            cube.Turn(7);
                                            Thread.Sleep(150);

                                            cube.Turn(7);
                                            solvetextstring += "77";
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);

                                        }
                                        break;
                                    case 3:
                                        if (cube.cubeorientationleft[0, 2] == Color.Red)
                                        {
                                            //in place
                                        }
                                        break;
                                    case 4:
                                        if (cube.cubeorientationfront[2, 1] == Color.Red)
                                        {
                                            cube.Turn(3);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(7);

                                            solvetextstring += "3577";
                                            solvedstringlist.Add(3);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);


                                        }
                                        break;
                                    case 5:
                                        if (cube.cubeorientationback[2, 1] == Color.Red)
                                        {
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(6);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            solvetextstring += "776577";
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(6);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);

                                        }
                                        break;

                                }
                            }
                            


                            else if (edges[i, 0] == 1 && edges[i, 1] == 0) // [1,0] case
                            {//top middle edge
                                switch (z)
                                {
                                    case 0:
                                        if (cube.cubeorientationup[1, 2] == Color.Red)
                                        {
                                            cube.Turn(3);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            solvetextstring += "37";
                                            solvedstringlist.Add(3);
                                            solvedstringlist.Add(7);

                                        }
                                        break;
                                    case 1:
                                        if (cube.cubeorientationup[1, 0] == Color.Red)
                                        {
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(3);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            solvetextstring += "5537";
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(3);
                                            solvedstringlist.Add(7);

                                        }
                                        break;
                                    case 2:
                                        if (cube.cubeorientationback[1, 0] == Color.Red)
                                        {
                                            cube.Turn(2);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(7);

                                            solvetextstring += "277";
                                            solvedstringlist.Add(2);
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);

                                        }
                                        break;
                                    case 3:
                                        if (cube.cubeorientationfront[1, 2] == Color.Red)
                                        {
                                            cube.Turn(10);
                                            Thread.Sleep(150);
                                            cube.Turn(6);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(7);

                                            solvedstringlist.Add(6);
                                            solvedstringlist.Add(6);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);

                                            solvetextstring += "66577";
                                        }
                                        break;
                                    case 4:
                                        if (cube.cubeorientationup[2, 1] == Color.Red)
                                        {
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(3);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(3);
                                            solvedstringlist.Add(7);

                                            solvetextstring += "537";
                                        }
                                        break;
                                    case 5:
                                        if (cube.cubeorientationup[0, 1] == Color.Red)
                                        {
                                            cube.Turn(2);
                                            Thread.Sleep(150);
                                            cube.Turn(3);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            solvedstringlist.Add(2);
                                            solvedstringlist.Add(3);
                                            solvedstringlist.Add(7);

                                            solvetextstring += "237";
                                        }
                                        break;


                                }
                            }
                            else if (edges[i, 0] == 1 && edges[i, 1] == 2) // [1,2] case
                            { //middle bottom
                                switch (z)
                                {
                                    case 0:
                                        if (cube.cubeorientationdown[1, 0] == Color.Red)
                                        {
                                            cube.Turn(6);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            solvedstringlist.Add(6);
                                            solvedstringlist.Add(7);

                                            solvetextstring += "67";
                                        }
                                        break;
                                    case 1:
                                        if (cube.cubeorientationdown[1, 2] == Color.Red)
                                        {
                                            cube.Turn(50);
                                            Thread.Sleep(150);
                                            cube.Turn(50);
                                            Thread.Sleep(150);
                                            cube.Turn(6);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(6);
                                            solvedstringlist.Add(7);

                                            solvetextstring += "5567";
                                        }
                                        break;
                                    case 2:
                                        if (cube.cubeorientationfront[0, 1] == Color.Red)
                                        {
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);

                                            solvetextstring += "577";
                                        }
                                        break;
                                    case 3:
                                        if (cube.cubeorientationback[1, 2] == Color.Red)
                                        {
                                            //rotate cube up down
                                            cube.YTurn();
                                            Thread.Sleep(150);
                                            cube.YTurn();
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.YTurn();
                                            Thread.Sleep(150);
                                            cube.YTurn();

                                            solvedstringlist.Add(5);

                                            solvetextstring += "5";
                                            //rotate cube up down
                                        }
                                        break;
                                    case 4:
                                        if (cube.cubeorientationdown[2, 1] == Color.Red)
                                        {
                                            cube.Turn(4);
                                            Thread.Sleep(150);
                                            cube.Turn(3);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(7);

                                            solvedstringlist.Add(4);
                                            solvedstringlist.Add(3);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);

                                            solvetextstring += "43577";
                                        }
                                        break;
                                    case 5:
                                        if (cube.cubeorientationdown[0, 1] == Color.Red)
                                        {
                                            cube.Turn(8);
                                            Thread.Sleep(150);
                                            cube.Turn(6);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            solvedstringlist.Add(8);
                                            solvedstringlist.Add(6);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);

                                            solvetextstring += "86577";
                                        }
                                        break;


                                }
                            }
                            else if (edges[i, 0] == 2 || edges[i, 1] == 1) // [2,1] case
                            { //right
                                switch (z)
                                {
                                    case 0:
                                        if (cube.cubeorientationright[0, 1] == Color.Red)
                                        {
                                            cube.Turn(4);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            solvedstringlist.Add(4);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);

                                            solvetextstring += "45577";
                                        }
                                        break;
                                    case 1:
                                        if (cube.cubeorientationleft[0, 1] == Color.Red)
                                        {
                                            cube.Turn(8);
                                            solvetextstring += "8";
                                            solvedstringlist.Add(8);

                                        }
                                        break;
                                    case 2:
                                        if (cube.cubeorientationright[1, 0] == Color.Red)
                                        {
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(7);

                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);

                                            solvetextstring += "5577";
                                        }
                                        break;
                                    case 3:
                                        if (cube.cubeorientationright[1, 2] == Color.Red)
                                        {
                                            cube.Turn(4);
                                            Thread.Sleep(150);
                                            cube.Turn(4);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            solvedstringlist.Add(4);
                                            solvedstringlist.Add(4);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);

                                            solvetextstring += "445577";
                                        }
                                        break;
                                    case 4:
                                        if (cube.cubeorientationback[0, 1] == Color.Red)
                                        {
                                            cube.Turn(1);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(3);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            solvedstringlist.Add(1);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(3);
                                            solvedstringlist.Add(7);

                                        }
                                        break;
                                    case 5:
                                        if (cube.cubeorientationfront[0, 1] == Color.Red)
                                        {
                                            cube.Turn(6);
                                            Thread.Sleep(150);
                                            cube.Turn(5);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            Thread.Sleep(150);
                                            cube.Turn(7);
                                            solvedstringlist.Add(6);
                                            solvedstringlist.Add(5);
                                            solvedstringlist.Add(7);
                                            solvedstringlist.Add(7);

                                        }
                                        break;


                                }
                            }








                        }
                    }
                }

            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            foreach (Sprite s in ff)
            {
                s.DrawSprite(_spriteBatch, s.spriteTexture);
            }
            foreach (Sprite s in rf)
            {
                s.DrawSprite(_spriteBatch, s.spriteTexture);
            }
            foreach (Sprite s in lf)
            {
                s.DrawSprite(_spriteBatch, s.spriteTexture);
            }
            foreach (Sprite s in uf)
            {
                s.DrawSprite(_spriteBatch, s.spriteTexture);
            }
            foreach (Sprite s in df)
            {
                s.DrawSprite(_spriteBatch, s.spriteTexture);
            }
            foreach (Sprite s in bf)
            {
                s.DrawSprite(_spriteBatch, s.spriteTexture);
            }
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}