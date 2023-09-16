let registerBtn = document.getElementById('btn-register');
let fn = document.getElementById('firstName');
let ln = document.getElementById('lastName');
let username = document.getElementById('username');
let password = document.getElementById('password');
let confirmPassword = document.getElementById('confirmPassword');
let role = document.getElementById('role');


let port = "5259";


let register = async () => {
    let url = "http://localhost:"+port+"/api/User/register"

    let user = {
        FirstName: fn.value,
        LastName: ln.value,
        Username: username.value,
        Password: password.value,
        ConfirmPassword: confirmPassword.value,
        Role: role.value
    };

    let response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
    .then(function (response){
        console.log(response);
        window.location.href = "http://localhost:5259/login.html"
    })
    .catch(function (err) {
        console.log(err);
    })

}

registerBtn.addEventListener("click", register);