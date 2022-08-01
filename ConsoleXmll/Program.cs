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
            int j =0;
            List<RootFIELD_DATA> list = new List<RootFIELD_DATA>();
            list = (from e in XDocument.Load("C:\\Users\\Vittorio\\source\\repos\\ConsoleXmll\\ConsoleXmll\\prova.xml").Root.Elements("FIELD_DATA")
                    select new RootFIELD_DATA
                    {
                        MSG_ID = (string)e.Attribute("MSG_ID"),
                        MSG_DT = (string)e.Attribute("MSG_DT"),
                        CISNUM = (string)e.Attribute("CISNUM"),
                        MODCOD = (string)e.Attribute("MODCOD"),
                        SEQNUM = (string)e.Attribute("SEQNUM"),
                        LB__ID = (string)e.Attribute("LB__ID"),
                        LINCOD = (string)e.Attribute("LINCOD"),
                        PRGNUM = (string)e.Attribute("PRGNUM"),
                        LINDES = (string)e.Attribute("LINDES"),
                        SUBGROUP_DESCRIPTION = (string)e.Attribute("SUBGROUP_DESCRIPTION"),
                        TRACED_OPERATION=(from f in e.Elements("TRACED_OPERATION")
                                                 select new RootFIELD_DATATRACED_OPERATION
                                                 { 
                                                        OPCODE = (string)f.Attribute("OPCODE"),
                                                        OPTYPE = (string)f.Attribute("OPTYPE"),
                                                        OPCYCLE = (string)f.Attribute("OPCYCLE"),
                                                        OPTOOL_ID = (string)f.Attribute("OPTOOL_ID"),
                                                        OP_RESULT = (string)f.Attribute("OP_RESULT"),
                                                        OPERATOR = (string)f.Attribute("OPERATOR"),
                                                        OPDATE = (string)f.Attribute("OPDATE"),
                                                        SINGLE_ITEM = (from o in f.Elements("SINGLE_ITEM")
                                                         select new RootFIELD_DATATRACED_OPERATIONSINGLE_ITEM
                                                         {
                                                            ITEM_ID = (string)o.Attribute("ITEM_ID"),
                                                            ITEM_DESC = (string)o.Attribute("ITEM_DESC"),
                                                            ITEM_RESULT = (string)o.Attribute("ITEM_RESULT"),
                                                            OPERATOR = (string)o.Attribute("OPERATOR"),
                                                            OPTOOL_ID = (string)o.Attribute("OPTOOL_ID"),
                                                            SINGLE_OP_RESULT = (from i in o.Elements("SINGLE_OP_RESULT")
                                                                                select new RootFIELD_DATATRACED_OPERATIONSINGLE_ITEMSINGLE_OP_RESULT
                                                                                {
                                                                                    TRY_ID = (string)i.Attribute("TRY_ID"),
                                                                                    TRY_RESULT = (string)i.Attribute("TRY_RESULT"),
                                                                                    OPERATOR = (string)i.Attribute("OPERATOR"),
                                                                                    OPTOOL_ID = (string)i.Attribute("OPTOOL_ID"),
                                                                                    MEASUREMENT = (from h in i.Elements("MEASUREMENT")
                                                                                                    select new RootFIELD_DATATRACED_OPERATIONSINGLE_ITEMSINGLE_OP_RESULTMEASUREMENT
                                                                                                    {
                                                                                                        MEAS_DESCR = (string)h.Attribute("MEAS_DESCR"),
                                                                                                        MEASURE = (string)h.Attribute("MEASURE"),
                                                                                                        VALUE = (string)h.Attribute("VALUE"),
                                                                                                        UM = (string)h.Attribute("UM"),
                                                                                                        UPPER_TOLERANCE = (string)h.Attribute("UPPER_TOLERANCE"),
                                                                                                        LOWER_TOLERANCE = (string)h.Attribute("LOWER_TOLERANCE")
                                                                                                    }).ToArray()
                                                                                    }).ToArray()

                                                             }).ToArray()
                                            }).ToArray()

                    }).ToList();
            XmlSerializer serializer = new XmlSerializer(list.GetType());
            using (StreamWriter writer = new StreamWriter(@"C:\Users\Vittorio\Desktop\pippo.xml"))
            {
                serializer.Serialize(writer, list);
            }
            //foreach (var item in list)
            //    Console.WriteLine(item.TRACED_OPERATION[0].SINGLE_ITEM[0].ITEM_ID);
            //Console.ReadLine();

            //var xml = new XElement("Root", from v in list
            //                               select new XElement("FIELD_DATA", new XAttribute("MSG_ID", v.MSG_ID), new XAttribute("MSG_DT", v.MSG_DT),
            //         new XAttribute("CISNUM", v.CISNUM),
            //         new XAttribute("MODCOD", v.MODCOD),
            //         new XAttribute("SEQNUM", v.SEQNUM),
            //         new XAttribute("LB__ID", v.LB__ID),
            //         new XAttribute("LINCOD", v.LINCOD),
            //         new XAttribute("PRGNUM", v.PRGNUM),
            //         new XAttribute("LINDES", v.LINDES),
            //         new XAttribute("SUBGROUP_DESCRIPTION", v.SUBGROUP_DESCRIPTION),
            //         new XElement("TRACED_OPERATION", new XAttribute("OPCODE", v.TRACED_OPERATION[0].OPCODE), new XAttribute("OPTYPE", v.TRACED_OPERATION[0].OPTYPE),
            //         new XAttribute("OPCYCLE", v.TRACED_OPERATION[0].OPCYCLE),
            //         new XAttribute("OPTOOL_ID", v.TRACED_OPERATION[0].OPTOOL_ID),
            //         new XAttribute("OP_RESULT", v.TRACED_OPERATION[0].OP_RESULT),
            //         new XAttribute("OPERATOR", v.TRACED_OPERATION[0].OPERATOR),
            //         new XAttribute("OPDATE", v.TRACED_OPERATION[0].OPDATE),
            //         new XElement("SINGLE_ITEM", new XAttribute("ITEM_ID", v.TRACED_OPERATION[0].SINGLE_ITEM[0].ITEM_ID), new XAttribute("ITEM_DESC", v.TRACED_OPERATION[0].SINGLE_ITEM[0].ITEM_DESC),
            //         new XAttribute("ITEM_RESULT", v.TRACED_OPERATION[0].SINGLE_ITEM[0].ITEM_RESULT),
            //         new XAttribute("OPERATOR", v.TRACED_OPERATION[0].SINGLE_ITEM[0].OPERATOR),
            //         new XAttribute("OPTOOL_ID", v.TRACED_OPERATION[0].SINGLE_ITEM[0].OPTOOL_ID),
            //         new XElement("SINGLE_OP_RESULT", new XAttribute("TRY_ID", v.TRACED_OPERATION[0].SINGLE_ITEM[0].SINGLE_OP_RESULT[0].TRY_ID), new XAttribute("TRY_RESULT", v.TRACED_OPERATION[0].SINGLE_ITEM[0].SINGLE_OP_RESULT[0].TRY_RESULT),
            //         new XAttribute("OPERATOR", v.TRACED_OPERATION[0].SINGLE_ITEM[0].SINGLE_OP_RESULT[0].OPERATOR),
            //         new XAttribute("OPTOOL_ID", v.TRACED_OPERATION[0].SINGLE_ITEM[0].SINGLE_OP_RESULT[0].OPTOOL_ID),
            //         new XElement("MEASUREMENT", new XAttribute("MEAS_DESCR", v.TRACED_OPERATION[0].SINGLE_ITEM[0].SINGLE_OP_RESULT[0].MEASUREMENT[0].MEAS_DESCR), new XAttribute("MEASURE", v.TRACED_OPERATION[0].SINGLE_ITEM[0].SINGLE_OP_RESULT[0].MEASUREMENT[0].MEASURE),
            //         new XAttribute("VALUE", v.TRACED_OPERATION[0].SINGLE_ITEM[0].SINGLE_OP_RESULT[0].MEASUREMENT[0].VALUE),
            //         new XAttribute("UM", v.TRACED_OPERATION[0].SINGLE_ITEM[0].SINGLE_OP_RESULT[0].MEASUREMENT[0].UM),
            //         new XAttribute("UPPER_TOLERANCE", v.TRACED_OPERATION[0].SINGLE_ITEM[0].SINGLE_OP_RESULT[0].MEASUREMENT[0].UPPER_TOLERANCE),
            //         new XAttribute("LOWER_TOLERANCE", v.TRACED_OPERATION[0].SINGLE_ITEM[0].SINGLE_OP_RESULT[0].MEASUREMENT[0].LOWER_TOLERANCE)
            //                              )
            //                              )
            //                              )
            //                              )
            //                              )
            //                             );
            //xml.Save(@"C:\\Users\\Vittorio\\source\\repos\\ConsoleXmll\\ConsoleXmll\\test.xml");

        }
    }
}
