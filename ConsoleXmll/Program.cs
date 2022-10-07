using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConsoleXmll
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "E:\\Pacco Batteria XML\\ConsoleXmll\\ConsoleXmll\\PLCDB.config";
            List<RootFIELD_DATA> list = new List<RootFIELD_DATA>();
            list = (from e in XDocument.Load(file).Descendants("PLC")
                    select new RootFIELD_DATA
                    {
                        MSG_ID =  (string)e.Attribute("MSG_ID"),
                        MSG_DT = (string)e.Attribute("MSG_DT"),
                        CISNUM = (string)e.Attribute("CISNUM"),
                        MODCOD = (string)e.Attribute("MODCOD"),
                        SEQNUM = (string)e.Attribute("SEQNUM"),
                        LB__ID = (string)e.Attribute("LB__ID"),
                        LINCOD = (string)e.Attribute("LINCOD"),
                        PRGNUM = (string)e.Attribute("PRGNUM"),
                        LINDES = (string)e.Attribute("LINDES"),
                        SUBGROUP_DESCRIPTION = (string)e.Attribute("SUBGROUP_DESCRIPTION"),
                        PROCESSING_DATA_SECTION = (from f in XDocument.Load("C:\\Users\\Vittorio\\source\\repos\\ConsoleXmll\\ConsoleXmll\\prova.xml").Descendants("TRACED_OPERATION")
                                                 select new RootFIELD_DATAPROCESSING_DATA_SECTIONTRACED_OPERATION
                                                 { 
                                                        OPCODE = (string)f.Attribute("OPCODE"),
                                                        OPTYPE = (string)f.Attribute("OPTYPE"),
                                                        OPCYCLE = (string)f.Attribute("OPCYCLE"),
                                                        OPTOOL_ID = (string)f.Attribute("OPTOOL_ID"),
                                                        OP_RESULT = (string)f.Attribute("OP_RESULT"),
                                                        OPERATOR = (string)f.Attribute("OPERATOR"),
                                                        OPDATE = (string)f.Attribute("OPDATE"),
                                                        SINGLE_ITEM = (from o in f.Elements("SINGLE_ITEM")
                                                         select new RootFIELD_DATAPROCESSING_DATA_SECTIONTRACED_OPERATIONSINGLE_ITEM
                                                         {
                                                            ITEM_ID = (string)o.Attribute("ITEM_ID"),
                                                            ITEM_DESC = (string)o.Attribute("ITEM_DESC"),
                                                            ITEM_RESULT = (string)o.Attribute("ITEM_RESULT"),
                                                            OPERATOR = (string)o.Attribute("OPERATOR"),
                                                            OPTOOL_ID = (string)o.Attribute("OPTOOL_ID"),
                                                            SINGLE_OP_RESULT = (from i in o.Elements("SINGLE_OP_RESULT")
                                                                                select new RootFIELD_DATAPROCESSING_DATA_SECTIONTRACED_OPERATIONSINGLE_ITEMSINGLE_OP_RESULT
                                                                                {
                                                                                    TRY_ID = (string)i.Attribute("TRY_ID"),
                                                                                    TRY_RESULT = (string)i.Attribute("TRY_RESULT"),
                                                                                    OPERATOR = (string)i.Attribute("OPERATOR"),
                                                                                    OPTOOL_ID = (string)i.Attribute("OPTOOL_ID"),
                                                                                    MEASUREMENT = (from h in i.Elements("MEASUREMENT")
                                                                                                    select new RootFIELD_DATAPROCESSING_DATA_SECTIONTRACED_OPERATIONSINGLE_ITEMSINGLE_OP_RESULTMEASUREMENT
                                                                                                    {
                                                                                                        MEAS_DESCR = (string)h.Attribute("MEAS_DESCR"),
                                                                                                        MEASURE = (string)h.Attribute("MEASURE"),
                                                                                                        VALUE = (string)h.Attribute("VALUE"),
                                                                                                        UM = (string)h.Attribute("UM"),
                                                                                                        UPPER_TOLERANCE = (string)h.Attribute("UPPER_TOLERANCE"),
                                                                                                        LOWER_TOLERANCE = (string)h.Attribute("LOWER_TOLERANCE"),
                                                                                                        Value=(string)h.Value
                                                                                                    }).ToArray()
                                                                                    }).ToArray()

                                                             }).ToArray()
                                            }).ToArray()

                    }).ToList();
            XmlSerializer serializer = new XmlSerializer(list.GetType());
            using (StreamWriter writer = new StreamWriter(@"C:\Users\Vittorio\Desktop\pluto.xml"))
            {
                serializer.Serialize(writer, list);
            }
            //foreach (var item in list)
            //    Console.WriteLine(item.MODCOD);
            //Console.ReadLine();


        }
    }
}
