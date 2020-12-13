using System.Collections.Generic;

namespace WinUISample.Models
{
    public class SeasonVM
    {
        public List<EpisodeVM> Episodes { get; set; }
        public string Poster { get; set; }
        public int SeasonNumber { get; set; }
    }
}
