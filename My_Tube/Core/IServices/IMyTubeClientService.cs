using My_Tube.Core.Dtos;

namespace My_Tube.Core.IServices
{
    public interface IMyTubeClientService
    {
        Task<SearchResponseDto> SearchAsync(string q, int MaxResult);
        Task<ChannelResponseDto> SearchChannelAsync(string ChannelName, int MaxResult);
    }
}
