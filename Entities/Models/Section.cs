using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
   public  class Section
    {
        [Column("SectionId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Setion Type is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Section Type is 30 characters.")]
        public string Type { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime UpdatedDate { get; set; }

        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }

        //  public Organization Organization { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
