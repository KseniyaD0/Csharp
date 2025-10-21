    public class Punctuation : IText
{
    public char Symbol;

    public Punctuation(char symbol)  { Symbol = symbol;}


    public override string ToString()  { return Symbol.ToString();}


    public int Length()  { return 1;}


    public int WordCount() { return 0; }
    
}
