using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace STMIS.Models;

[Table("MidtermMarksTable")]
public partial class MidtermMarksTable
{
    [Key]
    [Column("Marks_id")]
    public int MarksId { get; set; }

    [Column("Subject_id")]
    public int SubjectId { get; set; }

    [Column("Student_id")]
    public int StudentId { get; set; }

    [Column("Class_id")]
    public int ClassId { get; set; }

    public int? Marks { get; set; }

    [StringLength(50)]
    public string? Status { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("MidtermMarksTables")]
    public virtual ClassTable Class { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("MidtermMarksTables")]
    public virtual StudentsTable Student { get; set; } = null!;

    [ForeignKey("SubjectId")]
    [InverseProperty("MidtermMarksTables")]
    public virtual SubjectTable Subject { get; set; } = null!;
}
