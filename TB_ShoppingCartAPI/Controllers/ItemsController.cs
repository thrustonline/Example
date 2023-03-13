using Microsoft.AspNetCore.Mvc;
using TB_ShoppingCartAPI.Models;

namespace TB_ShoppingCartAPI.Controllers
{
    [ApiController]    
    public class ItemsController : ControllerBase
    {
        private InventoryContext dbContext;
        public ItemsController(InventoryContext _dbContext)
        {
            dbContext = _dbContext;
        }





        #region Items Controller methods
        [HttpGet]
        [Route("Item")]
        public ActionResult<List<Item>> GetItems()
        {
            List<Item> items = dbContext.Items.ToList();
            return Ok(items);
           
        }

        [HttpPost]
        [Route("Item")]
        public ActionResult AddItem(Item _item)
        {
            try
            {
                _item.Id = Guid.NewGuid() ;
                if(dbContext.Items.Find(_item.Id) == null)
                {
                    dbContext.Items.Add(_item);
                    dbContext.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
                
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
                      
        }

        [HttpPut]
        [Route("Item")]
        public ActionResult UpdateItem(Item _item)
        {
            try
            {
                var todo = dbContext.Items.Where(x => x.Id == _item.Id).Single();
                todo.Name = _item.Name;
                todo.Description = _item.Description;
                todo.Price = _item.Price;
                dbContext.SaveChanges();
                return Ok();
            }
            catch(InvalidOperationException _ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpDelete]
        [Route("Item")]
        public ActionResult DeleteItem(string Id)
        {
            try
            {
                var isValid = Guid.TryParse(Id, out var result);
                if (isValid)
                {
                    var todo = dbContext.Items.Where(x => x.Id == result).Single();
                    dbContext.Items.Remove(todo);
                    dbContext.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
                
            }
            catch (InvalidOperationException _ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

       
        #endregion
    }
}