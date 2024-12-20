using Mapster;
using Microsoft.AspNetCore.Mvc;
using Spotico.Core.Models;
using Spotico.Core.Stores;
using Spotico.Server.DTOs;

namespace Spotico.Server.Controllers;

public class PlaylistController : ControllerBase
{
    private readonly IPlaylistStore _playlistStore;
    
    public PlaylistController(IPlaylistStore playlistStore)
    {
        _playlistStore = playlistStore;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var playlists = await _playlistStore.GetAsync();
        return Ok(playlists);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var playlist = await _playlistStore.GetByIdAsync(id);
        return Ok(playlist);
    }
    
    [HttpPost]
    public async Task Post(PlaylistDTO playlistDto)
    {
        var playlist = playlistDto.Adapt<Playlist>();
        await _playlistStore.AddAsync(playlist);
    }
    
    [HttpPut]
    public async Task Put(Playlist playlist)
    {
        await _playlistStore.UpdateAsync(playlist);
    }
    
    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _playlistStore.DeleteAsync(id);
    }
}