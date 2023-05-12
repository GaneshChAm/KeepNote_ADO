using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;

namespace KeepNote_ADO
{
    public class NotesData
    {
        SqlConnection con = new SqlConnection("Server=US-CJB79S3; database=TestDB; Integrated Security=true");
        public void AddNote()
        {
            Console.WriteLine("Enter the Following Note Details");

            Console.WriteLine("Enter the Note Title");
            string title = Console.ReadLine();

            Console.WriteLine("Enter the Note Description");
            string des = Console.ReadLine();

            Console.WriteLine("Enter the Note Date (DD/MM/YYYY)");
            string Date = Console.ReadLine();

            SqlDataAdapter adp1 = new SqlDataAdapter("Select * From NotesApp", con);          
            DataSet ds = new DataSet();
            adp1.Fill(ds, "NotesApp");

            DataRow newRow = ds.Tables["NotesApp"].NewRow();
            newRow["Note_Title"] = title;
            newRow["Note_Description"] = des;
            newRow["Note_Date"] = Date;            
            ds.Tables["NotesApp"].Rows.Add(newRow);     
            
            SqlCommandBuilder builder = new SqlCommandBuilder(adp1);            
            adp1.Update(ds, "NotesApp");

            Console.WriteLine("Note added successfully.");        
        }
        public void GetNote() 
        {
            Console.WriteLine("Enter the NoteId to view:");
            int noteid =Convert.ToInt32(Console.ReadLine());

            SqlDataAdapter adp2 = new SqlDataAdapter($"Select * From NotesApp Where Note_Id ={noteid}", con);
            DataSet ds = new DataSet();
            adp2.Fill(ds, "Notes");

            if (ds.Tables["Notes"].Rows.Count > 0)
            {
                DataRow row = ds.Tables["Notes"].Rows[0];
                Console.WriteLine("Note Title: {0}", row["Note_Title"]);
                Console.WriteLine("Note Description: {0}", row["Note_Description"]);
                Console.WriteLine("Note Date: {0}", row["Note_Date"]);
            }
            else
            {
                Console.WriteLine("Note with ID " + noteid + " not found");
            }
        }
        public void GetAllNotes()
        {
            SqlDataAdapter adp3 = new SqlDataAdapter("Select * From NotesApp", con);
            DataSet ds = new DataSet();
            adp3.Fill(ds);

            Console.WriteLine("ID\tTitle\t\tDescription\t\t\t\tDate");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables[0].Rows[i][j]}\t");
                }
                Console.WriteLine();
            }
        }
        public void UpdateNote()
        {
            Console.WriteLine("Enter the NoteID to Update");
            int noteID = Convert.ToInt32(Console.ReadLine());

            SqlDataAdapter adp4 = new SqlDataAdapter($"Select * from NotesApp where Note_ID={noteID}", con);
            DataSet ds = new DataSet();
            adp4.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];

                Console.WriteLine("Enter the New Note Title");
                string title = Console.ReadLine();

                Console.WriteLine("Enter the New Note Description");
                string des = Console.ReadLine();

                Console.WriteLine("Enter the New Note Date");
                string Date = Console.ReadLine();

                row["Note_Title"] = title;
                row["Note_Description"] = des;
                row["Note_Date"] = Date;

                SqlCommandBuilder builder = new SqlCommandBuilder(adp4);
                adp4.Update(ds);

                Console.WriteLine("Note Updated Successfully");
            }
            else
            {
                Console.WriteLine("No note found with the given Note ID.");
            }
        }      
        public void DeleteNote()
        {
            Console.WriteLine("Enter the Note ID to Delete");
            int noteId = Convert.ToInt32(Console.ReadLine());

            SqlDataAdapter adp5 = new SqlDataAdapter($"SELECT * FROM NotesApp WHERE Note_ID = {noteId}", con);
            DataSet ds = new DataSet();
            adp5.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                row.Delete();

                SqlCommandBuilder builder = new SqlCommandBuilder(adp5);
                adp5.Update(ds);

                Console.WriteLine("Note with ID " + noteId + " deleted successfully");
            }
            else
            {
                Console.WriteLine("Note with ID "+ noteId  + " not found");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            NotesData n = new NotesData();
            string ret = "y";
            do
            {
                try
                {
                    Console.WriteLine("-*-*-*- Welcome to KEEP NOTE APP -*-*-*-");
                    Console.WriteLine("1. Create Note");
                    Console.WriteLine("2. View Note");
                    Console.WriteLine("3. ViewAllNotes");
                    Console.WriteLine("4. Update Note");
                    Console.WriteLine("5. Delete Note");

                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            {
                                n.AddNote();
                                break;                                
                            }
                        case 2:
                            {
                                n.GetNote();
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("All the Notes Avaiable are given below");
                                n.GetAllNotes();
                                break;
                            }
                        case 4:
                            {
                                n.UpdateNote();
                                break;
                            }
                        case 5:
                            {
                                n.DeleteNote();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Wrong Choice Entered");
                                break;
                            }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Choosing Options  Will be only in Numbers");
                }
                Console.WriteLine("Do you wish to continue? [y/n] ");
                ret = Console.ReadLine();
            } while (ret.ToLower() == "y");
        }
    }
}