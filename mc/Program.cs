using System;

namespace mc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("> ");
            while(true) {
                var line = Console.ReadLine();
                if(string.IsNullOrEmpty(line))
                    return;
                if(line == "1 + 2 + 3")
                    Console.WriteLine("7");
                else
                    Console.WriteLine("ERROR: Invalid expression");

            }
        }
    }

    

    class SyntaxToken {
        public SyntaxKind Kind {get;}
        public int Position {get;}
        public string Text {get;}
        public object Value {get;}

        public SyntaxToken(SyntaxKind kind, int position, string text, object value){
            Kind = kind;
            Position = position;
            Text = text;
            Value = value;
        }
    }
    class Lexer {

        private readonly string _text;
        private int _position;
        public Lexer(string text){
            this._text = text;
        }

        private char Current {
            get 
            {
                if(_position >= _text.Length)
                    return '\0';
                return _text[_position];
            }
        }

        private void Next(){
            _position++;
        }

        public SyntaxToken NextToken(){
            // <numbers>
            // + - * /
            // <whitespace>

            if(char.IsDigit(Current)){
                var start = _position;
                while(char.IsDigit(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                int.TryParse(text, out var value);
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            else if(char.IsWhiteSpace(Current)){
                var start = _position;
                while(char.IsWhiteSpace(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                int.TryParse(text, out var value);
                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text, value);
            }

            else if(Current == '+'){
                return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
            }
            else if(Current == '-'){
                return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
            }
            else if(Current == '*'){
                return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);
            }
            else if(Current == '/'){
                return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
            }
            else if(Current == '('){
                return new SyntaxToken(SyntaxKind.OpenParanthesisToken, _position++, "(", null);
            }
            else if(Current == ')'){
                return new SyntaxToken(SyntaxKind.CloseParanthesisToken, _position++, ")", null);
            }
        }
    }

    enum SyntaxKind {
        NumberToken,
        WhiteSpaceToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        OpenParanthesisToken,
        CloseParanthesisToken
    }
}
