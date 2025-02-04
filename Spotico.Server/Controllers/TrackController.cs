using Mapster;
using Microsoft.AspNetCore.Mvc;
using Spotico.Domain.Models;
using Spotico.Domain.Stores;
using Spotico.Infrastructure.Interfaces;
using Spotico.Server.DTOs;

namespace Spotico.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrackController : ControllerBase
{
    private readonly ITrackStore _trackStore;
    private readonly IMediaService _mediaService;
    
    public TrackController(ITrackStore trackStore, IMediaService mediaService)
    {
        _trackStore = trackStore;
        _mediaService = mediaService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tracks = await _trackStore.GetAsync();
        return Ok(tracks);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var track = await _trackStore.GetByIdAsync(id);
        return track != null ? Ok(track) : NotFound();
    }
    
    [HttpPost]
    public async Task Post(TrackDTO trackDto)
    {
        var track = trackDto.Adapt<Track>();
        // Upload the track file and get a file path
        track.TrackPath = await _mediaService.UploadTrackAsync(trackDto.File);
        
        await _trackStore.AddAsync(track);
    }
    
    [HttpPut]
    public async Task Put(Track track)
    {
        await _trackStore.UpdateAsync(track);
    }
    
    [HttpDelete("{id}")]
    public async Task Delete(Guid id)
    {
        await _trackStore.DeleteAsync(id);
    }
}