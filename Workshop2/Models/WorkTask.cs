using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Workshop2.Models
{
    public class WorkTask
    {
        [Key]
        public int TaskId { get; set; }

        public int CarId { get; set; }
        public Car? Car { get; set; }

        [DisplayName("Data rozpoczęcia")]
        public DateTime TaskStartDate { get; set; }

        [DisplayName("Data zakończenia")]
        public DateTime? TaskEndDate { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        [DisplayName("Opis")]
        public string? Description { get; set; }

        [DisplayName("Status")]
        public bool IsCompleted { get; set; }

        [NotMapped]
        [DisplayName("Samochód")]
        public string? CarName
        {
            get
            {
                return Car == null ? "" : Car.CarBrand + " " + Car.CarModel;
            }
        }

    }
}
