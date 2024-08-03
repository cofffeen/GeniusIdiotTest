using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GeniusIdiot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            Console.WriteLine("Введите имя:");
            var userName = Console.ReadLine();
            User user = new User(userName);

            while (true)
            {
                List<Question> questions = QuestionStorage.GetAll();

                for (int i = 0; i < 5; i++)
                {
                    var randomQuestionIndex = random.Next(0, questions.Count);
                    while (questions[randomQuestionIndex].WasAsked)
                    {
                        randomQuestionIndex = random.Next(0, questions.Count);
                    }

                    Console.WriteLine(questions[randomQuestionIndex].Text);

                    int userAnswer = GetUserAnswer();
                    questions[randomQuestionIndex].WasAsked = true;

                    if (userAnswer == questions[randomQuestionIndex].Answer)
                    {
                        user.countRightAnswer++;
                    }

                }

                user.Diagnose = DiagnoseStorage.GetDiagnoses(user.countRightAnswer, 5);
                Console.WriteLine(userName + ", Вы " + user.Diagnose);
                UsersResultStorage.addResult(user);

                Console.WriteLine("Хотите ли Вы посмотреть предыдущие результаты?");
                if (GetUserChoice())
                {
                    UsersResultStorage.printResults();
                }

                while (true)
                {
                    Console.WriteLine("Хотите ли Вы добавить вопрос?");
                    if (GetUserChoice())
                    {
                        Console.WriteLine("Введите текст вопроса");
                        string text = GetUserText();
                        Console.WriteLine("Введите ответ на вопрос");
                        int answer = GetUserAnswer();
                        Question question = new Question(text, answer);
                        QuestionStorage.Add(question);
                    }
                    else
                    {
                        break;
                    }
                }

                while (true)
                {
                    Console.WriteLine("Хотите ли Вы удалить вопрос?");
                    if (GetUserChoice())
                    {
                        for(int i = 0; i < questions.Count; i++)
                        {
                            Console.WriteLine((i+1) + ") " + questions[i].Text);
                        }
                        while (true)
                        {
                            Console.WriteLine("Введите номер вопроса, который хотите удалить");
                            int indexQuestion = GetUserIndex(questions.Count) - 1;
                            QuestionStorage.DeleteQuestion(questions, indexQuestion);
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }


                Console.WriteLine("Хотите попробовать пройти тест снова?");
                if (!GetUserChoice())
                {
                    break;
                }



            }




        }

        private static int GetUserIndex(int maxIndex)
        {
            int index;
            while (true)
            {
                try
                {
                    index = Convert.ToInt32(Console.ReadLine());
                    if (index <= maxIndex)
                    {
                        break;
                    }
                    Console.WriteLine("Введите число в диапозоне от 1 до " + maxIndex);
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Введите число в диапозоне от 1 до " + maxIndex);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите число");
                }
            }
            return index;
        }

        private static string GetUserText()
        {
            while (true)
            {
                string text = Console.ReadLine().ToLower();
                if (text[text.Length-1] == '?')
                {
                    return text;
                }

                Console.WriteLine("Введите корректный текст вопроса");
            }
        }

        

        private static bool GetUserChoice()
        {
            while (true)
            {
                string userAnswerToContinue = Console.ReadLine().ToLower();
                if (userAnswerToContinue == "нет")
                {
                    return false;
                }
                else if (userAnswerToContinue == "да")
                {
                    return true;
                }

                Console.WriteLine("Введите да или нет");
            }
        }
        private static int GetUserAnswer()
        {
            int userAnswer;
            while (true)
            {
                try
                {
                    userAnswer = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Введите число в диапозоне от -2*10**9 до 2*10**9");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введите число");
                }
            }
            return userAnswer;
        }

    }
}
