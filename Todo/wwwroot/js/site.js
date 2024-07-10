
//1st attempt calling the api through js
//async function callTodoApiGetUserProfile(email) {
//    const apiUrl = "https://localhost:7094/api/UserProfile?";

//    const response = await fetch(apiUrl + new URLSearchParams({ email: email }));
//    var data = await response.json();
//    console.log(data)

//    return new UserProfileModel(data.display_name)
//}

//class UserProfileModel {
//    constructor(displayName) {
//        this.display_name = displayName;
//    }
//}

//document.addEventListener("DOMContentLoaded", async function () {
//    var userProfile = await callTodoApiGetUserProfile();
//    var displayName = userProfile.display_name;
//    console.log(displayName)

//    console.log(document.getElementsByClassName("gravatarDisplayName"))

//    const anchorElements = document.querySelectorAll('a.gravatarDisplayName');

//    if (anchorElements.length > 0 && displayName) {
//        anchorElements.forEach(element => {
//            element.textContent = "- " + displayName;
//        });
//    }
   
//});