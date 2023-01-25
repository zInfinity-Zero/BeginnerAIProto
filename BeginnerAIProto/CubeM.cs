//using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharpDX.Direct3D9;
using System.Transactions;
using SharpDX.DirectWrite;

namespace BeginnerAIProto
{
    internal class CubeM
    {

        public Color[,] gFace, bFace, wFace, yFace, rFace, oFace;
        public Color[][,] solvedcube = new Color[6][,];
        private Color[,] tempgFace, tempbFace, tempwFace, tempyFace, temprFace, tempoFace;
        public bool finishgame { get; set; } = false;
        public Color[,] cubeorientationfront, cubeorientationright, cubeorientationleft, cubeorientationup, cubeorientationdown, cubeorientationback;
        public Color[][,] wholecube = new Color[6][,];
        private bool started = false;
        public int numofmoves { get; set; } = 0;

        private GameTime gtime;

        public CubeM()
        {

        }
        public void UpdateCube( GameTime g)
        {
            wholecube = new Color[6][,]
            {
                cubeorientationfront,cubeorientationback,cubeorientationup,cubeorientationdown,cubeorientationright,cubeorientationleft
            };
            if (started && wholecube == solvedcube)//cubeorientationfront[0,0] == gFace[0,0])//&& cubeorientationdown==yFace && cubeorientationleft== oFace)
            {
                finishgame = true;
            }
            KeyboardState kstate = Keyboard.GetState();

            gtime = g;

            if (kstate.IsKeyDown(Keys.X))
            {
                XTurn();


                Thread.Sleep(150);
            }
            if (kstate.IsKeyDown(Keys.Y))
            {
                YTurn();


                Thread.Sleep(150);
            }
            


        }
        public void XTurn()
        {
            tempwFace = cubeorientationup;
            tempyFace = cubeorientationdown;

            Color[,] front, back, right, left, up, down;
            front = cubeorientationfront;
            back = cubeorientationback;
            left = cubeorientationleft;
            right = cubeorientationright;

            cubeorientationfront = right;
            cubeorientationback = left;
            cubeorientationleft = front;
            cubeorientationright = back;

            Color d = cubeorientationup[2, 0];
            Color e = cubeorientationup[0, 0];
            Color f = cubeorientationup[1, 0];
            cubeorientationup[0, 0] = tempwFace[0, 2];
            cubeorientationup[1, 0] = tempwFace[0, 1];
            cubeorientationup[2, 0] = e;
            cubeorientationup[0, 1] = tempwFace[1, 2];
            cubeorientationup[0, 2] = tempwFace[2, 2];
            cubeorientationup[1, 2] = tempwFace[2, 1];
            cubeorientationup[2, 1] = f;
            cubeorientationup[2, 2] = d;

            d = cubeorientationdown[0, 0];
            e = cubeorientationdown[1, 0];
            cubeorientationdown[0, 0] = tempyFace[2, 0];
            cubeorientationdown[1, 0] = tempyFace[2, 1];
            cubeorientationdown[2, 0] = tempyFace[2, 2];
            cubeorientationdown[2, 1] = tempyFace[1, 2];
            cubeorientationdown[2, 2] = tempyFace[0, 2];
            cubeorientationdown[1, 2] = tempyFace[0, 1];
            cubeorientationdown[0, 2] = d;
            cubeorientationdown[0, 1] = e;

        }
        public void YTurn()
        {
            temprFace = cubeorientationright;
            tempoFace = cubeorientationleft;

            Color[,] front, back, right, left, up, down, tempback;
            front = this.cubeorientationfront;
            back = this.cubeorientationback;
            tempback = this.cubeorientationback;
            up = this.cubeorientationup;
            down = this.cubeorientationdown;


            this.cubeorientationup = front;

            this.cubeorientationfront = down;

            this.cubeorientationback = up;

            this.cubeorientationdown = back;

            Color a = cubeorientationback[0, 2];
            Color b = cubeorientationback[1, 2];
            Color c = cubeorientationback[2, 2];
            back = this.cubeorientationback;
            this.cubeorientationback[0, 1] = back[2, 1];
            this.cubeorientationback[1, 1] = back[1, 1];
            this.cubeorientationback[2, 1] = back[0, 1];
            this.cubeorientationback[2, 2] = back[0, 0];
            this.cubeorientationback[1, 2] = back[1, 0];
            this.cubeorientationback[0, 2] = back[2, 0];//alteration as cant do cubeorientationback = down
            this.cubeorientationback[2, 0] = a;
            this.cubeorientationback[1, 0] = b;
            this.cubeorientationback[0, 0] = c;

            a = cubeorientationdown[0, 2];
            b = cubeorientationdown[1, 2];
            c = cubeorientationdown[2, 2];
            down = this.cubeorientationdown;
            this.cubeorientationdown[0, 1] = down[2, 1];
            this.cubeorientationdown[1, 1] = down[1, 1];
            this.cubeorientationdown[2, 1] = down[0, 1];
            this.cubeorientationdown[2, 2] = down[0, 0];
            this.cubeorientationdown[1, 2] = down[1, 0];
            this.cubeorientationdown[0, 2] = down[2, 0];//alteration 
            this.cubeorientationdown[2, 0] = a;
            this.cubeorientationdown[1, 0] = b;
            this.cubeorientationdown[0, 0] = c;






            Color d = cubeorientationright[2, 0];
            Color e = cubeorientationright[0, 0];
            Color f = cubeorientationright[1, 0];//right face 4 turn
            cubeorientationright[0, 0] = temprFace[0, 2];
            cubeorientationright[1, 0] = temprFace[0, 1];
            cubeorientationright[2, 0] = e;
            cubeorientationright[0, 1] = temprFace[1, 2];
            cubeorientationright[0, 2] = temprFace[2, 2];
            cubeorientationright[1, 2] = temprFace[2, 1];
            cubeorientationright[2, 1] = f;
            cubeorientationright[2, 2] = d;



            //sa
            d = cubeorientationleft[0, 1];
            e = cubeorientationleft[0, 0];
            f = cubeorientationleft[1, 0];
            Color g = cubeorientationleft[1, 2];
            Color h = cubeorientationleft[0, 2];
            cubeorientationleft[0, 0] = tempoFace[2, 0];
            cubeorientationleft[1, 0] = tempoFace[2, 1];
            cubeorientationleft[2, 0] = tempoFace[2, 2];
            cubeorientationleft[0, 1] = f;
            cubeorientationleft[0, 2] = e;
            cubeorientationleft[1, 2] = d;
            cubeorientationleft[2, 1] = g;
            cubeorientationleft[2, 2] = h;

        }

        public void InitialiseCube()
        {
            cubeorientationfront = new Color[3, 3]
            {
                { Color.Green,Color.Green,Color.Green},
                 { Color.Green,Color.Green,Color.Green},
                { Color.Green,Color.Green,Color.Green}
            };
            cubeorientationback = new Color[3, 3]
            {
                { Color.Blue, Color.Blue, Color.Blue},
                 {  Color.Blue, Color.Blue, Color.Blue}
                ,{  Color.Blue, Color.Blue, Color.Blue}
            };
            cubeorientationup = new Color[3, 3]
            {
                {  Color.White, Color.White, Color.White},
                {  Color.White, Color.White, Color.White}
                ,{ Color.White, Color.White, Color.White}
            };
            cubeorientationdown = new Color[3, 3]
            {
                { Color.Yellow,Color.Yellow,Color.Yellow},
                 { Color.Yellow,Color.Yellow,Color.Yellow},
                { Color.Yellow,Color.Yellow,Color.Yellow}
            };
            cubeorientationright = new Color[3, 3]
            {
                { Color.Red, Color.Red, Color.Red},
                 {  Color.Red, Color.Red, Color.Red}
                ,{  Color.Red, Color.Red, Color.Red}
            };
            cubeorientationleft = new Color[3, 3]
            {
                {  Color.Orange, Color.Orange, Color.Orange},
                {  Color.Orange, Color.Orange, Color.Orange}
                ,{ Color.Orange, Color.Orange, Color.Orange}
            };

            gFace = new Color[3, 3]
            {
                { Color.Green,Color.Green,Color.Green},
                 { Color.Green,Color.Green,Color.Green},
                { Color.Green,Color.Green,Color.Green}
            };
            bFace = new Color[3, 3]
            {
                { Color.Blue, Color.Blue, Color.Blue},
                 {  Color.Blue, Color.Blue, Color.Blue}
                ,{  Color.Blue, Color.Blue, Color.Blue}
            };
            wFace = new Color[3, 3]
            {
                {  Color.White, Color.White, Color.White},
                {  Color.White, Color.White, Color.White}
                ,{ Color.White, Color.White, Color.White}
            };
            yFace = new Color[3, 3]
            {
                { Color.Yellow,Color.Yellow,Color.Yellow},
                 { Color.Yellow,Color.Yellow,Color.Yellow},
                { Color.Yellow,Color.Yellow,Color.Yellow}
            };
            rFace = new Color[3, 3]
            {
                { Color.Red, Color.Red, Color.Red},
                 {  Color.Red, Color.Red, Color.Red}
                ,{  Color.Red, Color.Red, Color.Red}
            };
            oFace = new Color[3, 3]
            {
                {  Color.Orange, Color.Orange, Color.Orange},
                {  Color.Orange, Color.Orange, Color.Orange}
                ,{ Color.Orange, Color.Orange, Color.Orange}
            };
            solvedcube = new Color[6][,]
            {
                gFace,bFace,wFace,yFace,rFace,oFace
            };
        }
        public void Turn(int turn)
        {
            tempgFace = cubeorientationfront;
            temprFace = cubeorientationright;
            tempbFace = cubeorientationback;
            tempoFace = cubeorientationleft;
            tempwFace = cubeorientationup;
            tempyFace = cubeorientationdown;
            if (turn == 5)
            {
                Color a = cubeorientationfront[0, 0];
                Color b = cubeorientationfront[1, 0];
                Color c = cubeorientationfront[2, 0];
                cubeorientationfront[0, 0] = temprFace[0, 0];
                cubeorientationfront[1, 0] = temprFace[1, 0];
                cubeorientationfront[2, 0] = temprFace[2, 0];

                cubeorientationright[0, 0] = tempbFace[0, 0];
                cubeorientationright[1, 0] = tempbFace[1, 0];
                cubeorientationright[2, 0] = tempbFace[2, 0];

                cubeorientationback[2, 0] = tempoFace[2, 0];
                cubeorientationback[1, 0] = tempoFace[1, 0];
                cubeorientationback[0, 0] = tempoFace[0, 0];


                cubeorientationleft[0, 0] = a;
                cubeorientationleft[1, 0] = b;
                cubeorientationleft[2, 0] = c;


                Color d = cubeorientationup[2, 0];
                Color e = cubeorientationup[0, 0];
                Color f = cubeorientationup[1, 0];
                cubeorientationup[0, 0] = tempwFace[0, 2];
                cubeorientationup[1, 0] = tempwFace[0, 1];
                cubeorientationup[2, 0] = e;
                cubeorientationup[0, 1] = tempwFace[1, 2];
                cubeorientationup[0, 2] = tempwFace[2, 2];
                cubeorientationup[1, 2] = tempwFace[2, 1];
                cubeorientationup[2, 1] = f;
                cubeorientationup[2, 2] = d;


                ///testing new rotating 2d array ///doesnt work
                //for (int i = 0; i <3; i++)
                //{
                //    for (int j = 0; j < 3; j++)
                //    {
                //        cubeorientationup[i, 2 - j] = tempwFace[j, i];
                //    }
                //}

            }
            else if (turn == 6)
            {
                Color a = cubeorientationup[0, 2];
                Color b = cubeorientationup[1, 2];
                Color c = cubeorientationup[2, 2];
                cubeorientationup[0, 2] = tempoFace[2, 2];
                cubeorientationup[1, 2] = tempoFace[2, 1];
                cubeorientationup[2, 2] = tempoFace[2, 0];

                cubeorientationleft[2, 0] = tempyFace[0, 0];
                cubeorientationleft[2, 1] = tempyFace[1, 0];
                cubeorientationleft[2, 2] = tempyFace[2, 0];

                cubeorientationdown[0, 0] = temprFace[0, 2];
                cubeorientationdown[1, 0] = temprFace[0, 1];
                cubeorientationdown[2, 0] = temprFace[0, 0];

                cubeorientationright[0, 0] = a;
                cubeorientationright[0, 1] = b;
                cubeorientationright[0, 2] = c;

                Color d = cubeorientationfront[2, 0];
                Color e = cubeorientationfront[0, 0];
                Color f = cubeorientationfront[1, 0];
                cubeorientationfront[0, 0] = tempgFace[0, 2];
                cubeorientationfront[1, 0] = tempgFace[0, 1];
                cubeorientationfront[2, 0] = e;
                cubeorientationfront[0, 1] = tempgFace[1, 2];
                cubeorientationfront[0, 2] = tempgFace[2, 2];
                cubeorientationfront[1, 2] = tempgFace[2, 1];
                cubeorientationfront[2, 1] = f;
                cubeorientationfront[2, 2] = d;

            }
            else if (turn == 4)
            {
                Color a = cubeorientationup[2, 2];
                Color b = cubeorientationup[2, 1];
                Color c = cubeorientationup[2, 0];
                cubeorientationup[2, 0] = tempgFace[2, 0];
                cubeorientationup[2, 1] = tempgFace[2, 1];
                cubeorientationup[2, 2] = tempgFace[2, 2];

                cubeorientationfront[2, 0] = tempyFace[2, 0];
                cubeorientationfront[2, 1] = tempyFace[2, 1];
                cubeorientationfront[2, 2] = tempyFace[2, 2];

                cubeorientationdown[2, 0] = tempbFace[0, 2];
                cubeorientationdown[2, 1] = tempbFace[0, 1];
                cubeorientationdown[2, 2] = tempbFace[0, 0];

                cubeorientationback[0, 0] = a;//tempwFace[2, 2];
                cubeorientationback[0, 1] = b;// tempwFace[2, 1];
                cubeorientationback[0, 2] = c;// tempwFace[2, 0];


                Color d = cubeorientationright[2, 0];
                Color e = cubeorientationright[0, 0];
                Color f = cubeorientationright[1, 0];
                cubeorientationright[0, 0] = temprFace[0, 2];
                cubeorientationright[1, 0] = temprFace[0, 1];
                cubeorientationright[2, 0] = e;
                cubeorientationright[0, 1] = temprFace[1, 2];
                cubeorientationright[0, 2] = temprFace[2, 2];
                cubeorientationright[1, 2] = temprFace[2, 1];
                cubeorientationright[2, 1] = f;
                cubeorientationright[2, 2] = d;

            }
            else if (turn == 7)
            {
                Color c = cubeorientationup[0, 2];
                Color b = cubeorientationup[0, 1];
                Color a = cubeorientationup[0, 0];
                cubeorientationup[0, 0] = tempbFace[2, 2];
                cubeorientationup[0, 1] = tempbFace[2, 1];
                cubeorientationup[0, 2] = tempbFace[2, 0];

                cubeorientationback[2, 0] = tempyFace[0, 2];
                cubeorientationback[2, 1] = tempyFace[0, 1];
                cubeorientationback[2, 2] = tempyFace[0, 0];

                cubeorientationdown[0, 0] = tempgFace[0, 0];
                cubeorientationdown[0, 1] = tempgFace[0, 1];
                cubeorientationdown[0, 2] = tempgFace[0, 2];

                cubeorientationfront[0, 0] = a;
                cubeorientationfront[0, 1] = b;
                cubeorientationfront[0, 2] = c;




                Color d = cubeorientationleft[2, 0];
                Color e = cubeorientationleft[0, 0];
                Color f = cubeorientationleft[1, 0];

                cubeorientationleft[0, 0] = tempoFace[0, 2];
                cubeorientationleft[1, 0] = tempoFace[0, 1];
                cubeorientationleft[2, 0] = e;
                cubeorientationleft[0, 1] = tempoFace[1, 2];
                cubeorientationleft[0, 2] = tempoFace[2, 2];
                cubeorientationleft[1, 2] = tempoFace[2, 1];
                cubeorientationleft[2, 1] = f;
                cubeorientationleft[2, 2] = d;

            }
            else if (turn == 1)
            {
                this.Turn(4);
                this.Turn(4);
                this.Turn(4);

            }
            else if (turn == 2)
            {
                this.Turn(5);
                this.Turn(5);
                this.Turn(5);

            }
            else if (turn == 3)
            {
                this.Turn(6);
                this.Turn(6);
                this.Turn(6);

            }
            else if (turn == 8)
            {
                this.Turn(7);
                this.Turn(7);
                this.Turn(7);

            }
            else if (turn == 0)
            {
                Color a = cubeorientationfront[1, 0];
                Color b = cubeorientationfront[1, 1];
                Color c = cubeorientationfront[1, 2];

                cubeorientationfront[1, 0] = tempwFace[1, 0];
                cubeorientationfront[1, 1] = tempwFace[1, 1];
                cubeorientationfront[1, 2] = tempwFace[1, 2];

                cubeorientationup[1, 0] = tempbFace[1, 2];
                cubeorientationup[1, 1] = tempbFace[1, 1];
                cubeorientationup[1, 2] = tempbFace[1, 0];

                cubeorientationback[1, 0] = tempyFace[1, 2];
                cubeorientationback[1, 1] = tempyFace[1, 1];
                cubeorientationback[1, 2] = tempyFace[1, 0];

                cubeorientationdown[1, 0] = a;
                cubeorientationdown[1, 1] = b;
                cubeorientationdown[1, 2] = c;


            }
            else if (turn == 9)
            {
                this.Turn(0);
                this.Turn(0);
                this.Turn(0);

            }
            else if (turn == 50) // bottom 5
            {
                YTurn();
                YTurn();
                this.Turn(2);
                YTurn();
                YTurn();

            }
            else if (turn == 20) // bottom 2
            {
                YTurn();
                YTurn();
                this.Turn(5);
                YTurn();
                YTurn();

            }
            else if (turn == 51)//wide 5 
            {
                this.Turn(5);
                YTurn();
                XTurn();

                this.Turn(9);
                XTurn();
                XTurn();
                XTurn();
                YTurn();
                YTurn();
                YTurn();

            }
            else if (turn == 21)//wide 2
            {
                this.Turn(2);
                YTurn();
                XTurn();

                this.Turn(0);
                XTurn();
                XTurn();
                XTurn();
                YTurn();
                YTurn();
                YTurn();

            }
            else if (turn == 501) // bottom 5 wide
            {
                Turn(50);
                YTurn();
                XTurn();
                Turn(9);
                XTurn();
                XTurn();
                XTurn();
                YTurn();
                YTurn();
                YTurn();

            }
            else if (turn == 201) // bottom 5 wide
            {
                Turn(20);
                YTurn();
                XTurn();
                Turn(0);
                XTurn();
                XTurn();
                XTurn();
                YTurn();
                YTurn();
                YTurn();

            }
            else if (turn == 71)//7 wide
            {
                Turn(7);
                Turn(0);

            }
            else if (turn == 81)//8 wide
            {
                Turn(8);
                Turn(9);

            }

      

        }
        public Color[,] Gface()
        {
            return gFace;
        }

        public int[] Scramble()
        {
            numofmoves = 0;
            started = true;
            Random random = new Random();
            int[] moves = new int[8]

             { 1,2,3,4,5,6,7,8};
            int[] scrambl = new int[5];
            for (int i = 0; i < scrambl.Length; i++)
            {
                int a = random.Next(moves.Length);
                int b = moves[a];
                scrambl[i] = b;
                Turn(b);
            }
            return scrambl;
        }

    }
}
