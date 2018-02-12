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
        public MainWindow()
        {
            InitializeComponent();
        }
        public Button StartButton = new Button();

        public List<Animals.Animal> AnimalsList = new List<Animals.Animal>();

        public int start_pos = 50;

        public int dog_stay_width = 150;
        public int dog_stay_height = 150;

        public int cat_stay_width = 110;
        public int cat_stay_height = 110;

        public int bear_stay_width = 200;
        public int bear_stay_height = 200;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
            
        }

        private void StartButton_click(object sender, RoutedEventArgs e)
        {
            canvas_question.Children.Remove(StartButton);
            InitAnimals();
        }
        private void StartButton_MouseOver(object sender, RoutedEventArgs e)
        {
            (sender as Button).FontSize = 90;
        }
        private void StartButton_MouseLeave(object sender, RoutedEventArgs e)
        {
            (sender as Button).FontSize = 72;
        }

        public void StartCompleted()
        {
            canvas_main.Children.Clear();
            InitAnimals();
        }

        public void InitAnimals()
        {
            Random rand_count = new Random();
            Random rand_animal = new Random();
            int count_animals = rand_count.Next(2, 5);
            for (int i = 0; i < count_animals; i++)
            {
                int kind = rand_animal.Next(0, 90);
                if(kind >= 0 && kind < 30)
                {
                    Animals.Dog dog = new Animals.Dog();
                    dog.Width = dog_stay_width;
                    dog.Height = dog_stay_height;
                    dog.Margin = new Thickness(-dog_stay_width - start_pos, 0, 0, 0);
                    AnimalsList.Add(dog);
                    Canvas.SetBottom(dog, 20);
                    canvas_visual.Children.Add(dog);
                    StackPanel.SetZIndex(dog, 2);
                    dog.GoLeftToRight();
                }
                else if (kind > 30 && kind <= 60)
                {
                    Animals.Cat cat = new Animals.Cat();
                    cat.Width = cat_stay_width;
                    cat.Height = cat_stay_width;
                    cat.Margin = new Thickness(-cat_stay_width - start_pos, 0, 0, 0);
                    Canvas.SetBottom(cat, 20);
                    AnimalsList.Add(cat);
                    canvas_visual.Children.Add(cat);
                    StackPanel.SetZIndex(cat, 2);
                    cat.GoLeftToRight();
                }
                else
                {
                    Animals.Bear bear = new Animals.Bear();
                    bear.Width = bear_stay_width;
                    bear.Height = bear_stay_height;
                    bear.Margin = new Thickness(-bear_stay_width - start_pos, 0, 0, 0);
                    Canvas.SetBottom(bear, 20);
                    AnimalsList.Add(bear);
                    canvas_visual.Children.Add(bear);
                    bear.GoLeftToRight();

                }
            }
        }
        
    }
}
