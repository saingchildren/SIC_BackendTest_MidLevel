using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercuryTest.Models
{
    public class MyOffice_ExcuteionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DeLog_AutoID { get; set; }

        [Required]
        [StringLength(120)]
        public string DeLog_Api { get; set; } = string.Empty;

        [Required]
        public Guid DeLog_GroupID { get; set; }

        public bool DeLog_isCustomDebug { get; set; }

        [Required]
        [StringLength(120)]
        public string DeLog_ExecutionProgram { get; set; } = string.Empty;

        public string? DeLog_ExecutionInfo { get; set; }

        public bool? DeLog_verifyNeeded { get; set; }

        public DateTime DeLog_ExDateTime { get; set; } = DateTime.Now;
    }
}
