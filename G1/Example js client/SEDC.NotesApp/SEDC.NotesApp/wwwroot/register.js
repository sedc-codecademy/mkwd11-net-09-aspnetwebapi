let regBtn = document.getElementById("regBtn");
let fn = document.getElementById("firstName");
let ln = document.getElementById("lastName");
let username = document.getElementById("username");
let pass = document.getElementById("pass");
let confPass = document.getElementById("confPass");
let role = document.getElementById("role");

let port = "64006";

let register = async() => {

    let url = "http://localhost:"+port+"/api/users/register";
    let user = {
        Username : username.value,
        FirstName: fn.value,
        LastName : ln.value,
        Password: pass.value,
        ConfirmedPassword : confPass.value,
        Role : role.value
    };
    
    let response = await fetch(url, {
        method : 'POST',
        headers : {
            'Content-Type' : 'application/json'
        },
        body : JSON.stringify(user)
    })
    .then(function(response){
        console.log(response);
        window.location.href = "http://localhost:64006/login.html"
    })
    .catch(function(error) {
        console.log(error);
    });
}
regBtn.addEventListener("click", register);