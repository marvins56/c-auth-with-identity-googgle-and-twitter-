using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace STMIS.Models;

[Table("ClassTable")]
public partial class ClassTable
{
    [Key]
    [Column("Class_id")]
    public string ClassId { get; set; } = null!;

    [Column("Class_Name")]
    [StringLength(50)]
    public string ClassName { get; set; } = null!;

    [InverseProperty("Class")]
    public virtual ICollection<EndOftermMarksTable> EndOftermMarksTables { get; } = new List<EndOftermMarksTable>();

    [InverseProperty("Class")]
    public virtual ICollection<MidtermMarksTable> MidtermMarksTables { get; } = new List<MidtermMarksTable>();

    [InverseProperty("Class")]
    public virtual ICollection<StudentsTable> StudentsTables { get; } = new List<StudentsTable>();

    [InverseProperty("Class")]
    public virtual ICollection<SubTopicsTable> SubTopicsTables { get; } = new List<SubTopicsTable>();

    [InverseProperty("Class")]
    public virtual ICollection<TopicsTable> TopicsTables { get; } = new List<TopicsTable>();
}
