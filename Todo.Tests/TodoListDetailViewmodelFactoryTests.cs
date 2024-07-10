using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models.TodoItems;
using Xunit;

namespace Todo.Tests
{
    public class TodoListDetailViewmodelFactoryTests
    {
        public TodoListDetailViewmodelFactoryTests()
        {
        }

        [Fact]
        public void Create_ItemsAreSortedByImportance()
        {
            var todoList = new TestTodoListBuilder(new IdentityUser { Email = "alice@example.com", UserName = "alice@example.com" }, "shopping")
                    .WithItem("bread", Importance.Low)
                    .WithItem("milk", Importance.High)
                    .WithItem("chocolate", Importance.Medium)
                    .Build();

            var breadLow = todoList.Items.First();
            var milkHigh = todoList.Items.Skip(1).First();  
            var chocolateMedium = todoList.Items.Last();

            var result = TodoListDetailViewmodelFactory.Create(todoList);

            //assert high importance
            Assert.Equal(milkHigh.TodoItemId, result.Items.First().TodoItemId);
            Assert.Equal(milkHigh.Importance, result.Items.First().Importance);
            Assert.Equal(milkHigh.Title, result.Items.First().Title);

            //assert medium importance
            Assert.Equal(chocolateMedium.TodoItemId, result.Items.Skip(1).First().TodoItemId);
            Assert.Equal(chocolateMedium.Importance, result.Items.Skip(1).First().Importance);
            Assert.Equal(chocolateMedium.Title, result.Items.Skip(1).First().Title);

            //assert low importance
            Assert.Equal(breadLow.TodoItemId, result.Items.Last().TodoItemId);
            Assert.Equal(breadLow.Importance, result.Items.Last().Importance);
            Assert.Equal(breadLow.Title, result.Items.Last().Title);
        }

        [Fact]
        public void Create_ItemsAreSortedByRank()
        {
            var todoList = new TestTodoListBuilder(new IdentityUser { Email = "alice@example.com", UserName = "alice@example.com" }, "shopping")
                    .WithItemAndRank("bread", 3)
                    .WithItemAndRank("milk", 1)
                    .WithItemAndRank("chocolate", 2)
                    .Build();

            var breadRank3 = todoList.Items.First();
            var milkRank1 = todoList.Items.Skip(1).First();
            var chocolateRank2 = todoList.Items.Last();

            var result = TodoListDetailViewmodelFactory.Create(todoList, true);

            //assert rank 1
            Assert.Equal(milkRank1.TodoItemId, result.Items.First().TodoItemId);
            Assert.Equal(milkRank1.Rank, result.Items.First().Rank);
            Assert.Equal(milkRank1.Title, result.Items.First().Title);

            //assert rank 2
            Assert.Equal(chocolateRank2.TodoItemId, result.Items.Skip(1).First().TodoItemId);
            Assert.Equal(chocolateRank2.Rank, result.Items.Skip(1).First().Rank);
            Assert.Equal(chocolateRank2.Title, result.Items.Skip(1).First().Title);

            //assert rank 3
            Assert.Equal(breadRank3.TodoItemId, result.Items.Last().TodoItemId);
            Assert.Equal(breadRank3.Rank, result.Items.Last().Rank);
            Assert.Equal(breadRank3.Title, result.Items.Last().Title);
        }
    }
}
