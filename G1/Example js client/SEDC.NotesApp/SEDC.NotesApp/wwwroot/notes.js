let getAllBtn = document.getElementById("btn1");
let addNoteBtn = document.getElementById("btn3");
let addNoteInput1 = document.getElementById("input31");
let addNoteInput2 = document.getElementById("input32");
let addNoteInput3 = document.getElementById("input33");
let addNoteInput4 = document.getElementById("input44");

let url = "http://localhost:64006/api/notes"

let getAllNotes = async() => {
  let token = localStorage.getItem("notesApiToken");

  let response = await fetch(url, {
      method : 'GET',
      headers : {
        'Authorization' : `Bearer ${token}`
      }
  });

  let notes = await response.json();
  console.log(notes);
}

let addNote = async() => {
    let token = localStorage.getItem("notesApiToken");

    let note = {
        Text : addNoteInput1.value,
        Color : addNoteInput2.value,
        Tag : parseInt(addNoteInput3.value),
        UserId : parseInt(addNoteInput4.value)
    };

    let response = await fetch(url, {
      method: 'POST',
      headers : {
          'Content-Type' : 'application/json',
          'Authorization' : `Bearer ${token}`
      },
      body : JSON.stringify(note)
    })
    .then(function(response){
        console.log(response);
    })
    .catch(function(err){
        console.log(err);
    })
}

getAllBtn.addEventListener("click", getAllNotes);
addNoteBtn.addEventListener("click", addNote);