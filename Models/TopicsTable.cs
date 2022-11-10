using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace STMIS.Models;

[Table("TopicsTable")]
public partial class TopicsTable
{
    [Key]
    [Column("Topic_id")]
    public string TopicId { get; set; } = null!;

    [Column("Topic_Name")]
    [StringLength(50)]
    public string? TopicName { get; set; }

    [Column("Class_id")]
    [StringLength(450)]
    public string ClassId { get; set; } = null!;

    public bool? IsComplete { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateTime { get; set; }

    [Column("Subject_id")]
    [StringLength(450)]
    public string SubjectId { get; set; } = null!;

    [ForeignKey("ClassId")]
    [InverseProperty("TopicsTables")]
    public virtual ClassTable Class { get; set; } = null!;

    [InverseProperty("Topic")]
    public virtual ICollection<SubTopicsTable> SubTopicsTables { get; } = new List<SubTopicsTable>();

    [ForeignKey("SubjectId")]
    [InverseProperty("TopicsTables")]
    public virtual SubjectTable Subject { get; set; } = null!;
}
