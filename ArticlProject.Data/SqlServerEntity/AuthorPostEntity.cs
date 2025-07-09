using ArticlProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArticlProject.Data.SqlServerEntity
{
    public class AuthorPostEntity : IDataHelper<AuthorPost>
    {
        private DBContext db;
        private AuthorPost _table;

        public AuthorPostEntity()
        {
            db = new DBContext();
        }
        public int Add(AuthorPost table)
        {
            if (db.Database.CanConnect())
            {
                db.AuthorPost.Add(table);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Delete(int Id)
        {
            if (db.Database.CanConnect())
            {
                _table = Find(Id);
                db.AuthorPost.Remove(_table);
                db.SaveChanges();

                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int Edit(int Id, AuthorPost table)
        {
            if (db.Database.CanConnect())
            {
                db=new DBContext();
                
                db.AuthorPost.Update(table);
                db.SaveChanges();

                return 1;
            }
            else
            {
                return 0;
            }
        }

        public AuthorPost Find(int Id)
        {
            if (db.Database.CanConnect())
            {
               

               
                return db.AuthorPost.Where(x=>x.Id==Id).First();
            }
            else
            {
                return null;
            }
        }

        public List<AuthorPost> GetAllData()
        {
            if (db.Database.CanConnect())
            {



                return db.AuthorPost.ToList();
            }
            else
            {
                return null;
            }
        }

        public List<AuthorPost> GetDataByUser(string UserId)
        {
            if (db.Database.CanConnect())
            {

                return db.AuthorPost.Where(x=>x.UserId==UserId).ToList();
            }
            else
            {
                return null;
            }
        }

        public List<AuthorPost> Serch(string SearchItem)
        {
            if (db.Database.CanConnect())
            {

                return db.AuthorPost.Where(x=>
                x.FullName.Contains(SearchItem)||
                x.UserName.Contains(SearchItem) ||
                x.UserId.Contains(SearchItem) ||
                x.PostTitle.Contains(SearchItem) ||
                x.PostDescription.Contains(SearchItem) ||
                x.PostCategory.Contains(SearchItem) ||
                x.PostImageUrl.Contains(SearchItem) ||
                x.AuthorId.ToString().Contains(SearchItem) ||
                x.CatrgoryId.ToString().Contains(SearchItem) ||
                x.AddedTime.ToString().Contains(SearchItem) ||
                x.Id.ToString().Contains(SearchItem)).ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
