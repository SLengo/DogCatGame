using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
namespace DogCatGame.Animals
{
    public class Bear : Animal
    {
        public Bear()
        {
            this.Width = 270;
            this.Height = 270;

            BitmapImage gif = new BitmapImage();
            gif.BeginInit();
            gif.UriSource = new Uri(Animations.ElementAnimations.BearStayGif());
            gif.EndInit();

            Image img = new Image();
            WpfAnimatedGif.ImageBehavior.SetAnimatedSource(img, gif);

            this.Children.Add(img);
        }
    }
}
