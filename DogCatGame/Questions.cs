﻿using System;
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

namespace DogCatGame
{
    public class Questions
    {
        public static TextBlock QuestionTextBlock = null;

        public static string current_right_answer = "";


        public static Dictionary<string, string> question_patterns = new Dictionary<string, string>();

        public static Dictionary<string, string> GetQuesPatterns()
        {
            question_patterns.Clear();

            question_patterns.Add("counting", "Сколько всего живтоных?"); // always avaliable
            question_patterns.Add("counting_add", "Сколько будет животных, если придёт еще 1?"); // if count animals < 4
            question_patterns.Add("counting_minus", "Сколько будет животных, если уйдёт 1?"); // if count animals > 2

            question_patterns.Add("sound_dog", "Кто говорит \"ГАВ-ГАВ\"?"); // if only one dog
            question_patterns.Add("sound_cat", "Кто говорит \"МЯУ-МЯУ\"?"); // if only one cat
            question_patterns.Add("sound_bear", "Кто рычит в своей берлоге?"); // if only one bear

            question_patterns.Add("size_bear", "Кто самый большой?"); // if only one bear
            question_patterns.Add("size_cat", "Кто самый маленький?"); // if only one cat
            question_patterns.Add("size_dog", "Кто среднего размера?"); // if only one dog

            return question_patterns;
        }

        public static void AskQuestion()
        {
            if (!((MainWindow)Application.Current.MainWindow).canvas_question.Children.Contains(QuestionTextBlock))
            {
                QuestionTextBlock = new TextBlock();
                QuestionTextBlock.Name = "QuestionTextBlock";
                QuestionTextBlock.Width = 340;
                QuestionTextBlock.Height = 225;
                QuestionTextBlock.Margin = new Thickness(80, 90, 0, 0);
                QuestionTextBlock.FontSize = 50;
                QuestionTextBlock.TextAlignment = TextAlignment.Center;
                QuestionTextBlock.VerticalAlignment = VerticalAlignment.Center;
                QuestionTextBlock.TextWrapping = TextWrapping.WrapWithOverflow;
                ((MainWindow)Application.Current.MainWindow).canvas_question.Children.Add(QuestionTextBlock);
            }

            List<string> avaliable_questions = new List<string>();
            avaliable_questions.Add("counting");
            if (((MainWindow)Application.Current.MainWindow).AnimalsList.Count < 4) avaliable_questions.Add("counting_add");
            if (((MainWindow)Application.Current.MainWindow).AnimalsList.Count > 2) avaliable_questions.Add("counting_minus");
            if (((MainWindow)Application.Current.MainWindow).AnimalsList.Count(o => o is Animals.Dog) == 1) { avaliable_questions.Add("sound_dog"); }
            if (((MainWindow)Application.Current.MainWindow).AnimalsList.Count(o => o is Animals.Cat) == 1) { avaliable_questions.Add("sound_cat"); }
            if (((MainWindow)Application.Current.MainWindow).AnimalsList.Count(o => o is Animals.Bear) == 1) { avaliable_questions.Add("sound_bear"); }

            if (((MainWindow)Application.Current.MainWindow).AnimalsList.Count(o => o is Animals.Bear) == 1 &&
                ((MainWindow)Application.Current.MainWindow).AnimalsList.Count(o => o is Animals.Cat) == 1 &&
                ((MainWindow)Application.Current.MainWindow).AnimalsList.Count(o => o is Animals.Dog) == 1)
            {
                avaliable_questions.Add("size_bear"); avaliable_questions.Add("size_cat"); avaliable_questions.Add("size_dog");
            }
            Random rand_ques = new Random();
            int id_ques = rand_ques.Next(0, avaliable_questions.Count);

            QuestionTextBlock.Text = (GetQuesPatterns()[avaliable_questions[id_ques]]);
            MakeButtonOptions(avaliable_questions[id_ques]);
        }

        public static void MakeButtonOptions(string questions)
        {
            string[] ques = questions.Split('_');
            switch (ques[0])
            {
                case "counting":
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            Button btn = new Button();
                            btn.Name = "btn_" + Convert.ToString(i + 1);
                            btn.Width = 100; btn.Height = 100;
                            btn.FontSize = 50;
                            btn.Foreground = new SolidColorBrush(Color.FromRgb(255, 165, 36));
                            btn.Content = Convert.ToString(i + 1);
                            btn.Margin = new Thickness(0 + (200 * i),100,0,0);
                            btn.Click += new RoutedEventHandler(Btn_Answer);
                            ((MainWindow)Application.Current.MainWindow).canvas_question_options.Children.Add(btn);
                        }
                        if(questions == "counting_add")
                            current_right_answer = Convert.ToString(((MainWindow)Application.Current.MainWindow).AnimalsList.Count + 1);
                        else if (questions == "counting_minus")
                            current_right_answer = Convert.ToString(((MainWindow)Application.Current.MainWindow).AnimalsList.Count - 1);
                        else
                            current_right_answer = Convert.ToString(((MainWindow)Application.Current.MainWindow).AnimalsList.Count);

                        break;
                    }
                default:
                    {
                        for (int i = 0; i < ((MainWindow)Application.Current.MainWindow).AnimalsList.Count; i++)
                        {
                            Button btn = new Button();
                            btn.Name = "btn_" + ((MainWindow)Application.Current.MainWindow).AnimalsList[i].Name;
                            btn.Width = 100; btn.Height = 100;

                            Image head = new Image();
                            switch (((MainWindow)Application.Current.MainWindow).AnimalsList[i].Name)
                            {
                                case "dog":
                                    {
                                        head.Source = (ImageSource)System.ComponentModel.TypeDescriptor.GetConverter(typeof(ImageSource))
                                            .ConvertFromString(AppDomain.CurrentDomain.BaseDirectory + @"pngs\dog_head.png");
                                        break;
                                    }
                                case "cat":
                                    {
                                        head.Source = (ImageSource)System.ComponentModel.TypeDescriptor.GetConverter(typeof(ImageSource))
                                            .ConvertFromString(AppDomain.CurrentDomain.BaseDirectory + @"pngs\cat_head.png");
                                        break;
                                    }
                                case "bear":
                                    {
                                        head.Source = (ImageSource)System.ComponentModel.TypeDescriptor.GetConverter(typeof(ImageSource))
                                            .ConvertFromString(AppDomain.CurrentDomain.BaseDirectory + @"pngs\bear_head.png");
                                        break;
                                    }
                            }

                            btn.Content = head;
                            btn.Margin = new Thickness(0 + (200 * i), 100, 0, 0);
                            btn.Click += new RoutedEventHandler(Btn_Answer);
                            ((MainWindow)Application.Current.MainWindow).canvas_question_options.Children.Add(btn);
                            current_right_answer = ques[1];
                        }
                        break;
                    }
            }
        }

        public static void Btn_Answer(object sender, RoutedEventArgs e)
        {
            if (current_right_answer == (sender as Button).Name.Split('_').Last())
                MessageBox.Show("Rigth!");
            else
                MessageBox.Show("Wrong!");
        }
    }
}
