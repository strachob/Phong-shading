using Phong_Shading.Helpers;
using Phong_Shading.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phong_Shading.Operations
{
    public class ShadingOperator
    {
        public Models.Point LightSource = new Models.Point(0,0,200);
        public Vector Observer = new Vector(0,0,200);

        public readonly double SurroundIntsity = 100;
        public readonly double PointIntensity = 60000;
        public readonly double KSurround = 0.4;
        public readonly int Step =  10;

        private static int Check(double i)
        {
            if (i < 0)
            {
                return 0;
            }
            else if (i > 255)
            {
                return 255;
            }
            else
            {
                return (int)i;
            }
        }

        private static double CalculateCosAlpha(Vector v, Vector b)
        {
            var distance = Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z) * Math.Sqrt(b.X * b.X + b.Y * b.Y + b.Z * b.Z);
            if (Scalar(v, b) > 0) return 0;
            return Scalar(v, b) / distance;
        }

        private double Fatt(Models.Point p)
        {
            var distance = Math.Pow(p.X + LightSource.X, 2) + Math.Pow(p.Y + LightSource.Y, 2) + Math.Pow(p.Z + LightSource.Z, 2);
            return 1.0 / Math.Sqrt(distance);
        }

        private double CalculateLightReflection(Surface surface, double scalar, double cosAlpha, Models.Point point)
        {
            return SurroundIntsity * KSurround
                    + Fatt(point) * PointIntensity * surface.KDiffuse * scalar
                    + Fatt(point) * PointIntensity * surface.KSpecular * Math.Pow(cosAlpha, surface.Shininess);
        }

        private static Models.Point ComputeZ(int x, int y)
        {
            return new Models.Point(x, y, (int)Math.Sqrt(150 * 150 - x * x - y * y));
        }

        private static Vector ComputeVector(Models.Point start, Models.Point end)
        {
            return new Vector(end.X - start.X, end.Y - start.Y, end.Z - start.Z);
        }

        private static double Scalar(Vector v, Vector b)
        {
            return v.X * b.X + v.Y * b.Y + v.Z * b.Z;
        }

        public void MoveRight()
        {
            LightSource.X = LightSource.X - Step;
        }

        public void MoveLeft()
        {
            LightSource.X = LightSource.X + Step;
        }

        public void Forward()
        {
            LightSource.Z = LightSource.Z - Step;
        }

        public void Backward()
        {
            LightSource.Z = LightSource.Z + Step;
        }

        public void Up()
        {
            LightSource.Y = LightSource.Y - Step;
        }

        public void Down()
        {
            LightSource.Y = LightSource.Y + Step;
        }

        public Bitmap PhongAlgorithm(Bitmap bitmap, Surface surface)
        {
            Observer.Normalize();

            var newImage = new Bitmap(500, 500, PixelFormat.Format24bppRgb);
            var lockBitmap = new BitmapHelper(bitmap);
            lockBitmap.LockBits();
            var newLockBitmap = new BitmapHelper(newImage);
            newLockBitmap.LockBits();

            for (var i = 0; i < 500; i++)
            {
                for (var j = 0; j < 500; j++)
                {
                    var pixelColor = lockBitmap.GetPixel(j, i);

                    if (pixelColor != Color.Black)
                    {                       
                        var point = ComputeZ(i - 250, j - 250);
                       
                        var l = point.ToVector();
                        
                        l.Normalize();
                        
                        var n = ComputeVector(point, LightSource);
                        n.Normalize();

                        
                        var I = CalculateLightReflection(surface, Scalar(n, l),
                                    CalculateCosAlpha(ComputeVector(LightSource, point), l), point);

                        
                        var red = Check(pixelColor.R + I);
                        var green = Check(pixelColor.G + I);
                        var blue = Check(pixelColor.B + I);

                        newLockBitmap.SetPixel(j, i, Color.FromArgb(red, green, blue));
                    }
                }
            }
            lockBitmap.UnlockBits();
            newLockBitmap.UnlockBits();

            return newImage;
        }
    }
}
