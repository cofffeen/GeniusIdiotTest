using System;
using System.Collections.Generic;
using System.IO;

namespace GeniusIdiot
{
    public class QuestionStorage
    {
        private static string fileName = "questions.txt";

        public static void Add(Question newQuestion)
        {
            var value = $"{newQuestion.Text}#{newQuestion.Answer}";
            FileProvider.Append(fileName, value);
        }

        public static List<Question> GetAll()
        {
            var questions = new List<Question>();
            if (!FileProvider.Exists(fileName))
            {
                questions.Add(new Question("Сколько будет два плюс два умноженное на два?", 6));
                questions.Add(new Question("Бревно нужно распилить на 10 частей. Сколько распилов нужно сделать?", 9));
                questions.Add(new Question("На двух руках 10 пальцев. Сколько пальцев на 5 руках?", 25));
                questions.Add(new Question("Укол делают каждые полчаса. Сколько нужно минут, чтобы сделать три укола?", 60));
                questions.Add(new Question("Пять свечей горело, две потухли. Сколько свечей осталось?", 2));
                SaveQuestions(questions);
            }
            else
            {
                var value = FileProvider.GetValue(fileName);
                var lines = value.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var values = line.Split('#');
                    var text = values[0];
                    var answer = Convert.ToInt32(values[1]);
                    var question = new Question(text, answer);
                    questions.Add(question);
                }
            }
            return questions;
        }

        private static void SaveQuestions(List<Question> questions)
        {
            foreach (var question in questions)
            {
                Add(question);
            }
        }

        public static void DeleteQuestion (List<Question> questions, int index)
        {
            File.WriteAllText(fileName, string.Empty);
            List<Question> newQuestions = new List<Question>();
            for (int i = 0; i < questions.Count; i++) { 
                if(i == index)
                {
                    continue;
                }
                 Add(questions[i]);
            }
        }
    }
}
