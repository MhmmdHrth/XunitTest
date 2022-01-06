using Basics.Units;
using System.Linq;
using Xunit;

namespace XunitTest
{
    public class TodoListTests
    {
        [Fact]
        public void Add_SavesTodoItem()
        {
            //arrange
            TodoList listClass = new TodoList();

            //act
            listClass.Add(new("Test Content"));

            //assert
            var savedItem = Assert.Single(listClass.All);
            Assert.NotNull(savedItem);
            Assert.Equal(1, savedItem.Id);
            Assert.Equal("Test Content", savedItem.Content);
            Assert.False(savedItem.Complete);
        }

        [Fact]
        public void TodoItemIdIncreamnetsEverTimeWeAdd()
        {
            //arrange
            var listClass = new TodoList();

            //act
            listClass.Add(new("Test 1"));
            listClass.Add(new("Test 2"));

            //assert
            var items = listClass.All.ToArray();
            Assert.Equal(1, items[0].Id);
            Assert.Equal(2, items[1].Id);
        }

        [Fact]
        public void Complete_SetsTodoItemCompleteFlagToTrue()
        {
            //arrange
            var listClass = new TodoList();
            listClass.Add(new("Test 1"));

            //act
            listClass.Complete(1);

            //assert
            var completedItem = Assert.Single(listClass.All);
            Assert.NotNull(completedItem);
            Assert.Equal(1, completedItem.Id);
            Assert.True(completedItem.Complete);

        }
    }
}
