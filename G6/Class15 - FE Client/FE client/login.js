let usernameInput = document.getElementById("username");
let passInput = document.getElementById("pass");
let loginBtn = document.getElementById("loginBtn");



let login = async() => {

let url = "http://localhost:5288/api/Users/login";   
let loginUser = {
    Username: usernameInput.value,
    Password: passInput.value
};

await fetch(url, {
    method: 'POST',
    body: JSON.stringify(loginUser),//json object that has the same properties as LoginUserDto on the backend side,
    headers: {
        'Content-Type' : 'application/json'
    }
})
.then(function(response){
    debugger
    console.log("User successfully logged in.");
    console.log(response);

    response.text()
    .then(function(token){
        localStorage.setItem("notesApiToken", token);
        window.location.href = "file:///C:/Tanja/Academy/API/Class15/Code/FE%20client/notes.html";
    })
})
.catch(function(error){
    debugger
    console.log(error);
})

}


loginBtn.addEventListener("click", login);