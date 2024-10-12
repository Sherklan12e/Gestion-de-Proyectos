namespace Consola;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
public class Blog
{
    public int Id {get;set;}
    public string Name {get;set;}
    public string Url {get;set;}

    public List<Blog> ObternerTodo(){
        List<Blog> blogs = new List<Blog>();
        using (var db = new BlogDbContext()){
            try{
                blogs = db.Blogs.ToList();
            }
            catch(Exception ex){
                Console.WriteLine($"Error while fetching blogs: {ex.Message}");
            }
        }
        return blogs;
    }
    public void Add
}
