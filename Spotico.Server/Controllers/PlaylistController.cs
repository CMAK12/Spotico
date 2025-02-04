using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotico.Domain.Models;
using Spotico.Domain.Stores;
using Spotico.Server.DTOs;

namespace Spotico.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        return playlist != null ? Ok(playlist) : NotFound();
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