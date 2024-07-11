# Aron Ridgway - StoryTeller
# Time Taken - 4 hours 45 minutes

## Information
- Apologies for not creating a branch off master 😃
- When running the Todo.Api this command will start up the service on the correct port `dotnet run --launch-profile "https"` 

## 2. Ordering by Importance
- Made the change in `TodoListDetailViewmodelFactory.cs` as i wanted to make this the default sort.
- Added in a simple unit test to validate the logic.

## 3. Fix Tests
- Issue was with `TodoItemEditFields.cs` having a hard coded value set in the constructor.

## 4. Friendly Text
- Updated both `Create.cshtml` and `Edit.cshtml` labels to a friendly name
### Post Thoughts
This could of been done using an attribute on the model

## 5. Hide Done Items
- I opted to make this a server side action, using a boolean to filter the list in the controller.
### Post Thoughts
- This would probably be better suited as a js request, instead of a full page load.
- Perfomance gain would be to filter the list when we get it out of the DB, instead of filtering a full list
- Could of been done client side, without a call to the server.

## 6. ToLists marked responsible for
- Change made to `ApplicationDbContextConvenience.cs` got two lists **owners** and **responsibleFor** and unioned them together.
- responsible for where all lists where i had at least 1 item and i was not the owner of.
### Post Thoughts
Unit tests to back this logic up

## 7. Add Rank on Edit, and new sort by Rank
- Added a new int Rank property which i made have a min value of 1. (made more sense than starting from 0)
- Created some tests to validate the logic, modified the TestTodoListBuilder with a new WithItemAndRank
### Post Thoughts
- Again full page reload for the sort isnt great, done with js in task 10.

## 8. Gravatar api
- Decided to create a new `Todo.Api` project to keep a separation of concerns.
- I opted for the Gravatar Rest API method, (api key is in appsettings) this should be in secrets and not in source control.
- Implemented a simple polly policy incase the gravatar api was down this made sense because the Gravatar api is a 3rd party and not something we can control/fix.
### Post Thoughts
With the limited time it may not of been the best decision, but glad i tried this approach.
- Page loads are very slow when the Todo.Api is not running, which would be something i would address with more time.
- Faced a few performance issue and bugs, when our API was down for example.
- Had a few commits after that addressed a few issues
- Some parts are very wasteful, i would of explored a better caching strategy with more time.

## 9. Adding Items
- added a new form on screen to create an item on the List detail screen.
- created a seperate javascript file, using jquery iy triggered on the new form submit. And calls the Create controller with the model and reloads the page.
### Post Thoughts
- could be more performant without a full page reload
- UI could do with some re formatting

## 10. JS functionality by rank
- Created a data attribute on the li element
- Added a button to sort by rank asc
- javascript event that gets the list, performs a sort by the rank value. clears the list and recreates with the new order.
### Post Thoughts
- Only sorted the list by ascending, would be better to be able to sort by asc and desc.
- In a real application i would not have both a server side and client side sort for the same property, but was good to see the performance enhancement using client side
