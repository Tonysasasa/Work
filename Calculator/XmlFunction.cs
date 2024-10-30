using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Calculator
{
    public class XmlFunction
    {
        private static void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown Node:" + e.Name + "\tline:" + e.LineNumber.ToString() + "\tpos:" + e.LinePosition.ToString());
        }
        static void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute " +
            attr.Name + "='" + attr.Value + "'");
        }

        public string SerializeOperation(MyMaths myMaths)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MyMaths));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, myMaths, namespaces);
                return writer.ToString();
            }
        }


        // Method to deserialize an XML file into an Operation object
        public MyMaths DeserializeObject1(string filename)
        {
            MyMaths mymaths = new MyMaths();
   
    
            XmlSerializer serializer = new XmlSerializer(typeof(MyMaths));
            

            serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
            serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                mymaths = (MyMaths)serializer.Deserialize(fs);
            }
            return mymaths;
        }
    }
}
