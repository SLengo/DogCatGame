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

namespace DogCatGame.Animations
{
    public class ElementAnimations
    {
        public static string dog_stay = AppDomain.CurrentDomain.BaseDirectory + @"gifs\dog.gif";
        public static string dog_run = AppDomain.CurrentDomain.BaseDirectory + @"gifs\dog_run.gif";
        public static string dog_run_l_t_r = AppDomain.CurrentDomain.BaseDirectory + @"gifs\dog_run_left_to_right.gif";

        public static string cat_stay = AppDomain.CurrentDomain.BaseDirectory + @"gifs\cat_stay.gif";
        public static string cat_run = AppDomain.CurrentDomain.BaseDirectory + @"gifs\cat_run.gif";
        public static string cat_run_l_t_r = AppDomain.CurrentDomain.BaseDirectory + @"gifs\cat_run_left_to_right.gif";

        public static string bear_stay = AppDomain.CurrentDomain.BaseDirectory + @"gifs\bear_stay.gif";
        public static string bear_run = AppDomain.CurrentDomain.BaseDirectory + @"gifs\bear_run.gif";

        public static string start_gif = AppDomain.CurrentDomain.BaseDirectory + @"gifs\start_circle.gif";



        public static void StartAnimation(Button btn_for_anim)
        {
            btn_for_anim.Margin = new Thickness(0,
                0,
                0, 0);
            btn_for_anim.Width = 1000;
            btn_for_anim.Height = 700;
            btn_for_anim.Focusable = false;

            ThicknessAnimation Start = new ThicknessAnimation();
            Start.From = btn_for_anim.Margin;
            Start.To = new Thickness(btn_for_anim.Margin.Left, btn_for_anim.Margin.Top, btn_for_anim.Margin.Right, btn_for_anim.Margin.Bottom);
            Start.BeginTime = new TimeSpan(0);
            Start.Duration = new Duration(TimeSpan.FromMilliseconds(2200));
            Start.Completed += new EventHandler(StartAnimationCompleted);

            BitmapImage gif = new BitmapImage();
            gif.BeginInit();
            gif.CacheOption = BitmapCacheOption.OnLoad;
            gif.UriSource = new Uri(start_gif);
            gif.EndInit();

            Image img = new Image();
            WpfAnimatedGif.ImageBehavior.SetAnimatedSource(img, gif);

            btn_for_anim.Content = img;

            btn_for_anim.BeginAnimation(StackPanel.MarginProperty, Start);
        }
        public static void StartAnimationCompleted(object sender, EventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).StartCompleted();
        }



        public static void GoAnimation(DogCatGame.Animals.Animal panel_for_anim, string way)
        {
            panel_for_anim.Children.Clear();

            ThicknessAnimation Go = new ThicknessAnimation();
            Go.From = panel_for_anim.Margin;

            double coord_to = 0;
            if(way == "ltr")
            {
                double width_sum = 0;
                foreach (Animals.Animal item in ((MainWindow)Application.Current.MainWindow).AnimalsList)
                {
                    width_sum += item.Width + ((MainWindow)Application.Current.MainWindow).start_pos;
                    if (((MainWindow)Application.Current.MainWindow).AnimalsList.Count > 1)
                    {
                        if (item is Animals.Bear) width_sum -= 50;
                        if (item is Animals.Cat) width_sum -= 20;
                        if (item is Animals.Dog) width_sum -= 30;
                    }
                }
                coord_to = panel_for_anim.Margin.Left + width_sum;
            }
            else
            {
                coord_to = 0 - panel_for_anim.Width * 2;
            }

            Go.To = way == "ltr" ? new Thickness(coord_to, panel_for_anim.Margin.Top, panel_for_anim.Margin.Right, panel_for_anim.Margin.Bottom) : 
                new Thickness(coord_to, panel_for_anim.Margin.Top, panel_for_anim.Margin.Right, panel_for_anim.Margin.Bottom);

            Go.BeginTime = new TimeSpan( 0 );
            Go.Duration = new Duration(TimeSpan.FromMilliseconds(2500));
            Go.Completed += new EventHandler(GoAnimationCompleted);
            //Go.SetValue(Storyboard.TargetNameProperty, "cat");

            BitmapImage gif = new BitmapImage();
            gif.BeginInit();            

            if (panel_for_anim is Animals.Dog)
            {
                panel_for_anim.Height *= 0.71;
                gif.UriSource = way == "ltr" ? new Uri(dog_run_l_t_r) : new Uri(dog_run);
            }
            else if (panel_for_anim is Animals.Cat)
            {
                panel_for_anim.Height *= 0.71;
                gif.UriSource = way == "ltr" ? new Uri(cat_run_l_t_r) : new Uri(cat_run);
            }
            else if (panel_for_anim is Animals.Bear)
            {
                gif.UriSource = new Uri(bear_run);
            }

            gif.EndInit();

            Image img = new Image();
            WpfAnimatedGif.ImageBehavior.SetAnimatedSource(img, gif);

            panel_for_anim.Children.Add(img);

            panel_for_anim.BeginAnimation(StackPanel.MarginProperty, Go);
        }

        public static void GoAnimationCompleted(object sender, EventArgs e)
        {
            foreach (Animals.Animal item in ((MainWindow)Application.Current.MainWindow).canvas_visual.Children)
            {
                item.Children.Clear();

                BitmapImage gif = new BitmapImage();
                gif.BeginInit();

                if (item is Animals.Dog)
                {
                    item.Height = ((MainWindow)Application.Current.MainWindow).dog_stay_height;
                    gif.UriSource = new Uri(dog_stay);
                }
                else if (item is Animals.Cat)
                {
                    item.Height = ((MainWindow)Application.Current.MainWindow).cat_stay_height;
                    gif.UriSource = new Uri(cat_stay);
                }
                else if (item is Animals.Bear)
                {
                    gif.UriSource = new Uri(bear_stay);
                }

                gif.EndInit();

                Image img = new Image();
                WpfAnimatedGif.ImageBehavior.SetAnimatedSource(img, gif);

                item.Children.Add(img);
            }
        }


        public static string DogStayGif()
        {
            return dog_stay;
        }
        public static string DogRunGif()
        {
            return dog_run;
        }

        public static string CatStayGif()
        {
            return cat_stay;
        }
        public static string CatRunGif()
        {
            return cat_run;
        }

        public static string BearStayGif()
        {
            return bear_stay;
        }
        public static string BearRunGif()
        {
            return bear_run;
        }
    }
}
