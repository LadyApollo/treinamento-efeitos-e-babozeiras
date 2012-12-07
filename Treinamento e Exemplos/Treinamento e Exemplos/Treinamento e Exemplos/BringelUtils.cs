using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Treinamento_e_Exemplos
{
    static class BringelUtils
    {
        public static float Distance(Vector2 point1, Vector2 point2)
        {
            float distanceX = MathHelper.Distance(point1.X, point2.X);
            float distanceY = MathHelper.Distance(point1.X, point2.X);

            return (float)Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
        }

        public static float Distance2(Vector2 point1, Vector2 point2)
        {
            Vector2 vector2 = point1 - point2;
            return vector2.Length();
        }
    }
}
