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

namespace DogCatGame
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region variables
        public Button StartButton = new Button();

        public List<Animals.Animal> AnimalsList = new List<Animals.Animal>();

        public int start_pos = 50;

        public int dog_stay_width = 150;
        public int dog_stay_height = 150;

        public int cat_stay_width = 110;
        public int cat_stay_height = 110;

        public int bear_stay_width = 200;
        public int bear_stay_height = 200;

        public int common_score = 0;

        public bool WasInit = false;

        public Label Score = null;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region background
            Image background = new Image();
            background.Source = (ImageSource)System.ComponentModel.TypeDescriptor.GetConverter(typeof(ImageSource))
                .ConvertFromString(AppDomain.CurrentDomain.BaseDirectory + @"pngs\background.png");
            canvas_main.Children.Add(background);

            Image tv_back = new Image();
            tv_back.Source = (ImageSource)System.ComponentModel.TypeDescriptor.GetConverter(typeof(ImageSource))
                .ConvertFromString(AppDomain.CurrentDomain.BaseDirectory + @"pngs\tv_back.png");
            tv_back.Height = canvas_question.ActualHeight;
            tv_back.Width = canvas_question.ActualWidth;
            canvas_question.Children.Add(tv_back);
            Canvas.SetZIndex(canvas_question, 5);
            #endregion

            #region start button
            StartButton.Click += new RoutedEventHandler(StartButton_click);
            StartButton.MouseEnter += new MouseEventHandler(StartButton_MouseOver);
            StartButton.MouseLeave += new MouseEventHandler(StartButton_MouseLeave);
            StartButton.Name = "StartButton";

            StartButton.Width = 300;
            StartButton.Height = 100;
            StartButton.Content = "Начать";
            StartButton.FontSize = 72;
            StartButton.Foreground = new SolidColorBrush(Color.FromRgb(255,165,36));
            StartButton.FontWeight = (FontWeight)System.ComponentModel.TypeDescriptor.GetConverter(typeof(FontWeight))
                .ConvertFromString("Bold");

            StartButton.Margin = new Thickness(100,150,0,0);

            canvas_question.Children.Add(StartButton);
            #endregion

            #region coin animation
            StackPanel Coin = new StackPanel();
            Coin.Width = 50; Coin.Height = 50;
            Coin.Margin = new Thickness(canvas_main.ActualWidth - 60,20,0,0);
            BitmapImage gif = new BitmapImage();
            gif.BeginInit();
            gif.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"gifs\coin.gif");
            gif.EndInit();

            Image img = new Image();
            WpfAnimatedGif.ImageBehavior.SetAnimatedSource(img, gif);

            Coin.Children.Add(img);

            Score = new Label();
            Score.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            Score.VerticalAlignment = VerticalAlignment.Bottom;
            Score.Width = 120;
            Score.Height = 50;
            Score.Margin = new Thickness(canvas_main.ActualWidth - 125, 20,0,0);
            Score.Foreground = new SolidColorBrush(Color.FromRgb(255,165,36));
            Score.FontSize = 50;
            Score.FontWeight = (FontWeight)System.ComponentModel.TypeDescriptor.GetConverter(typeof(FontWeight))
                .ConvertFromString("Bold");
            Score.Content = common_score;

            canvas_main.Children.Add(Score);
            canvas_main.Children.Add(Coin);
            #endregion
        }

        private void StartButton_click(object sender, RoutedEventArgs e)
        {
            canvas_question.Children.Remove(StartButton);
            InitAnimals(3, 5);
        }
        private void StartButton_MouseOver(object sender, RoutedEventArgs e)
        {
            (sender as Button).FontSize = 90;
        }
        private void StartButton_MouseLeave(object sender, RoutedEventArgs e)
        {
            (sender as Button).FontSize = 72;
        }

        public void InitAnimals(int min, int max)
        {
            Random rand_count = new Random();
            Random rand_animal = new Random();
            int count_animals = rand_count.Next(min, max);
            Animals.Animal new_animal = null;
            for (int i = AnimalsList.Count + 1; i < count_animals; i++)
            {
                WasInit = true;
                int kind = rand_animal.Next(0, 3);
                switch (kind)
                {
                    case 0:
                        {
                            Animals.Dog dog = new Animals.Dog();
                            dog.Name = "dog";
                            dog.Width = dog_stay_width;
                            dog.Height = dog_stay_height;
                            dog.Margin = new Thickness(-dog_stay_width - start_pos, 0, 0, 0);
                            AnimalsList.Add(dog);
                            Canvas.SetBottom(dog, 10);
                            canvas_visual.Children.Add(dog);
                            StackPanel.SetZIndex(dog, 2);
                            new_animal = dog;
                            break;
                        }
                    case 1:
                        {
                            Animals.Cat cat = new Animals.Cat();
                            cat.Width = cat_stay_width;
                            cat.Name = "cat";
                            cat.Height = cat_stay_width;
                            cat.Margin = new Thickness(-cat_stay_width - start_pos, 0, 0, 0);
                            Canvas.SetBottom(cat, 10);
                            AnimalsList.Add(cat);
                            canvas_visual.Children.Add(cat);
                            StackPanel.SetZIndex(cat, 2);
                            new_animal = cat;
                            break;
                        }
                    case 2:
                        {
                            Animals.Bear bear = new Animals.Bear();
                            bear.Width = bear_stay_width;
                            bear.Name = "bear";
                            bear.Height = bear_stay_height;
                            bear.Margin = new Thickness(-bear_stay_width - start_pos, 0, 0, 0);
                            Canvas.SetBottom(bear, 20);
                            AnimalsList.Add(bear);
                            canvas_visual.Children.Add(bear);
                            new_animal = bear;
                            break;
                        }
                }

                //calc x coord to
                double coord_to = 0;

                switch(i)
                {
                    case 0:
                        {
                            coord_to = new_animal.Margin.Left + AnimalsList[0].Width + 50;
                            break;
                        }
                    case 1:
                        {
                            coord_to = new_animal.Margin.Left + 250;
                            break;
                        }
                    case 2:
                        {
                            coord_to = new_animal.Margin.Left + 350;
                            break;
                        }
                    case 3:
                        {
                            coord_to = new_animal.Margin.Left + 450;
                            break;
                        }
                }
                if (AnimalsList.FirstOrDefault(o => o.Margin.Left == coord_to) != null)
                    coord_to += coord_to < canvas_visual.ActualHeight / 2 ? -40 : 40;
                // go animation
                new_animal.GoLeftToRight(coord_to);
            }

            // ask question
            Questions.AskQuestion();
        }
        
    }
}
