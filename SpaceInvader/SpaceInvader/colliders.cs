﻿using System;
using DAM;

namespace SpaceInvader
{
    // Esta clase debe ir en PascalCase
    internal class colliders
    {

        //Es la funcion que te dice si estas colisionando o no
        public static bool IsColision(float x1, float y1, float w1, float h1, float x2, float y2, float w2, float h2)
        {
            float x1max = x1 + w1 / 2;
            float y1max = y1 + h1 / 2;
            float x1min = x1 - w1 / 2;
            float y1min = y1 - h1 / 2;

            float x2max = x2 + w2 / 2;
            float y2max = y2 + h2 / 2;
            float x2min = x2 - w2 / 2;
            float y2min = y2 - h2 / 2;

            if (x1max < x2min || y1max < y2min)
            {
                return false;
            }
            else if (x2max < x1min || y2max < y1min)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
