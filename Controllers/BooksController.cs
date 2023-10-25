﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moscu_Diana_Stephani_Lab2.Data;
using Moscu_Diana_Stephani_Lab2.Models;

namespace Moscu_Diana_Stephani_Lab2.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(
            string sortOrder, 
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            
                ViewData["CurrentSort"] = sortOrder;
                ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : ""; 
                ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

                    //cod adaugat in Lab3
                    if (searchString != null)
                    {
                        pageNumber = 1;
                    }
                    else
                    {
                        searchString = currentFilter;
                    }

                ViewData["CurrentFilter"] = searchString;

            //var books = from b in _context.Books 
            //            select b;
            var books = _context.Books.Include(b => b.Author).Select(b=>b);

                if (!String.IsNullOrEmpty(searchString))
                {
                    books = books.Where(s => s.Title.Contains(searchString));
                }

                switch (sortOrder)
                {
                    case "title_desc":
                        books = books.OrderByDescending(b => b.Title);
                        break;
                    case "Price":
                        books = books.OrderBy(b => b.Price);
                        break;
                    case "price_desc":
                        books = books.OrderByDescending(b => b.Price);
                        break;
                    default:
                        books = books.OrderBy(b => b.Title);
                        break;
                    }
                    int pageSize = 2;
                return View(await PaginatedList<Book>.CreateAsync(books.AsNoTracking(), pageNumber ?? 1, pageSize));
            }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Am adaugat acest cod in Lab3
            var book = await _context.Books
            .Include(b => b.Author)
            .Include(s => s.Orders)
            .ThenInclude(e => e.Customer)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);
            
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "FullName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Am adaugat un block try-catch
        public async Task<IActionResult> Create([Bind("Title,AuthorID,Price")] Book book)
        {
            try
            {
                if (ModelState.IsValid) //added 
                {
                    _context.Add(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "FullName"); //added

            }
            catch (DbUpdateException /* ex*/)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists ");
            }

            return View(book);
            }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                var bookToUpdate = await _context.Books.FirstOrDefaultAsync(s => s.ID == id);
                if (await TryUpdateModelAsync<Book>
                    (bookToUpdate, 
                    "", 
                    s => s.Author, 
                    s => s.Title, 
                    s => s.Price))
                {
                    try
                    {
                        await _context.SaveChangesAsync(); return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists");
                    }
                }

            return View(bookToUpdate);
        }
        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .AsNoTracking() //cod adaugat in Lab3
                .FirstOrDefaultAsync(m => m.ID == id);
                if (book == null)
                {
                    return NotFound();
                }
                if (saveChangesError.GetValueOrDefault()) //cod adaugat in Lab3
                {
                    ViewData["ErrorMessage"] = "Delete failed. Try again";
                }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        { 
            var book = await _context.Books.FindAsync(id);
            if (book == null) //cod adaugat in Lab3
            {
                return RedirectToAction(nameof(Index));
            }
            
            try
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */) //cod adaugat in Lab3
            {
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
