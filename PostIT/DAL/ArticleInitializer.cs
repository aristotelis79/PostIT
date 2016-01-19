using PostIT.Models;
using System;
using System.Collections.Generic;

namespace PostIT.DAL
{
    public class ArticleInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ArcticleContext>
    {
        protected override void Seed(ArcticleContext context)
        {
            var tags = new List<Tag>
            {
                new Tag { Name = "Politics" },
                new Tag { Name = "Sports" },
                new Tag { Name = "Technology" },
                new Tag { Name = "Weather" },
                new Tag { Name = "Traffic & Roads" },
                new Tag { Name = "Breaking News" },
            };
            tags.ForEach(s => context.Tags.Add(s));
            context.SaveChanges();

            var articles = new List<Article>
            {
                new Article {Title = "Title 1",Content = "Content 1________", ShortDescription = "Short Description 1", Created = DateTime.Parse("20-01-2016"), Modified = DateTime.Parse("20-01-2016"), Published = DateTime.Parse("20-01-2016"), Category = tags.Single(i => i.Name == "Politics").ID },
                new Article {Title = "Title 2",Content = "Content 2________", ShortDescription = "Short Description 2", Created = DateTime.Parse("20-01-2016"), Modified = DateTime.Parse("20-01-2016"), Published = DateTime.Parse("20-01-2016"), Category = tags.Single(i => i.Name == "Sports").ID },
            };
            articles.ForEach(s => context.Articles.Add(s));
            context.SaveChanges();
        }
    }
}