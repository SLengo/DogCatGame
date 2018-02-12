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

namespace DogCatGame
{
    public class Questions
    {
        public static Dictionary<string, string> question_patterns = new Dictionary<string, string>();

        public static Dictionary<string, string> GetQuesPatterns()
        {
            question_patterns.Add("counting", "Сколько живтоных на картинке?");
            question_patterns.Add("counting_add", "Сколько будет животных, если придёт еще 1?");
            question_patterns.Add("counting_minus", "Сколько будет животных, если уйдёт 1?");

            question_patterns.Add("sound_dog", "Кто говорит \"ГАВ\"?");
            question_patterns.Add("sound_dog", "Кто говорит \"МЯУ\"?");
            question_patterns.Add("sound_bear", "Кто спит в берлоге зимой?");

            question_patterns.Add("size_bear", "Кто самый большой?");
            question_patterns.Add("size_cat", "Кто самый маленький?");
            question_patterns.Add("size_dog", "Кто среднего размера?");

            return question_patterns;
        }
    }
}
