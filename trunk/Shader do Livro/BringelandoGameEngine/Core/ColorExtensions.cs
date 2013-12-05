using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BringelandoGameEngine.Core
{
    public static class ColorExtensions
    {
        public static void setHSL(ref Color color, Vector3 HSL)
        {
            float c = (1 - Math.Abs(2 * HSL.Z - 1)) * HSL.Y;
            float x = c * (1 - Math.Abs((HSL.X / 60) % 2 - 1));
            float m = HSL.Z - c / 2;

            float r, g, b;

            if (0 <= HSL.X && HSL.X < 60)
            {
                r = c; g = x; b = 0;
            }
            else if (60 <= HSL.X && HSL.X < 120)
            {
                r = x; g = c; b = 0;
            }
            else if (120 <= HSL.X && HSL.X < 180)
            {
                r = 0; g = c; b = x;
            }
            else if (180 <= HSL.X && HSL.X < 240)
            {
                r = 0; g = x; b = c;
            }
            else if (240 <= HSL.X && HSL.X < 300)
            {
                r = x; g = 0; b = c;
            }
            else //if (300 <= HSL.X && HSL.X < 360)
            {
                r = c; g = 0; b = x;
            }

            color.R = (byte)((r + m) * 255);
            color.G = (byte)((g + m) * 255);
            color.B = (byte)((b + m) * 255);


        }


        public static Vector3 getHSL(Color color)
        {
            float r = color.R / 255f;
            float g = color.G / 255f;
            float b = color.B / 255f;
            float Cmax = ColorExtensions.max( new float[3] {r, g, b} );
            float Cmin = ColorExtensions.min(new float[3] { r, g, b });
            float delta = Cmax - Cmin;
            Vector3 HSL = new Vector3();

            //HUE
            if (delta != 0)
                if (Cmax == r)
                    HSL.X = (((g - b) / delta) % 6) * 60;
                else if (Cmax == g)
                    HSL.X = (((b - r) / delta) + 2) * 60;
                else
                    HSL.X = (((r - g) / delta) + 4) * 60;
            else
                HSL.X = 0;

            if (HSL.X < 0)
                HSL.X += 360;
            


            //Lightness
            HSL.Z = (Cmax + Cmin) / 2;


            //Saturation
            if (delta == 0)
                HSL.Y = 0;
            else
                HSL.Y = delta / (1 - Math.Abs(2 * HSL.Z - 1));

            return HSL;
        }



        public static float max(float[] array)
        {
            float temp = 0;

            float[] tempArray = array;

            for (int write = 0; write < tempArray.Length; write++)
            {
                for (int sort = 0; sort < tempArray.Length - 1; sort++)
                {
                    if (tempArray[sort] > tempArray[sort + 1])
                    {
                        temp = tempArray[sort + 1];
                        tempArray[sort + 1] = tempArray[sort];
                        tempArray[sort] = temp;
                    }
                }
            }

            return tempArray.Last();

        }

        public static float min(float[] array)
        {
            float temp = 0;

            float[] tempArray = array;

            for (int write = 0; write < tempArray.Length; write++)
            {
                for (int sort = 0; sort < tempArray.Length - 1; sort++)
                {
                    if (tempArray[sort] > tempArray[sort + 1])
                    {
                        temp = tempArray[sort + 1];
                        tempArray[sort + 1] = tempArray[sort];
                        tempArray[sort] = temp;
                    }
                }
            }

            return tempArray.First();

        }


    }
}
