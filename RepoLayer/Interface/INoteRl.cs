﻿using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entity;
using System.Collections.Generic;

namespace RepoLayer.Interface
{
    public interface INoteRl
    {
        public NoteEntity AddNote(NoteModel notes, long userid);
        public bool DeleteNote(long noteid);
        public NoteEntity UpdateNotes(NoteModel notes, long Noteid);
        public NoteEntity IsPinnedORNot(long noteid);
        public NoteEntity IsArchivedORNot(long noteid);
        IEnumerable<NoteEntity> GetAllNotes();
        IEnumerable<NoteEntity> GetAllNotesbyuserid(long userid);
        public NoteEntity UploadImage(long noteid, IFormFile img);
        public NoteEntity Color(long noteid, string color);
    }
}
