using Mapster;
using Microsoft.AspNetCore.Mvc;
using Spotico.Domain.Models;
using Spotico.Domain.Stores;
using Spotico.Server.DTOs;

namespace Spotico.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumController : ControllerBase
{
    private readonly IAlbumStore _albumStore;
    
    public AlbumController(IAlbumStore albumStore)
    {
        _albumStore = albumStore;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var albums = await _albumStore.GetAsync();
        return Ok(albums);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var album = await _albumStore.GetByIdAsync(id);
                    
        if (album == null) return NotFound();
        
        return Ok(album);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(AlbumDTO albumDto)
    {
        var album = albumDto.Adapt<Album>();
        await _albumStore.AddAsync(album);
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(Album album)
    {
        await _albumStore.UpdateAsync(album);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _albumStore.DeleteAsync(id);
        return Ok();
    }
}