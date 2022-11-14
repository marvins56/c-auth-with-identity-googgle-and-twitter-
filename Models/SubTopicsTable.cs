using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace STMIS.Models;

[Table("SubTopicsTable")]
public partial class SubTopicsTable
{
    [Key]
    [Column("SubTopics_id")]
    public int SubTopicsId { get; set; }

    [Column("Topic_id")]
    public int TopicId { get; set; }

    [StringLength(500)]
    public string SubTopic { get; set; } = null!;

    public string Overview { get; set; } = null!;

    public bool? IsComplete { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Datetime { get; set; }

    [Column("Class_id")]
    public int ClassId { get; set; }

    [ForeignKey("ClassId")]
    [InverseProperty("SubTopicsTables")]
    public virtual ClassTable Class { get; set; } = null!;

    [ForeignKey("TopicId")]
    [InverseProperty("SubTopicsTables")]
    public virtual TopicsTable Topic { get; set; } = null!;
}
