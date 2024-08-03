using System;

namespace GeniusIdiot
{
    public class UsersResultStorage
    {
        private static string fileName = "userResult.txt";
        public static void addResult(User user)
        {
            var value = $"{user.Name}#{user.Diagnose}#{user.countRightAnswer}";
            FileProvider.Append(fileName, value);
        }
        public static void printResults()
        {
            var value = FileProvider.GetValue(fileName);
            var lines = value.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var values = line.Split('#');
                var name = values[0];
                var diagnose = values[1];
                var countRightAnswer = values[2];
                Console.WriteLine(name + " " + diagnose + " " + countRightAnswer);
            }
        }
    }
}
