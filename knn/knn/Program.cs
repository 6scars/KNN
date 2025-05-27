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
                int NrClassy = (int)double.Parse(pierwszaLinia.Split('\t')[3], CultureInfo.InvariantCulture) + 1;
                stworz.Add(new iris(x, y, z, NrClassy));
            }
        }

        return stworz;
    }


    public static double[] Euklidesowa(List<List<iris>> A, iris probka)
    {
        // int k = 3;
        // double [] minWynik = new double[3];
        // minWynik[0] = Math.Pow(A[0][0].x - probka.x, 2) + Math.Pow(A[0][0].y - probka.y, 2) + Math.Pow(A[0][0].z - probka.z, 2);
        // double temp = minWynik[0];

        // foreach (var ob in A[i]) //czyli na klasie pierwszej robie teraz obliczanie
        // {
        //     temp = Math.Pow(ob.x - probka.x, 2) + Math.Pow(ob.y - probka.y, 2) + Math.Pow(ob.z - probka.z, 2);
        // }

        // int i = 0;
        //if(minWynik.Length > 0)
        // {

        // }
        // else
        // {
        //     if (minWynik[0] > temp)
        //     {
        //         minWynik[0] = Math.Sqrt(temp);
        //     }
        // }


        //     return minWynik;
    }

    //double Manhatan(double[] A, double[]B)
    //{
    //    double wynik = 0;
    //    for(int i=0; i<A.Length-1; i++)
    //    {
    //        wynik += Math.Abs(A[i] - B[i]);
    //    }
    //    return wynik;
    //}


    static List<double[]> minMax(List<iris> obiekty)
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


    static List<iris> normalizacja(List<iris> obiekty)
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


    static List<iris> najblizej(List<List<iris>> podzielonyKlasy, int k, iris probka)
    {
        List<iris> najblizsze = new List<iris>();
        double najmWar = Euklidesowa(podzielonyKlasy, probka);




        //foreach (var listaKlasy in podzielonyKlasy)
        //{

        //}
        //double najmWart = Euklidesowa(podzielonyKlasy[0], wartoscParaProbki);


        return najblizsze;
    }

    static void knn(List<iris> Obiekty, int k, iris probka)
    {
        List<List<iris>> podzieloneKlasy = dzielenieNaKlasy(Obiekty);
        Console.WriteLine(podzieloneKlasy[0][0].x);

        najblizej(podzieloneKlasy, k, probka);




    }


    static void Main()
    {
        int nr_probki = 5;
        int k = 3;
        string sciezka = "C:/Users/1/source/repos/knn/dane.txt";
        List<iris> Kwiaty = stworzListe(sciezka);
        Kwiaty = normalizacja(Kwiaty);
        iris kwiatek = new iris(
            Kwiaty[nr_probki].x,
            Kwiaty[nr_probki].y,
            Kwiaty[nr_probki].z
            );
        Kwiaty.RemoveAt(nr_probki);

        knn(Kwiaty, k, kwiatek);









    }
}
