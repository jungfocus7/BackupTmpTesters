using Microsoft.VisualBasic;
using OklahomaWorld.DataPump;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace OklahomaWorld
{
    public static class MainProgram
    {



        public static void Main(string[] args)
        {
            string allText = @"
C01	C02	C03	C04	C05	C06	C07	C08	C09	C10
RA1	RA2	RA3	RA4	RA5	RA6	RA7	RA8	RA9	RA10
RB1	RB2	RB3	RB4	RB5	RB6	RB7	RB8	RB9	RB10
RC1	RC2	RC3	RC4	RC5	RC6	RC7	RC8	RC9	RC10
RD1	RD2	RD3	RD4	RD5	RD6	RD7	RD8	RD9	RD10
RE1	RE2	RE3	RE4	RE5	RE6	RE7	RE8	RE9	RE10
RF1	RF2	RF3	RF4	RF5	RF6	RF7	RF8	RF9	RF10
RG1	RG2	RG3	RG4	RG5	RG6	RG7	RG8	RG9	RG10
RH1	RH2	RH3	RH4	RH5	RH6	RH7	RH8	RH9	RH10
RI1	RI2	RI3	RI4	RI5	RI6	RI7	RI8	RI9	RI10
RJ1	RJ2	RJ3	RJ4	RJ5	RJ6	RJ7	RJ8	RJ9	RJ10
RK1	RK2	RK3	RK4	RK5	RK6	RK7	RK8	RK9	RK10
            ".Trim();



            using (HfSqlMaker sqlMaker = HfSqlMaker.Create(allText, "DATATABLE"))
            {
                if (sqlMaker != null)
                {
                    sqlMaker.MakeSelect();
                    sqlMaker.MakeUpdate();
                    sqlMaker.MakeInsert();
                    sqlMaker.MakeDelete();

                    Console.WriteLine(sqlMaker.GetResult().query);
                }
            }

        }



    }



}
