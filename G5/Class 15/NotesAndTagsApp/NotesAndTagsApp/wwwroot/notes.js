let btnGetAllNoteSuperAdmin = document.getElementById('btn-get-all-superadmin');
let btnGetAllNote = document.getElementById('btn-get-all');
let btnAddNote = document.getElementById('btn-add-note');
let titleInput = document.getElementById('title');
let tagInput = document.getElementById('tag');
let userIdInput = document.getElementById('userId');
let priorityInput = document.getElementById('priority');


let url = 'http://localhost:5259/api/Notes'
let urlAddNote = 'http://localhost:5259/api/Notes/addNote'
let urlAllNote = 'http://localhost:5259/api/Notes/getAllUserNotes'



let getAllNoteSuperAdmin = async()=> {
    let token = localStorage.getItem('noteApiToken');

    let response =await fetch(url, { 
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    });

    let notes = await response.json();
    console.log(notes);
}

let getAllNote = async()=> {
    let token = localStorage.getItem('noteApiToken');

    let response =await fetch(urlAllNote, { 
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    });

    let notes = await response.json();
    console.log(notes);
}
let addNote = async()=>{
    let token = localStorage.getItem('noteApiToken');
    let note = {
        Text: titleInput.value,
        Priority: parseInt(priorityInput.value),
        Tag: parseInt(tagInput.value),
        UserId: parseInt(userIdInput.value)
    }

    let response = await fetch(urlAddNote,{
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(note)
    })
    .then(function(response) {
        console.log(response);
    })
    .catch(function(err) {
        console.log(err);
    })
}


btnGetAllNoteSuperAdmin.addEventListener("click", getAllNoteSuperAdmin );
btnAddNote.addEventListener("click", addNote );
btnGetAllNote.addEventListener("click", getAllNote );