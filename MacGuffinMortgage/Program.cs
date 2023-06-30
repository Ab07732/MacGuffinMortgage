
using MacGuffinMortgage;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text.Json.Nodes;

namespace MacGuffinMortgage
{
    public class MortgageDatum
    {
        public string LoanGuid { get; set; }
        public int LoanId { get; set; }
        public string BorrowerFirstName { get; set; }
        public string BorrowerLastName { get; set; }
        public string SubjectAddress1 { get; set; }
        public string SubjectAddress2 { get; set; }
        public string SubjectCity { get; set; }
        public string SubjectState { get; set; }
        public string SubjectZip { get; set; }
        public double SubjectAppraisedAmount { get; set; }
        public double LoanAmount { get; set; }
        public double InterestRate { get; set; }
    }

    public class Root
    {
        public List<MortgageDatum> MortgageData { get; set; }
    }
     
    class Program
    {
        static void Main(string[] args)
        {
            string link = @"C:\Users\austi\source\repos\MacGuffinMortgage\MacGuffinMortgage\loans.json";



            WebRequest request = WebRequest.Create(link);
            WebResponse response = request.GetResponse();



            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                //string json = JsonConvert.SerializeObject(reader, Formatting.Indented);
                Root root = JsonConvert.DeserializeObject<Root>(responseFromServer);
                //string json = JsonConvert.SerializeObject(reader, Formatting.Indented);

                foreach (MortgageDatum md in root.MortgageData)
                {
                    Console.WriteLine("SubjectState: " + md.SubjectState + ", LoanAmount: " + md.LoanAmount + ", SubjectAppraisedAmount: " + md.SubjectAppraisedAmount + ", InterestRate: " + md.InterestRate);
                    //for(int i = 0; i < md.InterestRate; i++)
                    //{
                    //    Console.WriteLine(md.InterestRate);
                    //}
                }

                MortgageDatum MD = new MortgageDatum();

                string JSONresult = JsonConvert.SerializeObject(MD);
                string path = @"C:\Users\austi\source\repos\MacGuffinMortgage\MacGuffinMortgage\loans2.json";

                if (File.Exists(path))
                {
                    File.Delete(path);
                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine(JSONresult.ToString());
                        tw.Close();
                    }
                }
                else if (!File.Exists(path))
                {
                    using (var tw = new StreamWriter(path, true))
                    {
                        tw.WriteLine(JSONresult.ToString());
                        tw.Close();
                    }
                }

            }


            Console.ReadKey();
        }

    }
}