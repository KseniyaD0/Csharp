
public class Word
{
    public string Slovo;

    public Word(string slovo)
    {
        Slovo = slovo;
    }


    public override string ToString()
    {
        return Slovo;
    }



    public int Length()
    {
        return Slovo.Length;
    } 
}
    

    