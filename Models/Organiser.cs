﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _272ass.Models
{
    public class Organiser : User
    {
        public virtual ICollection<Event> EventsCreated { get; set; }
    }
}