let regBtn = document.getElementById("regBtn");
let fn = document.getElementById("firstName");
let ln = document.getElementById("lastName");
let username = document.getElementById("username");
let pass = document.getElementById("pass");
let confPass = document.getElementById("confPass");
let role = document.getElementById("role");


let register = async() => {

let url = "http://localhost:5288/api/Users/register";
//js object
let user = {
    FirstName : fn.value,
    LastName: ln.value,
    Username: username.value,
    Password: pass.value,
    ConfirmedPassword: confPass.value,
    Role: role.value
};

await fetch(url, {
    method: 'POST',
    body: JSON.stringify(user),//json object that has the same properties as RegisterUserDto on the backend side,
    headers: {
        'Content-Type' : 'application/json'
    }
})
.then(function(response){
    console.log("User successfully registered.");
    console.log(response);
    debugger;
    //put the url to your login.html (open it in browser and copy it)
    window.location.href = "file:///C:/Tanja/Academy/API/Class15/Code/FE%20client/login.html";
})
.catch(function(error){
    console.log(error);
})

}

regBtn.addEventListener("click", register);