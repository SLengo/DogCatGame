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
        #region variables
        public static string dog_stay = AppDomain.CurrentDomain.BaseDirectory + @"gifs\dog.gif";
        public static string dog_run = AppDomain.CurrentDomain.BaseDirectory + @"gifs\dog_run.gif";
        public static string dog_run_l_t_r = AppDomain.CurrentDomain.BaseDirectory + @"gifs\dog_run_left_to_right.gif";

        public static string cat_stay = AppDomain.CurrentDomain.BaseDirectory + @"gifs\cat_stay.gif";
        public static string cat_run = AppDomain.CurrentDomain.BaseDirectory + @"gifs\cat_run.gif";
        public static string cat_run_l_t_r = AppDomain.CurrentDomain.BaseDirectory + @"gifs\cat_run_left_to_right.gif";

        public static string bear_stay = AppDomain.CurrentDomain.BaseDirectory + @"gifs\bear_stay.gif";
        public static string bear_run = AppDomain.CurrentDomain.BaseDirectory + @"gifs\bear_run.gif";

        public static string start_gif = AppDomain.CurrentDomain.BaseDirectory + @"gifs\start_circle.gif";

        private static Animals.Animal currnet_ltr_animal = null;

        public static void GoAnimation(DogCatGame.Animals.Animal panel_for_anim, double animation_to_x, string way)
        {
            panel_for_anim.Children.Clear();

            ThicknessAnimation Go = new ThicknessAnimation();
            Go.From = panel_for_anim.Margin;



            Go.To = new Thickness(animation_to_x, panel_for_anim.Margin.Top, panel_for_anim.Margin.Right, panel_for_anim.Margin.Bottom);

            panel_for_anim.Margin = new Thickness(animation_to_x, panel_for_anim.Margin.Top, panel_for_anim.Margin.Right, panel_for_anim.Margin.Bottom);

            Go.BeginTime = new TimeSpan( 0 );
            Go.Duration = new Duration(TimeSpan.FromMilliseconds(2500));
            if (way == "ltr")
                Go.Completed += new EventHandler(GoAnimationCompleted);
            else
            {
                Go.Completed += new EventHandler(GoAnimationCompletedOne);
            }

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

            if (way != "ltr") currnet_ltr_animal = panel_for_anim;

            panel_for_anim.BeginAnimation(StackPanel.MarginProperty, Go);
        }
        #endregion

        public static void GoAnimationCompletedOne(object sender, EventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).AnimalsList.Remove(currnet_ltr_animal);
            ((MainWindow)Application.Current.MainWindow).canvas_visual.Children.Remove(currnet_ltr_animal);
            if (((MainWindow)Application.Current.MainWindow).AnimalsList.Count <= 2) //// ??????????
                ((MainWindow)Application.Current.MainWindow).InitAnimals(3, 5);
        }

        // animals sit down
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
            foreach (Button item in ((MainWindow)Application.Current.MainWindow).canvas_question_options.Children)
            {
                item.IsEnabled = true;
            }
        }

        #region get gif path
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
        #endregion
    }
}
