using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepoLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        public string LabelName { get; set; }
        [ForeignKey("Users")]
        public long Userid { get; set; }
        [ForeignKey("Notes")]
        public long Noteid { get; set; }
        [JsonIgnore]
        public virtual UserEntity user { get; set; }
        [JsonIgnore]
        public virtual NoteEntity notes { get; set; }
    }
}
