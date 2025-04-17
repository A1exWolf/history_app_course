using HistoryBackend.Data;
using HistoryBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HistoryBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;
    
    [HttpGet] // api/TodoItems Получить все элементы
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
    {
        return await _context.TodoItems.ToListAsync();
    }

    [HttpGet("{id}")] // api/TodoItems/{id} Получить элемент по id
    public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);

        if (todoItem == null)
        {
            return NotFound();
        }

        return todoItem;
    }
    
    [HttpPost] // api/TodoItems Добавить элемент
    public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
    {
        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
    }
    
    [HttpPut("{id}")] // api/TodoItems/{id} Обновить элемент
    public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
    {
        if (id != todoItem.Id)
        {
            return BadRequest();
        }
        _context.Entry(todoItem).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpDelete("{id}")] // api/TodoItems/{id} Удалить элемент
    public async Task<IActionResult> DeleteTodoItem(long id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }
        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}