using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            string map = @"C:\Users\jobka\Documents\Hoge School\Analyse\Periode 4\TestMap\";
            string file = "TestOne.txt";
            test test1 = new test(map, file);
            System.Threading.Thread.Sleep(100000);
        }

    }

    public class test
    {
        string path;
        string map;
        string file;

        List<List<int>> Inputs;
        List<int> ExpectedResults;
        List<int> Output;
        List<List<int>> data;


        public test(string ma, string fi)
        {
            map = ma;
            file = fi;
            path = map + file;
            Output = new List<int>();



            //Inputs = In;
            //ExpectedResults = ExRes;
            data = readdata();

            ExpectedResults = data[0];
            data.RemoveAt(0);
            Inputs = data;
            StartTest();
        }


        public List<List<int>> readdata()
        {
            // data = [expectedoutput, input<list>]
            List<List<int>> OutputList = new List<List<int>>();

            List<int> ExpectedOutput = new List<int>();

            OutputList.Add(ExpectedOutput);

            String[] lines = System.IO.File.ReadAllLines(path);

            foreach (string l in lines)
            {
                string line;
                if ((l.Length) > 1)
                {
                    string firstChar = l.Substring(0, 1);
                    line = l.Substring(1);
                    if (firstChar == "/")
                    {
                        string[] d = line.Split(','); //data

                        // creates input lists
                        int count = 0;
                        foreach (string singledata in d)
                        {
                            if (OutputList.Count() <= count)
                            {
                                OutputList.Add(new List<int>());
                            }
                            OutputList[count].Add(Convert.ToInt32(singledata));

                            count += 1;
                        }

                    }
                }

            }
            return OutputList;
        }


        public void StartTest()
        {

            // Checking results and printing
            int Accepted = 0;
            int Rejected = 0;

            int count = -1;
            foreach (int Result in ExpectedResults)
            {
                count += 1;

                List<int> inpu = new List<int>();

                foreach (List<int> inp in Inputs)
                {
                    inpu.Add(inp[count]);
                }

                Output.Add(TestAdapter(inpu));
            }
            Console.Write("File name: ");

            string Filename = Console.ReadLine();
            Console.Clear();
            string newfilepath = map + Filename + ".txt";

            using (StreamWriter sw = File.CreateText(newfilepath))
            {
                int Count = -1;
                sw.WriteLine("Testresults of (" + path + ")\n");
                foreach (int Result in ExpectedResults)
                {
                    Count += 1;
                    int outp = Output[Count];
                    string inputstr = "";
                    string status = "";
                    foreach (List<int> In in Inputs)
                    {
                        inputstr += In[Count].ToString() + " ";
                    }

                    if (outp.ToString() == Result.ToString())
                    {

                        status = "Accepted";
                        Accepted += 1;
                    }
                    else
                    {
                        Rejected += 1;
                        status = "Rejected";
                    }
                    sw.WriteLine("Step " + (Count + 1) + "\t | " + status + " | \t Inputs: " + inputstr + "\t | Expected result: " + ExpectedResults[Count] + "\t | Output result: " + Output[Count]);
                }
                sw.WriteLine("Finished testing with " + Accepted + " accepted and " + Rejected + " rejected results");
                Console.Write("Saved file at " + newfilepath + "\n\n" + (new string('-', 95)) + "\n\n");
                sw.Close();
                // printing it
                using (StreamReader sr = new StreamReader(newfilepath))
                {
                    String line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
                System.Diagnostics.Process.Start(newfilepath);
            }
        }


        public int TestAdapter(List<int> inputs)
        {
            /*
            //testing blijdorp
            // adult_subscription, adults, child_subscription, child, baby, int begeleider, int childguest, int adultguest, int onlinebesteld (0/1)
            Blijdorp blijdorp = new Blijdorp(inputs[0], inputs[1], inputs[2], inputs[3], inputs[4], inputs[5], inputs[6], inputs[7], inputs[8]);
            return blijdorp.GetPrice();

            //testing atm machine


            return 0;
            */
            /*
            //testing geldautomaat
            // status (1 = online), limiet, voorraad
            Geldautomaat automaat = new Geldautomaat(inputs[0], inputs[1], inputs[2]);
            return automaat.run();

            */
  
            //testing our own geldautomaat
            MyOwnGeldAutomaat automaat = new MyOwnGeldAutomaat();
            return automaat.Run(inputs[0], inputs[1], inputs[2]);

        }

    }


    class Blijdorp
        {
            int adult_sub;
            int adult;
            int child_sub;
            int child;
            int baby;
            int accompanist;
            int child_guest;
            int adult_guest;
            Boolean online;

            public Blijdorp(int ad_sub, int ad, int ch_sub, int ch, int ba, int acc, int ch_guest, int ad_guest, int on)
            {

                online = (on == 1);
                int max = 100;
                int min = 0;
                if (!online)
                {
                    max = 1000; // to keep people from using unrealistic numbers
                    adult = GetInput(ad);
                    child = GetInput(ch);
                    adult_sub = GetInput(ad_sub);
                    child_sub = GetInput(ch_sub);
                    accompanist = GetInput(acc);
                    Console.WriteLine(accompanist);
                    baby = GetInput(ba);

                    int guest = (adult_sub + child_sub) * 4;
                    if (adult - guest < 0)
                    {
                        adult_guest = adult;
                        guest = guest - adult;
                        adult = 0;
                        if (child - guest < 0)
                        {
                            child_guest = child;
                            guest = guest - child;
                            child = 0;
                        }
                        else
                        {
                            child_guest = guest;
                            child = child - guest;
                        }
                    }
                    else
                    {
                        adult_guest = guest;
                        adult = adult - guest;
                    }


                }
                else
                {
                    max = 20;
                }


            }

            private int GetInput(int res)
            {
                int min = 0;
                int max = 1000;
                if (res < min || res > max)
                {
                    return 0;
                }
                return res;
            }

            private Boolean GetBoolInput(string question)
            {
                Console.WriteLine(question);
                string res = Console.ReadLine();
                while (res != "true" && res != "false")
                {
                    Console.WriteLine(question);
                    res = Console.ReadLine();
                }
                return Convert.ToBoolean(res);
            }

            public int GetPrice()
            {
                double res = 0.00;
                double adult_price = 23.00;
                double child_price = 18.50;
                double accompanist_price = 13.00;

                if (online)
                {
                    adult_price = adult_price - 2;
                    child_price = child_price - 2;
                    res = res + (adult * adult_price);
                    res = res + (child * child_price);
                }
                else
                {
                    if (adult + adult_guest + adult_sub + child + child_guest + child_sub + accompanist + baby >= 20)
                    {
                        adult_price = adult_price - 2;
                        child_price = child_price - 2;
                    }
                    res = res + (adult * adult_price);
                    res = res + (child * child_price);
                    res = res + (adult_guest * (adult_price * 0.75));
                    res = res + (child_guest * (child_price * 0.75));
                    res = res + (accompanist * accompanist_price);
                }
                Console.WriteLine(res);
                return Convert.ToInt32(res);
            }
        }

    class Geldautomaat
        {
            string statusgeldautomaat;
            int limiet;
            int voorraad;
            int opgevraagdbedrag = 500;
            int saldo = 1000;
            int kredietlimiet = 300;
            public Geldautomaat(int stat, int lim, int voor)
            {
                if (stat == 1)
                {
                    statusgeldautomaat = "on";
                }
                else
                {
                    statusgeldautomaat = "off";
                }

                voorraad = voor;
                limiet = lim;
            }

            public int run() //Checkt of het bedrag kan worden gegeven.
            {
                if ((saldo >= opgevraagdbedrag || opgevraagdbedrag - saldo <= kredietlimiet) && statusgeldautomaat == "on")
                {
                    if (opgevraagdbedrag <= limiet && opgevraagdbedrag <= voorraad)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

    class MyOwnGeldAutomaat
    {
        public int Run(int stat, int Opanemlimiet, int Voorraad) // standaard saldo bankpas = 500, kredietlimiet = 200, opname van 200 euro
        {
            String Status;
            if (stat == 1)
            {
                Status = "Online";
            }
            else
            {
                Status = "Offline";
            }


            int Saldo = 1000;
            int Kredietlimiet = 200;

            int OpTeNemen = 500;


            bool geven = false;

            // opnemen geld

            if (Status == "Online")
            {
                if (Voorraad - OpTeNemen >= 0)
                {
                    if (Opanemlimiet - OpTeNemen >= 0)
                    {
                        if (Saldo - OpTeNemen >= 0)
                        {
                            geven = true;
                        }

                        else
                        {
                            if (Saldo - OpTeNemen + Kredietlimiet >= 0)
                            {
                                geven = true;
                            }
                        }
                    }
                }

                if (geven == true)
                {
                    return 1;
                }
                else
                {
                }


            }
            return 0;
        }
    }
}

