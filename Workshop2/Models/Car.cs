using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Workshop2.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Marka")]
        public string CarBrand { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Model")]
        public string CarModel { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Numer VIN")]
        public string CarVIN { get; set; }


        [Column(TypeName = "nvarchar(2000)")]
        [DisplayName("Opis")]
        public string? CarDescription { get; set; }

        [DisplayName("Status")]
        public bool IsActual { get; set; }


        [NotMapped]
        [DisplayName("Właściciel")]
        public string? CustomerName
        {
            get
            {
                return Customer == null ? "" : Customer.Name + " " + Customer.Surename;
            }
        }

        [NotMapped]
        [DisplayName("Samochód")]
        public string? CarFullName
        {
            get
            {
                return CarBrand + " " + CarModel;
            }
        }

    }
}
