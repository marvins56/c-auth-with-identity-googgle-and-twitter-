using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace STMIS.Models;

[Table("StudentsTable")]
public partial class StudentsTable
{
    [Key]
    [Column("Student_id")]
    public int StudentId { get; set; }

    [Column("Student_Name")]
    [StringLength(50)]
    public string StudentName { get; set; } = null!;

    [Column("Class_id")]
    public int ClassId { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("StudentsTables")]
    public virtual ClassTable Class { get; set; } = null!;

    [InverseProperty("Student")]
    public virtual ICollection<EndOftermMarksTable> EndOftermMarksTables { get; } = new List<EndOftermMarksTable>();

    [InverseProperty("Student")]
    public virtual ICollection<MidtermMarksTable> MidtermMarksTables { get; } = new List<MidtermMarksTable>();
}
