using System.Collections.Generic;
using System.IO;
// Gain access to the BinaryFormatter in mscorlib.dll.
using System.Runtime.Serialization.Formatters.Binary;
// Must reference System.Runtime.Serialization.Formatters.Soap.dll.
using System.Runtime.Serialization.Formatters.Soap;
// Defined within System.Xml.dll.
using System.Xml.Serialization;
using static System.Console;
using static System.IO.File;

namespace SimpleSerialize
{
    class Program
    {
        // Be sure to import the System.Runtime.Serialization.Formatters.Binary
        // and System.IO namespaces.
        static void Main()
        {
            WriteLine("***** Fun with Object Serialization *****\n");

            // Make a JamesBondCar and set state.
            JamesBondCar jbc = new JamesBondCar
            {
                canFly = true,
                canSubmerge = false
            };
            jbc.theRadio.stationPresets = new double[] { 89.3, 105.1, 97.1 };
            jbc.theRadio.hasTweeters = true;

            // Now save the car to a specific file in a binary format.
            SaveAsBinaryFormat(jbc, "CarData.dat");
            LoadFromBinaryFile("CarData.dat");

            // Save as SOAP.
            SaveAsSoapFormat(jbc, "CarData.soap");

            // XML
            SaveAsXmlFormat(jbc, "CarData.xml");

            SaveListOfCars();
            SaveListOfCarsAsBinary();

            ReadLine();
        }

        static void LoadFromBinaryFile(string fileName)
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            // Read the JamesBondCar from the binary file.
            using (Stream fStream = OpenRead(fileName))
            {
                JamesBondCar carFromDisk =
                  (JamesBondCar)binFormat.Deserialize(fStream);
                WriteLine($"Can this car fly? : {carFromDisk.canFly}");
            }
        }

        static void SaveAsBinaryFormat(object objGraph, string fileName)
        {
            // Save object to a file named CarData.dat in binary.
            BinaryFormatter binFormat = new BinaryFormatter();

            using (Stream fStream = new FileStream(fileName,
                  FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, objGraph);
            }
            WriteLine("=> Saved car in binary format!");
        }

        // Be sure to import System.Runtime.Serialization.Formatters.Soap
        // and reference System.Runtime.Serialization.Formatters.Soap.dll.
        static void SaveAsSoapFormat(object objGraph, string fileName)
        {
            // Save object to a file named CarData.soap in SOAP format.
            SoapFormatter soapFormat = new SoapFormatter();

            using (Stream fStream = new FileStream(fileName,
              FileMode.Create, FileAccess.Write, FileShare.None))
            {
                soapFormat.Serialize(fStream, objGraph);
            }
            WriteLine("=> Saved car in SOAP format!");
        }

        static void SaveAsXmlFormat(object objGraph, string fileName)
        {
            // Save object to a file named CarData.xml in XML format.
            XmlSerializer xmlFormat = new XmlSerializer(typeof(JamesBondCar));

            using (Stream fStream = new FileStream(fileName,
              FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, objGraph);
            }
            WriteLine("=> Saved car in XML format!");
        }


        static void SaveListOfCars()
        {
            // Now persist a List<T> of JamesBondCars.
            List<JamesBondCar> myCars = new List<JamesBondCar>
            {
                new JamesBondCar(true, true),
                new JamesBondCar(true, false),
                new JamesBondCar(false, true),
                new JamesBondCar(false, false)
            };

            using (Stream fStream = new FileStream("CarCollection.xml",
              FileMode.Create, FileAccess.Write, FileShare.None))
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<JamesBondCar>));
                xmlFormat.Serialize(fStream, myCars);
            }
            WriteLine("=> Saved list of cars!");
        }

        static void SaveListOfCarsAsBinary()
        {
            // Save ArrayList object (myCars) as binary.
            List<JamesBondCar> myCars = new List<JamesBondCar>();

            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream("AllMyCars.dat",
              FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, myCars);
            }
            WriteLine("=> Saved list of cars in binary!");
        }
    }
}