namespace Game.Controller.Request
{
    public class Choice
    {
        public int Value {get;}
        public string Text {get;}

        public Choice(int value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}