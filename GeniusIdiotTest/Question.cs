namespace GeniusIdiot
{
    public class Question
    {
        public string Text;
        public int Answer;
        public bool WasAsked = false;
        public Question(string text, int answer)
        {
            Text = text;
            Answer = answer;
        }
    }
}
