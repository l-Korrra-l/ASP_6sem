using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Lab2.Models
{
    public class TelephoneBookRow
    {
        public int Id { get; set; }
        public string Surname { get; set; }

        public string TelephoneNumber { get; set; }


        public TelephoneBookRow(int id, string surname, string telephoneNumber)
        {
            Id = id;
            Surname = surname;
            TelephoneNumber = telephoneNumber;
        }
    }

    public class TelephoneBook
    {
        public List<TelephoneBookRow> Rows { get; set; }

        private int id;

        public TelephoneBook()
        {
            Rows = new List<TelephoneBookRow>();

            Initialize();
        }

        private void Initialize()
        {

            string json = File.ReadAllText(@"C:\Users\User\Documents\Study_6sem\ПИС\Labs\Lab2\Lab2\App_Data\telephoneBook.json", Encoding.Default);


            var bookRows = JsonConvert.DeserializeObject<List<TelephoneBookRow>>(json);

            if (bookRows.Count != 0)
            {
                Rows.AddRange(bookRows);
                id = Rows.Max<TelephoneBookRow>(el => el.Id);
            }
        }

        public List<TelephoneBookRow> GetAll()
        {
            return Rows.OrderBy(tr => tr.Surname).ToList();
        }

        public void AddRow(string surname, string phoneNumber)
        {
            Rows.Add( new TelephoneBookRow(++id, surname, phoneNumber));
            SaveInFile();
        }

        public bool Update(int id, string surname, string phoneNumber)
        {
            for(int i = 0; i < Rows.Count; i++)
            {
                if(Rows[i].Id == id)
                {
                    if(surname != "")
                    {
                        Rows[i].Surname = surname;
                    }

                    if(phoneNumber != "")
                    {
                        Rows[i].TelephoneNumber = phoneNumber;
                    }

                    SaveInFile();
                    return true;
                }
            }

            return false;
        }

        public bool Delete(int id)
        {
            var deletedRow = Rows.Find(row => row.Id == id);
            if(deletedRow == null)
            {
                return false;
            }

            Rows.Remove(deletedRow);

            SaveInFile();

            return true;
        }

        private void SaveInFile()
        {
            string json = JsonConvert.SerializeObject(Rows);

            File.WriteAllText(@"C:\Users\User\Documents\Study_6sem\ПИС\Labs\Lab2\Lab2\App_Data\telephoneBook.json", json, Encoding.Default);
        }
    }
}