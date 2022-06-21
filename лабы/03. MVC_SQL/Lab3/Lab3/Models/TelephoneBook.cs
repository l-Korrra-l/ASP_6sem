using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Lab2.Models
{
    [Table("PhoneBook")]
    public class TelephoneBookRow
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("surname")]
        public string Surname { get; set; }

        [Required]
        [MaxLength(13)]
        [Column("phone_number")]
        public string TelephoneNumber { get; set; }


        public TelephoneBookRow(string surname, string telephoneNumber)
        {
            Surname = surname;
            TelephoneNumber = telephoneNumber;
        }

        public TelephoneBookRow() { }
    }

    public class TelephoneBook: DbContext
    {
        public DbSet<TelephoneBookRow> Rows { get; set; }

        public TelephoneBook(): base("connection")
        {
        }

        public List<TelephoneBookRow> GetAll()
        {
            return Rows.OrderBy(tr => tr.Surname).ToList();
        }

        public void AddRow(string surname, string phoneNumber)
        {
            Rows.Add(new TelephoneBookRow(surname, phoneNumber));
            SaveChanges();
        }

        public bool Update(int id, string surname, string phoneNumber)
        {
            var changedRow = Rows.FirstOrDefault(tr => tr.Id == id);

            if(changedRow != null)
            {
                if(surname != "")
                {
                    changedRow.Surname = surname;
                }

                if(phoneNumber != "")
                {
                    changedRow.TelephoneNumber = phoneNumber;
                }

                SaveChanges();

                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            var deletedRow = Rows.FirstOrDefault(tr => tr.Id == id);
            if (deletedRow == null)
            {
                return false;
            }

            Rows.Remove(deletedRow);

            SaveChanges();
            return true;
        }
    }
}