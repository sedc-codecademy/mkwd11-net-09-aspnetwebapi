let getButton = document.getElementById("btn1");
let addNoteButton = document.getElementById("btn3")

let port = "7136";
let url = `https://localhost:${port}/api/notes`;

let getAllNotes = async () => {

    let response = await fetch(url);
    let data = await response.json();

    console.log(data)
}

let createNewNote = async () => {

    var note = {
        noteName: "some test note"
    }

    let response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': "application/json",
        },
        body: JSON.stringify(note)
    })

    let data = await response.json()
    console.log(data)
}

getButton.addEventListener("click", getAllNotes)
addNoteButton.addEventListener("click", createNewNote)