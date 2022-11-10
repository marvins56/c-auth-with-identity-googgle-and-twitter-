using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace STMIS.Models;

[Table("SubjectTable")]
public partial class SubjectTable
{
    [Key]
    [Column("Subject_id")]
    public string SubjectId { get; set; } = null!;

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [InverseProperty("Subject")]
    public virtual ICollection<EndOftermMarksTable> EndOftermMarksTables { get; } = new List<EndOftermMarksTable>();

    [InverseProperty("Subject")]
    public virtual ICollection<MidtermMarksTable> MidtermMarksTables { get; } = new List<MidtermMarksTable>();

    [InverseProperty("Subject")]
    public virtual ICollection<TopicsTable> TopicsTables { get; } = new List<TopicsTable>();
}
