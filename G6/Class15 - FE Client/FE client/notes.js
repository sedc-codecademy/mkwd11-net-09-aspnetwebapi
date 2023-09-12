let getAllNotesBtn = document.getElementById("btn1");
let addNoteBtn = document.getElementById("btn3");
let addNoteInput1 = document.getElementById("input31");
let addNoteInput4 = document.getElementById("input44");

let getAllNotes = async() => {

    let url = "http://localhost:5288/api/notes"; 
    //read the token from local storage. The token is saved in local storage on successful login
    let token = localStorage.getItem("notesApiToken");


    await fetch(url, {
        method: "GET",
        headers: {
            "Authorization" : `Bearer ${token}`
        }
    })
    .then(function(response){
     debugger;
        response.json()
        .then(function(notes){
            debugger;
            console.log(notes);
        })
    })
    .catch(function(error){
        debugger
        console.log(error);
    })
}

getAllNotesBtn.addEventListener("click", getAllNotes);

let addNote = async() => {
    let url = "http://localhost:5288/api/notes/addNote";
    let token = localStorage.getItem("notesApiToken");0
    let note = {
        Text: addNoteInput1.value,
        UserId: parseInt(addNoteInput4.value)
    };

    await fetch(url, {
        method: 'POST',
        body: JSON.stringify(note),//json object that has the same properties as LoginUserDto on the backend side,
        headers: {
            'Content-Type' : 'application/json',
            "Authorization" : `Bearer ${token}`
        }
    })
    .then(function(response){
        debugger
        console.log("User successfully logged in.");
        console.log(response);
    })
    .catch(function(error){
        debugger
        console.log(error);
    })
}
addNoteBtn.addEventListener("click", addNote);