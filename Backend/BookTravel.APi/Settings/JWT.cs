﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookTravel.APi.Settings
{
    public class JWT
    {
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int DurationOnDays { get; set; }
    }
}
