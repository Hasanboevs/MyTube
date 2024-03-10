using My_Tube.Core.Modoles;

namespace My_Tube.Core.Dtos
{
    public class SearchResponseDto
    {
        public List<VideoResult> Videos { get; set; } = new List<VideoResult>();
        public List<PlaylistResult> Playlists { get; set; } = new List<PlaylistResult>();
        public List<ChannelResult> Channels { get; set; } = new List<ChannelResult>();
    }
}
