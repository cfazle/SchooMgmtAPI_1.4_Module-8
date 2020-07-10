using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Submission
    {
        [Column("SubmissionId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Submission title  is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the submission text is 30 characters.")]
        public String SubmissionTitle { get; set; }

        public int Score { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime UpdatedDate { get; set; }

        [ForeignKey(nameof(Assignment))]
        public Guid AssignmentId { get; set; }

       public ICollection<Assignment> Assignments { get; set; }
    }
}
