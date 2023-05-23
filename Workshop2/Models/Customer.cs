using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Workshop2.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Imie")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Nazwisko")]
        public string? Surename { get; set; }

        [Column(TypeName = "nvarchar(9)")]
        [DisplayName("Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        [DisplayName("Opis")]
        public string? Description { get; set; }

        [NotMapped]
        [DisplayName("Klient")]
        public string? CustomerFullName
        {
            get
            {
                return Name + " " + Surename + " " + PhoneNumber;
            }
        }
    }
}
