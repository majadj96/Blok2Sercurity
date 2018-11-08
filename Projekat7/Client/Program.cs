﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ProxyProtection proxy = new ProxyProtection())
            {

                proxy.CheckLevel();

                int action;

                do
                {

                    Console.WriteLine("*********MENI*********");
                    Console.WriteLine("1. Pravljenje foldera");
                    Console.WriteLine("2. Pravljenje fajla");
                    Console.WriteLine("3. Izmena imena foldera");
                    Console.WriteLine("4. Izmena fajla");
                    Console.WriteLine("5. Citanje iz fajla");
                    Console.WriteLine("6. Brisanje foldera");
                    Console.WriteLine("7. Brisanje fajla");
                    Console.WriteLine("Za zeljenu akciju, izaberite odgovarajucu brojku :)");

                    action = 0;

                    action = Convert.ToInt32(Console.ReadLine());

                    switch (action)
                    {
                        case 1:
                            Console.WriteLine("Molim vas upisete zeljeno ime za folder:");
                            string foldeName = Console.ReadLine();
                            proxy.CreateFolder(foldeName);
                            break;
                        case 2:
                            Console.WriteLine("Molim vas upisete zeljeno ime za fajl:");
                            string fileName = Console.ReadLine();
                            proxy.CreateFile(fileName);
                            break;
                        case 3:
                            Console.WriteLine("Molim vas upisete folder cije ime zelite da izmenite:");
                            string oldName = Console.ReadLine();
                            Console.WriteLine("Molim vas upisete zeljeno ime za folder:");
                            string newName = Console.ReadLine();
                            proxy.ModifyFolderName(oldName,newName);
                            break;
                        case 4:
                            Console.WriteLine("Molim vas upisete koji fajl zelite da izmenite:");
                            string fileNameM = Console.ReadLine();
                            proxy.ModifyFile(fileNameM);
                            break;
                        case 5:
                            Console.WriteLine("Molim vas upisete ime fajla koji zelite da procitate:");
                            string fileName2 = Console.ReadLine();
                            proxy.Read(fileName2);
                            break;
                        case 6:
                            Console.WriteLine("Molim vas upisete ime foldera koji zelite da obrisete:");
                            string foldeName2 = Console.ReadLine();
                            proxy.DeleteFolder(foldeName2);
                            break;
                        case 7:
                            Console.WriteLine("Molim vas upisete ime fajla koji zelite da obrisete:");
                            string fileNameD = Console.ReadLine();
                            proxy.DeleteFile(fileNameD);
                            break;

                    }

                } while (action != 0);



            }


        }
    }
}
