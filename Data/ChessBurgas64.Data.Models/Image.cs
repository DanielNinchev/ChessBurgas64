﻿namespace ChessBurgas64.Data.Models
{
    using System;

    using ChessBurgas64.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string RemoteImageUrl { get; set; }

        public string Extension { get; set; }

        public string TrainerId { get; set; }

        public virtual Trainer Trainer { get; set; }

        public string MemberId { get; set; }

        public virtual Member Member { get; set; }

        public int? AnnouncementId { get; set; }

        public virtual Announcement Announcement { get; set; }

        public int? PuzzleId { get; set; }

        public virtual Puzzle Puzzle { get; set; }
    }
}