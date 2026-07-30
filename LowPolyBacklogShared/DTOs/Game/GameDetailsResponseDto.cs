using LowPolyBacklogShared.DTOs.Backlog;

namespace LowPolyBacklogShared.DTOs.Game
{
    public class GameDetailsResponseDto : GameResponseDto
    { 
        public BacklogResponseDto? BacklogInfo { get; set; }
    }
}
