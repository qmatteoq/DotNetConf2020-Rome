using System.Collections.Generic;

namespace WinUISample.Models
{
    public class TmdbRequest
    {
        public int Id { get; set; }
        public List<TmdbVideo> Results { get; set; }
    }
}
