using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace STMIS.Models;

[Table("WeeksTable")]
public partial class WeeksTable
{
    [Key]
    [Column("Week_id")]
    public string WeekId { get; set; } = null!;

    [Column("Week_Name")]
    [StringLength(50)]
    public string WeekName { get; set; } = null!;
}
