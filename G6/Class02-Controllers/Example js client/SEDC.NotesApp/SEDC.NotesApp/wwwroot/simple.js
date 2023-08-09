let getAllBtn = document.getElementById("btn1");
let getByIdBtn = document.getElementById("btn2");
let addNoteBtn = document.getElementById("btn3");
let getAllTagsBtn = document.getElementById("btn4");
let getTagByIdBtn = document.getElementById("btn5");
let getByIdInput = document.getElementById("input2");
let addNoteInput = document.getElementById("input3");

//change the port from your api
let port = "5256";
let getAllNotes = async () => {
    let url = "http://localhost:" + port + "/api/notes";

    let response = await fetch(url);
    console.log(response);
    debugger;
    let data = await response.json();
    console.log(data);
};

let getNoteById = async () => {
    let url = "http://localhost:" + port + "/api/notes/" + getByIdInput.value;
    debugger
    let response = await fetch(url);
    let data = await response.text();
    console.log(data);
};


let addNote = async () => {
    let url = "http://localhost:" + port + "/api/notes";
   var response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'text/plain'
        },
        body: addNoteInput.value 
   });
   var data = await response.text();
   console.log(data);
}


getAllBtn.addEventListener("click", getAllNotes);
getByIdBtn.addEventListener("click", getNoteById);
addNoteBtn.addEventListener("click", addNote);