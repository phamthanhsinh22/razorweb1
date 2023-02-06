using System;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using razorweb.models;

#nullable disable

namespace razorweb.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.Id);
                });
                //Insert Data
                //Fake Data
                Randomizer.Seed = new Random(8675309);
                //khởi tạo những quy luật cho models
                var fakeArticle = new Faker<Article>();

                //thiết lập luật cho đối tượng Title là một đoạn văn từ 5 đến 10 từ
                fakeArticle.RuleFor(a => a.Title, f => f.Lorem.Sentence(5,5));
                //thiết lập quy luật cho Created là một ngày tháng trong 1 khoảng nào đó
                fakeArticle.RuleFor(a=> a.Created, f => f.Date.Between(new DateTime(2023,2,4),new DateTime(2023,3,4)));
                //thiết lập quy luật cho Content là một nôi dung văn bản có chứa nhiều đoạn văn
                fakeArticle.RuleFor(a=> a.Content,f =>f.Lorem.Paragraphs(1,4));

                for (int i = 0; i < 150; i++)
                {
                    //phát sinh ra Article ngẫu nhiên
                    Article article = fakeArticle.Generate();

                    migrationBuilder.InsertData(
                        table : "articles",
                        columns : new[] {"Title", "Created", "Content"},
                        values :  new object[]{
                            article.Title,
                            article.Created,
                            article.Content
                        }
                    );
                }
                
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles");
        }
    }
}
