﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepoLayer.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }
        public string CollabEmail { get; set; }
        [ForeignKey("Users")]
        public long Userid { get; set; }
        [ForeignKey("NotesTable")]
        public long Noteid { get; set; }
        [JsonIgnore]
        public virtual UserEntity user { get; set; }
        [JsonIgnore]
        public virtual NoteEntity notes { get; set; }
    }
}
