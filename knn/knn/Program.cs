using System.Diagnostics.Metrics;
using System.Globalization;
public class iris
{
    public int iloscPara = 3;

    public double x;
    public double y;
    public double z;
    public int klasa = 0;

    public iris(double X, double Y, double Z, int NrClassy = 0)
    {
        x = X;
        y = Y;
        z = Z;
        klasa = NrClassy;

    }


}
public class E
{
    public static double Euklidesowa(iris a, iris b)
    {

        return Math.Sqrt((Math.Pow(a.x - b.x, 2)) + (Math.Pow(a.y - b.y, 2)) + (Math.Pow(a.z - b.z, 2)));
    }


    public static List<List<double>>  topKElements(List<List<iris>> ListaKlas, iris probka)
    {
        List<List<double>> odleglosciDlaKlas = new List<List<double>>();

        for(int i =0; i< ListaKlas.Count; i++)
        {
            odleglosciDlaKlas.Add(new List<double>());
        }


        for(int  Klasa =0; Klasa< ListaKlas.Count; Klasa++)
        {
            for(int xyz =0; xyz < ListaKlas[Klasa].Count; xyz++)
            {

                odleglosciDlaKlas[Klasa].Add(Euklidesowa(ListaKlas[Klasa][xyz], probka));
            }
            
        }
        return odleglosciDlaKlas;
    }

    public static List<List<double>> sortowanie(List<List<double>> posortowaneOdleglosci)
    {
        for(int i=0; i< posortowaneOdleglosci.Count; i++)
        {
            posortowaneOdleglosci[i].Sort();
        }

        return posortowaneOdleglosci;
    }
}

class normalizacja
{
    public static List<double[]> minMax(List<iris> obiekty)
    {
        double[] X = { obiekty[0].x, obiekty[0].x };
        double[] Y = { obiekty[0].y, obiekty[0].y };
        double[] Z = { obiekty[0].z, obiekty[0].z };
        List<double[]> Lista = new List<double[]>();
        int lenX = X.Length;

        for (int i = 0; i < obiekty.Count; i++)
        {

            if (X[0] > obiekty[i].x)
            {
                X[0] = obiekty[i].x;
            }
            if (X[1] < obiekty[i].x)
            {
                X[1] = obiekty[i].x;
            }

            if (Y[0] > obiekty[i].y)
            {
                Y[0] = obiekty[i].y;
            }
            if (Y[1] < obiekty[i].y)
            {
                Y[1] = obiekty[i].y;
            }

            if (Z[0] > obiekty[i].z)
            {
                Z[0] = obiekty[i].z;
            }
            if (Z[1] < obiekty[i].z)
            {
                Z[1] = obiekty[i].z;
            }


        }
        Lista.Add(X);
        Lista.Add(Y);
        Lista.Add(Z);

        return Lista;
    }


    public  static List<iris> normalizowanie(List<iris> obiekty)
    {
        List<iris> ListaZnorma = new List<iris>();
        List<double[]> lista = minMax(obiekty);


        for (int k = 0; k < obiekty.Count; k++)
        {
            double xNorm = (obiekty[k].x - lista[0][0]) / (lista[0][1] - lista[0][0]);
            double yNorm = (obiekty[k].y - lista[1][0]) / (lista[1][1] - lista[1][0]);
            double zNorm = (obiekty[k].z - lista[2][0]) / (lista[2][1] - lista[2][0]);

            ListaZnorma.Add(new iris(xNorm, yNorm, zNorm, obiekty[k].klasa) { });
        }




        return ListaZnorma;

    }


}


public class najblizsze
{
    public static List<double>najblizej(List<List<iris>> podzielonyKlasy, int k, iris probka)
    {
        List<List<double>> wartosci = E.topKElements(podzielonyKlasy, probka);
        wartosci = E.sortowanie(wartosci);
        List<double> lista = nablizszeW(wartosci, k);
        return lista;
    }

    static List<double> nablizszeW(List<List<double>> wartosci, int k)
    { 
        List<double> lista = new List<double>();
        foreach (var klasa in wartosci)
        {
            double suma = 0;
            for (int i = 0; i < k; i++)
            {
                suma += klasa[i];
            }
            lista.Add(suma);
        }

        return lista;
    }
}
class Program
{

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
                int NrClassy = (int)double.Parse(pierwszaLinia.Split('\t')[3], CultureInfo.InvariantCulture) + 1;
                stworz.Add(new iris(x, y, z, NrClassy));
            }
        }

        return stworz;
    }


    static List<List<iris>> dzielenieNaKlasy(List<iris> Obiekty)
    {
        List<List<iris>> PodzieloneKlasy = new List<List<iris>>();
        List<iris> klasa1 = new List<iris>();
        List<iris> klasa2 = new List<iris>();
        List<iris> klasa3 = new List<iris>();
        PodzieloneKlasy.Add(klasa1);
        PodzieloneKlasy.Add(klasa2);
        PodzieloneKlasy.Add(klasa3);

        foreach (var ob in Obiekty)
        {
            if (ob.klasa == 1)
            {
                klasa1.Add(ob);
            }
            if (ob.klasa == 2)
            {
                klasa2.Add(ob);
            }
            if (ob.klasa == 3)
            {
                klasa3.Add(ob);
            }
        }

        return PodzieloneKlasy;
    }




    static void knn(List<iris> Obiekty, int k, iris probka)
    {
        List<List<iris>> podzieloneKlasy = dzielenieNaKlasy(Obiekty);
        List<double> najblizszeW = najblizsze.najblizej(podzieloneKlasy, k, probka);

        for(int i =0; i<najblizszeW.Count; i++)
        {
            Console.WriteLine("odległości Eukalidesa");
            Console.Write("dla klasy" + (i+1) + ": ");
            Console.WriteLine(najblizszeW[i]);
            Console.WriteLine("");
        }






    }


    static void Main()
    {
        int k = 3;
        string sciezka = "C:/Users/1/source/repos/knn/dane.txt";
        List<iris> Kwiaty = stworzListe(sciezka);

        
        Console.WriteLine("Podaj parametr x:");
        double xUser = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        Console.WriteLine("Podaj parametr y:");
        double yUser = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        Console.WriteLine("Podaj parametr z:");
        double zUser = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

        
        iris probka = new iris(xUser, yUser, zUser);
        List<iris> wszystkieObiekty = new List<iris>(Kwiaty);
        wszystkieObiekty.Add(probka);

        
        List<iris> znormalizowaneWszystko = normalizacja.normalizowanie(wszystkieObiekty);

        
        iris znormalizowanaProbka = znormalizowaneWszystko[znormalizowaneWszystko.Count - 1];

       
        znormalizowaneWszystko.RemoveAt(znormalizowaneWszystko.Count - 1);

        
        knn(znormalizowaneWszystko, k, znormalizowanaProbka);

       
        List<List<iris>> podzielone = dzielenieNaKlasy(znormalizowaneWszystko);
        List<double> sumaOdleglosci = najblizsze.najblizej(podzielone, k, znormalizowanaProbka);
        int przypisanaKlasa = sumaOdleglosci.IndexOf(sumaOdleglosci.Min()) + 1;

        Console.WriteLine($"Obiekt został przypisany do klasy: {przypisanaKlasa}");
    }

}
