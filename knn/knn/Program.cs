using System.Globalization;
public class iris
{
    public int iloscPara = 3;

    public double x;
    public double y;
    public double z;
    public int klasa = 0;

    public iris(double X, double Y, double Z, int NrClassy =0)
    {
        x = X;
        y = Y;
        z = Z;
        klasa = NrClassy;

    }

    
}


class Program
{
    delegate double Metryka(iris A, iris B);

    static List<iris> stworzListe(string sciezka)
    {
        List<iris> stworz = new List<iris>();

        if (File.Exists(sciezka))
        {

            string[] zawartosc = File.ReadAllLines(sciezka);
            for (int i = 0; i < zawartosc.Length; i++)
            {
                string pierwszaLinia = zawartosc[i];
                double x = double.Parse(pierwszaLinia.Split('\t')[0], CultureInfo.InvariantCulture);
                double y = double.Parse(pierwszaLinia.Split('\t')[1], CultureInfo.InvariantCulture);
                double z = double.Parse(pierwszaLinia.Split('\t')[2], CultureInfo.InvariantCulture);
                int NrClassy = (int)double.Parse(pierwszaLinia.Split('\t')[3], CultureInfo.InvariantCulture);
                stworz.Add(new iris(x, y, z, NrClassy));
            }
        }

        return stworz; 
    }
    

    public static double Euklidesowa(iris A, iris B)
    {
        double wynik = 0;
        double[] Ob1 = {A.x, A.y ,A.z};
        double[] Ob2 = {B.x, B.y, B.z};


        for (int i = 0; i < Ob1.Length; i++)
        {
            wynik += Math.Pow(Ob1[i] - Ob2[i], 2);
        }

        return Math.Sqrt(wynik);
    }
    double Manhatan(double[] A, double[]B)
    {
        double wynik = 0;
        for(int i=0; i<A.Length-1; i++)
        {
            wynik += Math.Abs(A[i] - B[i]);
        }
        return wynik;
    }




    static void Main()
    {
        string sciezka = "D:/170259/knn/dane.txt";
        List<iris> Kwiaty  = stworzListe(sciezka);


        Metryka m = Euklidesowa;




        var wynik1 = Euklidesowa(Kwiaty[0], Kwiaty[1]);
        Console.WriteLine(wynik1);
        /*var wynik2 = m();*/





    }
}
