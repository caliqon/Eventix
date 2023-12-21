using Eventix.State.Entities;
using Eventix.State.Requests;
using Eventix.State.Stores;
using Microsoft.AspNetCore.Mvc;

namespace Eventix.State.Controllers;

[ApiController]
[Route("v1/state/{provider}/{store}")]
public class StateController : ControllerBase
{
    private readonly IStateStore _stateStore;

    public StateController(IStateStore stateStore)
    {
        _stateStore = stateStore;
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> GetAsync([FromRoute] string provider, [FromRoute]string store, [FromRoute]string key)
    {
        var result = await _stateStore.GetAsync(store, key, provider);
        
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromRoute] string provider, [FromRoute]string store, [FromBody] CreateStateEntryRequest data)
    {
        var entity = new StateEntity
        {
            Data = data.Data,
            PartitionKey = store,
            TimeToLive = data.ExpiresAt ?? null,
            Id = data.Key
        };
        var result = await _stateStore.CreateAsync(store, provider, entity);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromRoute] string provider, [FromRoute]string store, [FromBody] StateEntity data)
    {
        var result = await _stateStore.UpdateAsync(store, provider, data);
        
        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
    }
    
    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] string provider, [FromRoute]string store, [FromRoute]string key)
    {
        var result = await _stateStore.DeleteAsync(store, key, provider);
        
        return result.IsSuccess ? Ok() : BadRequest(result.Errors);
    }
}