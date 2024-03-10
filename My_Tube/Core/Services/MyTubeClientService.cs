using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Options;
using My_Tube.Core.Dtos;
using My_Tube.Core.IServices;
using My_Tube.Core.Modoles;

namespace My_Tube.Core.Services
{
    public class MyTubeClientService : IMyTubeClientService
    {

        #region DI & Ctor

        private readonly YouTubeService _service;

        public MyTubeClientService(IOptions<MyTubeKey> Keys)
        {
            _service = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = Keys.Value.Apikey,
                ApplicationName = Keys.Value.ApplicationName
            });
        }

        #endregion

        public async Task<SearchResponseDto> SearchAsync(string q, int MaxResult)
        {
            var searchRequest = _service.Search.List("snippet");
            searchRequest.Q = q;
            searchRequest.MaxResults = MaxResult;


            var searchResponse = await searchRequest.ExecuteAsync();


            SearchResponseDto searchResponseDto = new SearchResponseDto();

            foreach (var item in searchResponse.Items)
            {
                switch (item.Id.Kind)
                {
                    case "youtube#video":
                       searchResponseDto.Videos.Add(new VideoResult()
                       {
                           Id = item.Id.VideoId,
                           Thumbnail = item.Snippet.Thumbnails.Medium.Url,
                           Title = item.Snippet.Title,
                           Url = $"https://www.youtube.com/watch?v={item.Id.VideoId}",
                           PublishTime = item.Snippet.PublishedAtDateTimeOffset
                       });
                    break;
                    case "youtube#channel":
                        searchResponseDto.Channels.Add(new ChannelResult()
                        {
                            Id = item.Id.ChannelId,
                            Title = item.Snippet.Title,
                            Url = $"https://www.youtube.com/channel/{item.Id.ChannelId}"

                        });

                    break;
                    case "youtube#playlist":
                        searchResponseDto.Playlists.Add(new PlaylistResult()
                        {
                            Id = item.Id.PlaylistId,
                            Thumbnail = item.Snippet.Thumbnails.Medium.Url,
                            Title = item.Snippet.Title,
                            Url = $"https://www.youtube.com/playlist?list={item.Id.PlaylistId}",
                        });
                        break;
                }
                    
            }

            return searchResponseDto;
        }

        public async Task<ChannelResponseDto> SearchChannelAsync(string ChannelName, int MaxResult)
        {
            var searchRequest = _service.Search.List("snippet");
            searchRequest.ChannelId = ChannelName;
            searchRequest.MaxResults = MaxResult;
            searchRequest.Order = SearchResource.ListRequest.OrderEnum.Date;


            var searchResponse = await searchRequest.ExecuteAsync();


            ChannelResponseDto channelResponseDto = new ChannelResponseDto();

            foreach (var item in searchResponse.Items)
            {
                switch (item.Id.Kind)
                {
                    case "youtube#video":
                        channelResponseDto.Videos.Add(new VideoResult()
                        {
                            Id = item.Id.VideoId,
                            Thumbnail = item.Snippet.Thumbnails.Medium.Url,
                            Title = item.Snippet.Title,
                            Url = $"https://www.youtube.com/watch?v={item.Id.VideoId}",
                            PublishTime = item.Snippet.PublishedAtDateTimeOffset
                        });
                        break;
                    case "youtube#playlist":
                        channelResponseDto.Playlists.Add(new PlaylistResult()
                        {
                            Id = item.Id.PlaylistId,
                            Thumbnail = item.Snippet.Thumbnails.Medium.Url,
                            Title = item.Snippet.Title,
                            Url = $"https://www.youtube.com/playlist?list={item.Id.PlaylistId}",
                        });
                        break;
                }

            }

            return channelResponseDto;
        }
    }
}
