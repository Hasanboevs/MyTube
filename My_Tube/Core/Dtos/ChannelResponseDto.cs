using My_Tube.Core.Modoles;

namespace My_Tube.Core.Dtos
{
    public class ChannelResponseDto
    {
        public List<VideoResult> Videos { get; set; } = new List<VideoResult>();
        public List<PlaylistResult> Playlists { get; set; } = new List<PlaylistResult>();
    }
}
