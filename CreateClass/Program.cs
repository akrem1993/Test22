using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace CreateClass
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-RJ44SKV;Integrated Security=True;Initial Catalog=penDB");
            SqlDataAdapter dp = new SqlDataAdapter("select * from GetNames",con);
            DataTable table = new DataTable();
            dp.Fill(table);
            foreach (DataRow col in table.Rows)
            {
                string classname = "";
                string tek = col[0].ToString();
                if(tek.EndsWith("s")) classname = tek.Remove(tek.Length - 1, 1);
                if (tek.EndsWith("ies")) classname = tek.Replace("ies", "y");
                if (tek.EndsWith("Master")) classname = tek;
                if (tek=="Cashes") classname = "Cash";

                string tablename = col[0].ToString();
                string viewname = col[1].ToString();

                //SqlCommand cmd = new SqlCommand("SetToLEntity", con);
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //cmd.Parameters.Add("@ViewName", SqlDbType.NVarChar).Value = viewname;
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //string property = "";
                //da.Fill(dt);
                //foreach (DataRow row in dt.Rows)
                //{
                //    property += "\t\t" + row[0] + "\n";
                //}
                //string lines = "using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Text;\nusing System.Threading.Tasks;\n\nnamespace PenUI.Entities\n{\n\tpublic class L" + classname + "\n\t{\n" + property + "\n\t}\n}";

                //string path = @"D:\PenSystemSolution\PenUI\LEntity\L" + classname + ".cs";

                SqlCommand cmd = new SqlCommand("SetToIUEntity", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = tablename;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                string property = "";
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    property += "\t\t" + row[0] + "\n";
                }
                string lines = "using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Text;\nusing System.Threading.Tasks;\n\nnamespace PenUI.Entities\n{\n\tpublic class " + classname + "\n\t{\n" + property + "\n\t}\n}";

                string path = @"D:\PenSystemSolution\PenUI\Entity\" + classname + ".cs";



                try
                {
                    if (!File.Exists(path))
                    {
                        File.Create(path);
                        TextWriter tw = new StreamWriter(path);
                        tw.WriteLine(lines);
                        tw.Close();
                    }
                    else if (File.Exists(path))
                    {
                        TextWriter tw = new StreamWriter(path);
                        tw.WriteLine(lines);
                        tw.Close();
                    }
                }
                catch 
                {

                 
                }
            }
            

        }
    }
}
