let username = document.getElementById('username');
let password = document.getElementById('password');
let loginBtn = document.getElementById('btn-login')

let port = "5259";

let login = async()=>{
    let user = {
        Username: username.value,
        Password: password.value
    };

    let url = "http://localhost:"+port+"/api/User/login"

    let response = await fetch(url,{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
    .then(function(response) {
        console.log(response);
        response.text()
        .then(function(text){
            localStorage.setItem('noteApiToken', text);
            window.location.href = "http://localhost:5259/notes.html"
        })
    })
}

loginBtn.addEventListener("click",login);