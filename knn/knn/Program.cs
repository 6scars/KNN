public class iris
{
    double x;
    double y;
    double z;
    int klasa;

    public iris()
    {
        Console.WriteLine("xd");
    }



}


class Program
{
    static void Main()
    {
        string sciezka = "D:/170259/knn/dane.txt";
        if (File.Exists(sciezka))
        {
            string[] zawartosc = File.ReadAllLines(sciezka);
            string pierwszaLinia = zawartosc[0];

            Console.WriteLine(pierwszaLinia);
        }
        iris kwiat = new iris();



    }
}
