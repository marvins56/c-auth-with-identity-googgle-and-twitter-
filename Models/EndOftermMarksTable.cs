using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace STMIS.Models;

[Table("EndOftermMarksTable")]
public partial class EndOftermMarksTable
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
    [InverseProperty("EndOftermMarksTables")]
    public virtual ClassTable Class { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("EndOftermMarksTables")]
    public virtual StudentsTable Student { get; set; } = null!;

    [ForeignKey("SubjectId")]
    [InverseProperty("EndOftermMarksTables")]
    public virtual SubjectTable Subject { get; set; } = null!;
}
