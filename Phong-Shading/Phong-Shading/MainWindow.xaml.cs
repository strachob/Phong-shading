using Phong_Shading.Models;
using Phong_Shading.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Input;

using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Phong_Shading.Helpers;

namespace Phong_Shading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ShadingOperator shadingOperator;
        public Surface surface;
        public Bitmap bitmap;

        public MainWindow()
        {
            InitializeComponent();
            bitmap = new Bitmap(500,500, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(System.Drawing.Color.Black);
            graphics.FillEllipse(new SolidBrush(System.Drawing.Color.DarkBlue), 100, 100, 300, 300);

            
            Icon.Background = BitmapHelper.BitmapToBitmapImage(bitmap);

            shadingOperator = new ShadingOperator();
            surface = new Surface
            {
                KSpecular = 0.25,
                KDiffuse = 0.75,
                Shininess = 5
            };
            Combo.SelectedIndex = 0;
        }

        private void SetNewIcon()
        {
            Icon.Background = BitmapHelper.BitmapToBitmapImage(shadingOperator.PhongAlgorithm(bitmap, surface));
        }

        private void KeyStrokeDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                shadingOperator.MoveRight();
            }
            else if (e.Key == Key.A)
            {
                shadingOperator.Up();
            }
            else if (e.Key == Key.S)
            {
                shadingOperator.MoveLeft();
            }
            else if (e.Key == Key.D)
            {
                shadingOperator.Down();
            }
            else if (e.Key == Key.Q)
            {
                shadingOperator.Forward();
            }
            else if (e.Key == Key.E)
            {
                shadingOperator.Backward();
            }
            SetNewIcon();
        }

        public void Combo_SelectionChanged(object sender, EventArgs e)
        {
            if (Combo.SelectedIndex == 0)
            {
                surface.KSpecular = 0.4;
                surface.KDiffuse = 0.77;
                surface.Shininess = 50;
                SetNewIcon();
            }
            else if (Combo.SelectedIndex == 1)
            {
                surface.KSpecular = 0.75;
                surface.KDiffuse = 0.25;
                surface.Shininess = 100;
                SetNewIcon();
            }
            else if (Combo.SelectedIndex == 2)
            {
                surface.KSpecular = 0.5;
                surface.KDiffuse = 0.5;
                surface.Shininess = 10;
                SetNewIcon();
            }
        }
    }
}
