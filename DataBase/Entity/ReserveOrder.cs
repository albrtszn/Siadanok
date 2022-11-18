using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Siadanok.Models
{
    public class ReserveOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ReserveId { get; set; }
        public string DateOfOrder { get; set; }
        public string ReserveDate { get; set; }
        public string PayMethod { get; set; }
        public string UserId { get; set; }
        public string CartId { get; set; }
        public string Table { get; set; }
    }
}
