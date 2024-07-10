using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListDetailViewmodelFactory
    {
        public static TodoListDetailViewmodel Create(TodoList todoList, bool sortedByRank = false)
        {
            var items = sortedByRank
                ? todoList.Items.Select(TodoItemSummaryViewmodelFactory.Create).OrderBy(o => o.Rank).ThenBy(o => o.Importance).ToList()
                : todoList.Items.Select(TodoItemSummaryViewmodelFactory.Create).OrderBy(o => o.Importance).ToList();

            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, items);
        }
    }
}