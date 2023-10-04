using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;


namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemController: ControllerBase
    {
        private static readonly List<ItemDto> items =new()
        {
            new ItemDto( Guid.NewGuid(), "Potion", "Restores a small amount of HP", 100, DateTimeOffset.UtcNow),
            new ItemDto( Guid.NewGuid(), "Antidote", "Action game", 100, DateTimeOffset.UtcNow),
            new ItemDto( Guid.NewGuid(), "Bronze Sword", "Restores a small amount of HP", 100, DateTimeOffset.UtcNow),
            new ItemDto( Guid.NewGuid(), "Zelda", "Strategy ", 100, DateTimeOffset.UtcNow),
            new ItemDto( Guid.NewGuid(), "Mario", "Action and Strategy", 100, DateTimeOffset.UtcNow),
            new ItemDto( Guid.NewGuid(), "Mine Craft", "Restores a small amount of HP", 100, DateTimeOffset.UtcNow),
            new ItemDto( Guid.NewGuid(), "Roblox", "Restores a small amount of HP", 100, DateTimeOffset.UtcNow)            
         };

         [HttpGet]
         public IEnumerable<ItemDto> Get()
         {

            return items;
            
         }

         [HttpGet ("{id}")]         
         public ItemDto GetById(Guid id)
         {
            var  item =items.Where( item => item.Id== id).SingleOrDefault();
            return item;

         }

         [HttpPost]
         public ActionResult<ItemDto> Post (CreateItemDto createItemDto)
         {
            var item =new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price,DateTimeOffset.UtcNow );
            items.Add(item);
            return CreatedAtAction(nameof(GetById), new {id= item.Id},item);

         }
       
    }

    
}

