using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Todo.Data.Entities;

namespace Todo.Tests
{
    /*
     * This class makes it easier for tests to create new TodoLists with TodoItems correctly hooked up
     */
    public class TestTodoListBuilder
    {
        private readonly string title;
        private readonly IdentityUser owner;
        private readonly List<(string, Importance)> items = new();
        private readonly List<(string, int)> rankedItems = new();

        public TestTodoListBuilder(IdentityUser owner, string title)
        {
            this.title = title;
            this.owner = owner;
        }

        public TestTodoListBuilder WithItem(string itemTitle, Importance importance)
        {
            items.Add((itemTitle, importance));
            return this;
        }

        public TestTodoListBuilder WithItemAndRank(string itemTitle, int rank)
        {
            rankedItems.Add((itemTitle, rank));
            return this;
        }

        public TodoList Build()
        {
            var todoList = new TodoList(owner, title);
            var todoItems = items.Select(itm => new TodoItem(todoList.TodoListId, owner.Id, itm.Item1, itm.Item2, owner)).ToList();
            todoItems.AddRange(rankedItems.Select(itm => new TodoItem(todoList.TodoListId, owner.Id, itm.Item1, Importance.Medium, owner, itm.Item2)));
            todoItems.ToList().ForEach(tlItm =>
            {
                todoList.Items.Add(tlItm);
                tlItm.TodoList = todoList;
            });
            return todoList;
        }
    }
}