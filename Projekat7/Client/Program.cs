using Common;
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
                string tekst;
                bool result;
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
                            result = proxy.CreateFolder(foldeName);
                            if (result)
                                Console.WriteLine("Folder uspesno kreiran.");
                            else
                                Console.WriteLine("Folder nije kreiran.");

                            break;

                        case 2:
                            Console.WriteLine("Molim vas upisete zeljeno ime za fajl:");
                            string fileName = Console.ReadLine();
                            result = proxy.CreateFile(fileName);
                            if (result)
                                Console.WriteLine("Fajl uspesno kreiran.");
                            else
                                Console.WriteLine("Fajl nije kreiran.");
                            break;

                        case 3:
                            Console.WriteLine("Molim vas upisete folder cije ime zelite da izmenite:");
                            string oldName = Console.ReadLine();
                            Console.WriteLine("Molim vas upisete zeljeno ime za folder:");
                            string newName = Console.ReadLine();
                            result = proxy.ModifyFolderName(oldName, newName);
                            if (result)
                                Console.WriteLine("Folder uspesno izmenjen.");
                            else
                                Console.WriteLine("Folder nije izmenjen.");

                            break;

                        case 4:
                            Console.WriteLine("Molim vas upisete koji fajl zelite da izmenite:");
                            string fileNameM = Console.ReadLine();
                            Console.WriteLine("Molim vas upisete novi naziv fajla");
                            string text = Console.ReadLine();

                            result = proxy.ModifyFile(fileNameM, text);
                            if (result)
                                Console.WriteLine("Fajl uspesno izmenjen.");
                            else
                                Console.WriteLine("Fajln nije izmenjen.");
                            break;
                        case 5:
                            Console.WriteLine("Molim vas upisete ime fajla koji zelite da procitate:");
                            string fileName2 = Console.ReadLine();
                            tekst = proxy.Read(fileName2);
                            if (tekst != "")
                            {
                                Console.WriteLine("Fajl uspesno procitan.");
                                Console.WriteLine(tekst);
                            }
                            else
                                Console.WriteLine("Fajl nije procitan.");
                            break;
                        case 6:
                            Console.WriteLine("Molim vas upisete ime foldera koji zelite da obrisete:");
                            string foldeName2 = Console.ReadLine();
                            result = proxy.DeleteFolder(foldeName2);
                            if (result)
                                Console.WriteLine("Folder uspesno izbrisan.");
                            else
                                Console.WriteLine("Folder nije izbrisan.");
                            break;
                        case 7:
                            Console.WriteLine("Molim vas upisete ime fajla koji zelite da obrisete:");
                            string fileNameD = Console.ReadLine();
                            result = proxy.DeleteFile(fileNameD);
                            if (result)
                                Console.WriteLine("Fajl uspesno izbrisan.");
                            else
                                Console.WriteLine("Fajl nije izbrisan.");
                            break;

                    }
                    result = false;
                } while (action != 0);



            }


        }
    }
}
