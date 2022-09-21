using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _272ass.Models
{
    public class EventType
    {
    
    public int ID{ get; set; }
    public string Title { get; set; }
    public virtual ICollection<Event> Event { get; set; }
    }
}