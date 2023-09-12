let usernameInput = document.getElementById("username");
let passInput = document.getElementById("pass");
let loginBtn = document.getElementById("loginBtn");

let login = async() =>{
    let user = {
        Username : usernameInput.value,
        Password : passInput.value
    };

    let response = await fetch("http://localhost:64006/api/users/login", {
        method : 'POST',
        headers : {
            'Content-type' : 'application/json'
        },
        body : JSON.stringify(user)
    })
    .then(function(response){
        console.log(response);
        response.text()
        .then(function(text){

            localStorage.setItem("notesApiToken", text);
            debugger
            window.location.href = "http://localhost:64006/notes.html"
        })
    })
    .catch(function(err){
        console.log(err);
    })
}

loginBtn.addEventListener("click", login);