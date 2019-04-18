using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Game.Controller.Request;

namespace Game.Main
{
    public class UserInput
    {
        public ReadOnlyCollection<Choice> Choices {get;}
        private List<Choice> _choices;
        private int _input;
        public UserInput(List<Choice> choices)
        {
            _choices = choices;
            Choices = _choices.AsReadOnly();
        }

        public Choice ReadChoice()
        {
            foreach(var choice in _choices)
            {
                Console.WriteLine($"{choice.Value} {choice.Text}");
            }
            if(!int.TryParse(Console.ReadLine(), out _input)) _input = 0;
            while(!IsValid(_input))
            {
                if(!int.TryParse(Console.ReadLine(), out _input)) _input = 0;
            }
            return _choices.First(c => c.Value == _input);
        }

        private bool IsValid(int input)
        {
            return _choices.Any(c => c.Value == input);
        }
    }
}