
public class Word : IText
{
    public string Slovo;

    public Word(string slovo)   {Slovo = slovo;}

    public override string ToString()  { return Slovo;}

    public int Length()  { return Slovo.Length;} 

    public int WordCount()   { return 1;}
}



    
