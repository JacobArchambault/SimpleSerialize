using System;
using System.Xml.Serialization;

namespace SimpleSerialize
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.MyCompany.com")]
    public class JamesBondCar : Car
    {
        [XmlAttribute]
        public bool canFly;
        [XmlAttribute]
        public bool canSubmerge;

        public JamesBondCar(bool skyWorthy, bool seaWorthy)
        {
            canFly = skyWorthy;
            canSubmerge = seaWorthy;
        }
        // The XmlSerializer demands a default constructor!
        public JamesBondCar() { }

    }
}
