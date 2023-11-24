using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;


namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemController: ControllerBase
    {

        private readonly ItemsRepository itemsRepository = new();
        // private static readonly List<ItemDto> items =new()
        // {
        //     new ItemDto( Guid.NewGuid(), "Potion", "Restores a small amount of HP", 100, DateTimeOffset.UtcNow),
        //     new ItemDto( Guid.NewGuid(), "Antidote", "Action game", 100, DateTimeOffset.UtcNow),
        //     new ItemDto( Guid.NewGuid(), "Bronze Sword", "Restores a small amount of HP", 100, DateTimeOffset.UtcNow),
        //     new ItemDto( Guid.NewGuid(), "Zelda", "Strategy ", 100, DateTimeOffset.UtcNow),
        //     new ItemDto( Guid.NewGuid(), "Mario", "Action and Strategy", 100, DateTimeOffset.UtcNow),
        //     new ItemDto( Guid.NewGuid(), "Mine Craft", "Restores a small amount of HP", 100, DateTimeOffset.UtcNow),
        //     new ItemDto( Guid.NewGuid(), "Roblox", "Restores a small amount of HP", 100, DateTimeOffset.UtcNow)            
        //  };

         [HttpGet]
         public async Task< IEnumerable<ItemDto>> GetAsync()
         {
            var items = (await itemsRepository.GetAllAsync())
                        .Select(item => item.AsDto());

            return items;
            
         }

         [HttpGet ("{id}")]         
         public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
         {

            // var  item =items.Where( item => item.Id== id).SingleOrDefault();
            var item =await itemsRepository.GetAsync(id);

            if(item ==null)
            {
                return NotFound();
            }
            return item.AsDto();

         }

         [HttpPost]
         public async Task<ActionResult<ItemDto>> PostAsync (CreateItemDto createItemDto)
         {
            // var item =new ItemDto(Guid.NewGuid(), createItemDto.Name, createItemDto.Description, createItemDto.Price,DateTimeOffset.UtcNow );
            // items.Add(item);
            var item =new Item{

                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreateDate = DateTimeOffset.UtcNow
            };
            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new {id= item.Id},item);

         }
        [HttpPut ("{id}")]
        public async Task<IActionResult> PutAsync (Guid id ,UpdateItemDto updateItemDto)
        {
            var existingItem =await itemsRepository.GetAsync(id);
            if(existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = updateItemDto.Name;
            existingItem.Description = updateItemDto.Description;
            existingItem.Price = updateItemDto.Price;
            
            await itemsRepository.UpdateAsync(existingItem);
            // var existingItem = items.Where(item => item.Id == id ).SingleOrDefault();
            // if(existingItem == null)
            // {
            //     return NotFound();
            // }

            // var updatedItem = existingItem with {
            //     Name =  updateItemDto.Name,
            //     Description = updateItemDto.Description,
            //     Price = updateItemDto.Price
            // };
                
            //     var index = items.FindIndex (existingItem => existingItem.Id == id);
            //     items[index] = updatedItem;

                return NoContent();
                
        }

     [HttpDelete ("{id}")]
     public async  Task<IActionResult> DeleteAsync (Guid id)
     {
        var existingItem =await itemsRepository.GetAsync(id);
        if(existingItem == null)
        {
            return NotFound();
        }

        await itemsRepository.RemoveAsync(existingItem.Id);
        
    //  var existingItem =items.Where(item => item.Id ==id).SingleOrDefault();
    // if(existingItem == null)
    //     {
    //     return NotFound();
    //     }

    //     var index = items.FindIndex(existingItem => existingItem.Id == id);
    //     items.RemoveAt(index);

        return NoContent();
     }
     
    
      

        
    }
}

    


