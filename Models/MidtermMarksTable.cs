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
    public string MarksId { get; set; } = null!;

    [Column("Subject_id")]
    [StringLength(450)]
    public string SubjectId { get; set; } = null!;

    [Column("Student_id")]
    [StringLength(450)]
    public string StudentId { get; set; } = null!;

    [Column("Class_id")]
    [StringLength(450)]
    public string ClassId { get; set; } = null!;

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
