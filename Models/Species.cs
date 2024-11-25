using System;
namespace FinalWorkshop.Models
{
	public class Species
	{
		public int id { get; set; }
		public string name { get; set; }

		public ICollection<Animals> Animals = new List<Animals>();
    }
}

