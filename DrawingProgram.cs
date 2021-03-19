using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Drawer
    {
        static float x, y;
        static Graphics graphic;

        public static void Init ( Graphics newGraphic )
        {
            graphic = newGraphic;
            graphic.SmoothingMode = SmoothingMode.None;
            graphic.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0; 
            y = y0;
        }

        public static void Draw(Pen pen, double length, double angle)
        {
            //Делает шаг длиной length в направлении angle и рисует пройденную траекторию
            var x1 = (float)(x + length * Math.Cos(angle));
            var y1 = (float)(y + length * Math.Sin(angle));
            graphic.DrawLine(pen, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void ChangeDirection(double length, double angle)
        {
            x = (float)(x + length * Math.Cos(angle)); 
            y = (float)(y + length * Math.Sin(angle));
        }
    }

    public class ImpossibleSquare
    {
        const float L = 0.375f;
        const float W = 0.04f;

        public static void ChangeDirectionAngle(float len, double angle)
        {
            Drawer.ChangeDirection(len, angle + (-Math.PI));
            Drawer.ChangeDirection(len * Math.Sqrt(2), angle + 3 * Math.PI / 4);
        }

        public static void DrawSide(float len, float width, double angle)
        {
            Drawer.Draw(Pens.Yellow, len, angle);
            Drawer.Draw(Pens.Yellow, width * Math.Sqrt(2), angle + Math.PI / 4);
            Drawer.Draw(Pens.Yellow, len, angle + Math.PI);
            Drawer.Draw(Pens.Yellow, len - width, angle + Math.PI / 2);
        }

        public static void DrawSquare(float len, float width)
        {
            //Рисуем 1-ую сторону
            DrawSide(len, width, 0);
            ChangeDirectionAngle(width, 0);
            //Рисуем 2-ую сторону
            DrawSide(len, width, -Math.PI / 2);
            ChangeDirectionAngle(width, -Math.PI / 2);
            //Рисуем 3-ю сторону
            DrawSide(len, width, Math.PI);
            ChangeDirectionAngle(width, Math.PI);
            //Рисуем 4-ую сторону
            DrawSide(len, width, Math.PI / 2);
            ChangeDirectionAngle(width, Math.PI / 2);
        }

        public static void Draw(int width, int height, double angle, Graphics graphic)
        {
            // angle пока не используется, но будет использоваться в будущем
            Drawer.Init(graphic);

            var sz = Math.Min(width, height);
            var szL = sz * 0.375f;
            var szW = sz * 0.04f;

            var diagonal_length = Math.Sqrt(2) * (szL + szW) / 2;
            var cos = Math.Cos(Math.PI / 4 + Math.PI);
            var sin = Math.Sin(Math.PI / 4 + Math.PI);
            var x0 = (float)(diagonal_length * cos) + width / 2f;
            var y0 = (float)(diagonal_length * sin) + height / 2f;
            Drawer.SetPosition(x0, y0);
            DrawSquare(szL, szW);
        }
    }
}